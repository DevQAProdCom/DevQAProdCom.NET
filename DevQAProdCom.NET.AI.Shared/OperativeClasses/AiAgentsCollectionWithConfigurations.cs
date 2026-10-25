using DevQAProdCom.NET.AI.Shared.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class AiAgentsCollectionWithConfigurations<TAiEntityConfiguration> : AiEntitiesCollection<TAiEntityConfiguration>, IAiAgentsCollectionWithConfigurations<TAiEntityConfiguration>
        where TAiEntityConfiguration : IAiAgentConfigurationWithCustomMetadata
    {
        private readonly ISearchEntitiesInLocations<IAiAgentConfiguration>? _configurationsSearcher;
        private readonly IAiAgentsConfigurationsMappers<TAiEntityConfiguration> _mapper;
        private readonly List<IAiAgentConfiguration> _configurations = new();

        public AiAgentsCollectionWithConfigurations(
            ILogger logger,
            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> aiAgentsSearcher,
            IAiAgentsConfigurationsMappers<TAiEntityConfiguration> mapper,
            ILocationsProvider? aiAgentsDefaultLocationsProvider = null,
            ISearchEntitiesInLocations<IAiAgentConfiguration>? configurationsSearcher = null,
            ILocationsProvider? defaultConfigurationsLocationsProvider = null,
            string? collectionIdentifier = null)
            : base(logger, searcher: aiAgentsSearcher, defaultLocationsProvider: aiAgentsDefaultLocationsProvider, collectionIdentifier: collectionIdentifier)
        {
            _configurationsSearcher = configurationsSearcher;
            _mapper = mapper;
            InitializeConfigurationsFromDefaultLocations(defaultConfigurationsLocationsProvider);
        }

        public override IAiEntityWithTConfigurationType<TAiEntityConfiguration> Add(IAiEntityWithTConfigurationType<TAiEntityConfiguration> entity)
        {
            var addedEntity = base.Add(entity);
            AddAgentConfigurations(addedEntity);
            return addedEntity;
        }

        public override IAiEntityWithTConfigurationType<TAiEntityConfiguration> AddFromFile(string filePath)
        {
            var addedEntity = base.AddFromFile(filePath);
            AddAgentConfigurations(addedEntity);
            return addedEntity;
        }

        public override bool TryGetByIdentifier(string entityIdentifier, out IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entity)
        {
            entity = ResolveByIdentifierAndConfiguration(entityIdentifier, SharedAiConstants.Configurations.DEFAULT, throwExceptionIfNotFound: false);
            return entity != null;
        }

        public override IAiEntityWithTConfigurationType<TAiEntityConfiguration> GetByIdentifier(string entityIdentifier)
        {
            return ResolveByIdentifierAndConfiguration(entityIdentifier, SharedAiConstants.Configurations.DEFAULT, throwExceptionIfNotFound: true)!;
        }

        public bool TryGetByConfiguration(string configurationIdentifier, out IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entity)
        {
            entity = ResolveByConfiguration(configurationIdentifier, throwExceptionIfNotFound: false);
            return entity != null;
        }

        public IAiEntityWithTConfigurationType<TAiEntityConfiguration> GetByConfiguration(string configurationIdentifier)
        {
            return ResolveByConfiguration(configurationIdentifier, throwExceptionIfNotFound: true)!;
        }

        public bool TryGetByIdentifierAndConfiguration(string entityIdentifier, string configurationIdentifier, out IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entity)
        {
            entity = ResolveByIdentifierAndConfiguration(entityIdentifier, configurationIdentifier, throwExceptionIfNotFound: false);
            return entity != null;
        }

        public IAiEntityWithTConfigurationType<TAiEntityConfiguration> GetByIdentifierAndConfiguration(string entityIdentifier, string configurationIdentifier)
        {
            return ResolveByIdentifierAndConfiguration(entityIdentifier, configurationIdentifier, throwExceptionIfNotFound: true)!;
        }

        public override bool TryGetByFilePath(string filePath, out IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entity)
        {
            entity = ResolveByFilePath(filePath, throwExceptionIfNotFound: false);
            return entity != null;
        }

        public override IAiEntityWithTConfigurationType<TAiEntityConfiguration> GetByFilePath(string filePath)
        {
            return ResolveByFilePath(filePath, throwExceptionIfNotFound: true)!;
        }

        public IAiEntityWithTConfigurationType<TAiEntityConfiguration> GetByFilePathAndConfiguration(string filePath, string configurationIdentifier)
        {
            return ResolveByFilePathAndConfiguration(filePath, configurationIdentifier, throwExceptionIfNotFound: true)!;
        }

        public bool TryGetByFilePathAndConfiguration(string filePath, string configurationIdentifier, out IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entity)
        {
            entity = ResolveByFilePathAndConfiguration(filePath, configurationIdentifier, throwExceptionIfNotFound: false);
            return entity != null;
        }

        private void InitializeConfigurationsFromDefaultLocations(ILocationsProvider? defaultConfigurationsLocationsProvider)
        {
            if (defaultConfigurationsLocationsProvider == null)
            {
                return;
            }

            if (_configurationsSearcher == null)
                throw new InvalidOperationException($"[{CollectionIdentifier}] Configurations searcher is not provided, but default configurations locations provider is specified.");

            foreach (var location in defaultConfigurationsLocationsProvider)
            {
                var configurationEntities = _configurationsSearcher.Search(location);

                foreach (var configurationEntity in configurationEntities)
                {
                    AddConfiguration(configurationEntity);
                }
            }
        }

        private void AddConfiguration(IAiAgentConfiguration configuration)
        {
            if (configuration == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(configuration.ConfigurationIdentifier))
            {
                return;
            }

            if (string.Equals(configuration.ConfigurationIdentifier, SharedAiConstants.Configurations.DEFAULT, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (_configurations.Any(existingConfiguration => existingConfiguration.ConfigurationIdentifier == configuration.ConfigurationIdentifier))
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] Duplicate configuration identifiers found for agent '{configuration.Name}': {configuration.ConfigurationIdentifier}.");
            }

            _configurations.Add(configuration);
        }

        private void AddAgentConfigurations(IAiEntityWithTConfigurationType<TAiEntityConfiguration> entity)
        {
            var agentConfigurationsWithoutAiEntityIdentifiers = entity.ConfigurationData.CustomMetadata?.Configurations;

            if (agentConfigurationsWithoutAiEntityIdentifiers == null || agentConfigurationsWithoutAiEntityIdentifiers.Count == 0)
            {
                return;
            }

            //Exluding default configurations. It is supposed that there is only single default configuration, which has either empty or "default" configuration identifier.
            //Default configuration is treated as agent specific configuration, which is not shared between agents. If user wants to have shared configuration between agents, he should create a configuration with a specific identifier to be referenced and used in multiple agents.
            //If there are multiple default configurations, it is considered a misconfiguration. 
            var configurationsExceptDefault = agentConfigurationsWithoutAiEntityIdentifiers
                .Where(configuration =>
                    !string.IsNullOrEmpty(configuration.ConfigurationIdentifier) &&
                    !string.Equals(configuration.ConfigurationIdentifier, SharedAiConstants.Configurations.DEFAULT, StringComparison.OrdinalIgnoreCase));

            var duplicatedConfigurationIdentifiers = configurationsExceptDefault
                .GroupBy(c => c.ConfigurationIdentifier)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicatedConfigurationIdentifiers.Count > 0)
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] Duplicate configuration identifiers found for agent '{entity.ConfigurationData.Name}': {string.Join(", ", duplicatedConfigurationIdentifiers)}.");
            }

            foreach (var configuration in configurationsExceptDefault)
            {
                var mappedConfiguration = _mapper.ToIAiAgentConfigurationFromAgentWithTConfigurationTypeConfigurations(entity, configuration);
                _configurations.Add(mappedConfiguration);
            }
        }

        private IAiEntityWithTConfigurationType<TAiEntityConfiguration>? ResolveByConfiguration(
           string configurationIdentifier,
           bool throwExceptionIfNotFound)
        {
            var configuration = GetConfiguration(configurationIdentifier, throwExceptionIfNotFound: throwExceptionIfNotFound);

            if (configuration == null)
                return null;

            var agent = GetAgentForAiAgentConfiguration(configuration, throwExceptionIfNotFound);

            if (agent == null)
                return null;

            return _mapper.ToAiAgentWithConfiguration(agent, configuration);
        }

        private IAiEntityWithTConfigurationType<TAiEntityConfiguration>? ResolveByIdentifierAndConfiguration(
            string entityIdentifier,
            string configurationIdentifier,
            bool throwExceptionIfNotFound)
        {
            if (!base.TryGetByIdentifier(entityIdentifier, out var agent))
            {
                if (throwExceptionIfNotFound)
                    throw new InvalidOperationException($"[{CollectionIdentifier}] Agent with identifier '{entityIdentifier}' is not found in the collection.");

                _logger.Warning("[{CollectionIdentifier}] Agent with identifier '{EntityIdentifier}' is not found in the collection.", CollectionIdentifier, entityIdentifier);
                return null;
            }

            if (string.IsNullOrEmpty(configurationIdentifier) || string.Equals(configurationIdentifier, SharedAiConstants.Configurations.DEFAULT, StringComparison.OrdinalIgnoreCase))
            {
                var defaultConfiguration = agent!.ConfigurationData.CustomMetadata?.GetDefaultConfiguration();
                return _mapper.ToAiAgentWithConfiguration(agent, defaultConfiguration);
            }

            var configuration = GetConfiguration(configurationIdentifier, throwExceptionIfNotFound: throwExceptionIfNotFound);

            if (configuration == null)
            {
                return null;
            }

            return _mapper.ToAiAgentWithConfiguration(agent!, configuration);
        }

        private IAiEntityWithTConfigurationType<TAiEntityConfiguration>? ResolveByFilePathAndConfiguration(
            string filePath,
            string configurationIdentifier,
            bool throwExceptionIfNotFound)
        {
            if (!base.TryGetByFilePath(filePath, out var agent))
            {
                if (throwExceptionIfNotFound)
                    throw new InvalidOperationException($"[{CollectionIdentifier}] Entity with file path '{filePath}' is not found in the collection.");

                _logger.Warning("[{CollectionIdentifier}] Entity with file path '{FilePath}' is not found in the collection.", CollectionIdentifier, filePath);
                return null;
            }

            if (string.IsNullOrEmpty(configurationIdentifier) || string.Equals(configurationIdentifier, SharedAiConstants.Configurations.DEFAULT, StringComparison.OrdinalIgnoreCase))
            {
                var defaultConfiguration = agent!.ConfigurationData.CustomMetadata?.GetDefaultConfiguration();
                return _mapper.ToAiAgentWithConfiguration(agent, defaultConfiguration);
            }

            var configuration = GetConfiguration(configurationIdentifier, throwExceptionIfNotFound: throwExceptionIfNotFound);

            if (configuration == null)
            {
                return null;
            }

            return _mapper.ToAiAgentWithConfiguration(agent!, configuration);
        }

        private IAiEntityWithTConfigurationType<TAiEntityConfiguration>? ResolveByFilePath(
            string filePath,
            bool throwExceptionIfNotFound)
        {
            if (!base.TryGetByFilePath(filePath, out var agent))
            {
                if (throwExceptionIfNotFound)
                    throw new InvalidOperationException($"[{CollectionIdentifier}] Entity with file path '{filePath}' is not found in the collection.");

                _logger.Warning("[{CollectionIdentifier}] Entity with file path '{FilePath}' is not found in the collection.", CollectionIdentifier, filePath);
                return null;
            }

            var defaultConfiguration = agent!.ConfigurationData.CustomMetadata?.GetDefaultConfiguration();
            return _mapper.ToAiAgentWithConfiguration(agent, defaultConfiguration);
        }

        private IAiEntityWithTConfigurationType<TAiEntityConfiguration>? GetAgentForAiAgentConfiguration(IAiAgentConfiguration configuration, bool throwExceptionIfNotFound)
        {
            //when trying to get an entity by configuration, we need to ensure that the configuration has a valid agent name associated with it, and that the agent exists in the collection, cause this is where prompt comes from
            if (string.IsNullOrEmpty(configuration.Name))
            {
                if (throwExceptionIfNotFound)
                    throw new InvalidOperationException($"[{CollectionIdentifier}] Configuration with identifier '{configuration.ConfigurationIdentifier}' does not have an agent name.");

                _logger.Warning("[{CollectionIdentifier}] Configuration with identifier '{ConfigurationIdentifier}' does not have an agent name.", CollectionIdentifier, configuration.ConfigurationIdentifier);
                return null;
            }

            if (!base.TryGetByIdentifier(configuration.Name, out var agent))
            {
                if (throwExceptionIfNotFound)
                    throw new InvalidOperationException($"[{CollectionIdentifier}] Agent with name '{configuration.Name}' for configuration '{configuration.ConfigurationIdentifier}' is not found in the collection.");

                _logger.Warning("[{CollectionIdentifier}] Agent with name '{AgentName}' for configuration '{ConfigurationIdentifier}' is not found in the collection.", CollectionIdentifier, configuration.Name, configuration.ConfigurationIdentifier);
                return null;
            }

            return agent;
        }

        private IAiAgentConfiguration? GetConfiguration(string configurationIdentifier, bool throwExceptionIfNotFound)
        {
            var configurations = _configurations.Where(existingConfiguration => existingConfiguration.ConfigurationIdentifier == configurationIdentifier);

            if (configurations.Count() > 1)
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] Multiple configurations with identifier '{configurationIdentifier}' are found.");
            }

            if (!configurations.Any())
            {
                if (throwExceptionIfNotFound)
                {
                    throw new InvalidOperationException($"[{CollectionIdentifier}] Configuration with identifier '{configurationIdentifier}' is not found in the collection.");
                }
                else
                {
                    _logger.Warning("[{CollectionIdentifier}] Configuration with identifier '{ConfigurationIdentifier}' is not found in the collection.", CollectionIdentifier, configurationIdentifier);
                }
            }

            return configurations.FirstOrDefault();
        }
    }
}
