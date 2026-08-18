using DevQAProdCom.NET.AI.GitHubCopilot.Collections;
using DevQAProdCom.NET.AI.GitHubCopilot.Mappers;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class SessionConfigBuilder
    {
        private readonly SessionConfig _sessionConfig = new();

        private readonly Dictionary<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> _permissionDecisions = new();

        private PermissionDecisionsCollection? _permissionDecisionsCollection;
        private PermissionDecisionsCollection PermissionDecisionsCollection => _permissionDecisionsCollection ??= new();

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _allAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> AllAgentsCollection => _allAgentsCollection ??= new GitHubCopilotAiAgentsCollection(_logger);

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _sessionAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> SessionAgentsCollection => _sessionAgentsCollection ??= new GitHubCopilotAiAgentsCollection(_logger, initializeWithDefaultLocations: false);

        private GitHubCopilotMappers? _gitHubCopilotMappers;
        private GitHubCopilotMappers GitHubCopilotMappers => _gitHubCopilotMappers ??= new();

        private readonly ILogger _logger;

        private string? _sessionDirectory = null;

        public SessionConfigBuilder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public SessionConfigBuilder WithModel(string model)
        {
            LogSetting(nameof(_sessionConfig.Model), model);
            _sessionConfig.Model = model;
            return this;
        }

        public SessionConfigBuilder WithPrimaryAgent(string agentIdentifier)
        {
            WithAgent(agentIdentifier);
            var entityData = AllAgentsCollection.GetEntityDataByIdentifier(agentIdentifier);
            _sessionConfig.Agent = entityData.ConfigurationData.Name;
            LogSetting(nameof(_sessionConfig.Agent), _sessionConfig.Agent);
            WithModel(entityData.ConfigurationData.Model!);
            WithAvailableTools(entityData.ConfigurationData?.Tools?.ToArray()!);
            WithPermissions(entityData.ConfigurationData?.CustomPermissions?.ToArray()!);

            return this;
        }

        public SessionConfigBuilder WithPrimaryAgentFromFile(string filePath)
        {
            WithAgentFromFile(filePath);
            var entityData = SessionAgentsCollection.GetEntityDataByFilePath(filePath);
            _sessionConfig.Agent = entityData.ConfigurationData.Name;
            LogSetting(nameof(_sessionConfig.Agent), _sessionConfig.Agent);

            return this;
        }

        public SessionConfigBuilder WithAgent(string agentIdentifier)
        {
            _logger.Info("Loading {TypeName} '{PropertyName}' from agent identifier '{AgentIdentifier}'", nameof(SessionConfig), nameof(_sessionConfig.CustomAgents), agentIdentifier);
            var entityData = AllAgentsCollection.GetEntityDataByIdentifier(agentIdentifier);
            SessionAgentsCollection.AddEntityData(entityData);
            var customAgentConfig = GitHubCopilotMappers.ToCustomAgentConfig(entityData);
            WithCustomAgentConfig(customAgentConfig);

            return this;
        }

        public SessionConfigBuilder WithAgents(params string[] agentsIdentifiers)
        {
            foreach (var agentIdentifier in agentsIdentifiers)
            {
                WithAgent(agentIdentifier);
            }

            return this;
        }

        public SessionConfigBuilder WithAgentFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var entityData = AllAgentsCollection.AddEntityDataFromFile(filePath);
            SessionAgentsCollection.AddEntityData(entityData);
            var customAgentConfig = GitHubCopilotMappers.ToCustomAgentConfig(entityData);
            WithCustomAgentConfig(customAgentConfig);
            return this;
        }

        public SessionConfigBuilder WithAgentsFromFiles(params string[] filePaths)
        {
            foreach (var filePath in filePaths)
            {
                WithAgentFromFile(filePath);
            }

            return this;
        }

        public SessionConfigBuilder WithAgentsFromDirectory(string directoryPath)
        {
            var entities = AllAgentsCollection.AddEntitiesDataFromDirectory(directoryPath);
            var sessionEntities = SessionAgentsCollection.AddEntitiesDataFromDirectory(directoryPath);

            foreach (var entityData in sessionEntities)
            {
                WithCustomAgentConfig(GitHubCopilotMappers.ToCustomAgentConfig(entityData));
            }

            return this;
        }

        public SessionConfigBuilder WithAgentsFromDirectories(params string[] directoriesPaths)
        {
            foreach (var directoryPath in directoriesPaths)
            {
                WithAgentsFromDirectory(directoryPath);
            }

            return this;
        }

        public SessionConfigBuilder WithSystemMessage(string content, SystemMessageMode mode = SystemMessageMode.Append)
        {
            LogSystemMessage(mode, content);
            _sessionConfig.SystemMessage = new SystemMessageConfig { Content = content, Mode = mode };
            return this;
        }

        public SessionConfigBuilder WithAvailableTools(params string[] tools)
        {
            if (tools?.Length > 0)
            {
                var toolList = tools.ToList();
                LogCollectionSetting(nameof(_sessionConfig.AvailableTools), toolList);
                _sessionConfig.AvailableTools ??= new List<string>();

                foreach (var tool in toolList)
                {
                    if (!_sessionConfig.AvailableTools.Contains(tool))
                    {
                        _sessionConfig.AvailableTools.Add(tool);
                    }
                }
            }

            return this;
        }

        public SessionConfigBuilder WithStreaming(bool streaming = true)
        {
            LogSetting(nameof(_sessionConfig.Streaming), streaming);
            _sessionConfig.Streaming = streaming;
            return this;
        }

        public SessionConfigBuilder WithCustomAgentConfig(CustomAgentConfig config)
        {
            if (config == null)
            {
                _logger.Error("Attempted to add null {PropertyName} to {TypeName}", nameof(_sessionConfig.CustomAgents), nameof(SessionConfig));
                throw new ArgumentNullException(nameof(config), $"Custom agent configuration cannot be null when adding to {nameof(_sessionConfig.CustomAgents)}.");
            }

            _sessionConfig.CustomAgents ??= new List<CustomAgentConfig>();

            if (_sessionConfig.CustomAgents.Any(a => a.Name == config.Name))
            {
                _logger.Error("Agent with name '{AgentName}' already exists in {PropertyName} list", config.Name, nameof(_sessionConfig.CustomAgents));
                throw new InvalidOperationException($"Agent with name '{config.Name}' already exists in CustomAgentConfig list. Use {nameof(WithCustomAgentConfig)} to add a new agent config with a different name.");
            }

            _logger.Info("Adding {TypeName} '{PropertyName}' parameter as agent '{AgentName}'", nameof(SessionConfig), nameof(_sessionConfig.CustomAgents), config.Name);
            _sessionConfig.CustomAgents.Add(config);

            return this;
        }

        public SessionConfigBuilder WithMcpServer(string name, McpServerConfig config)
        {
            _logger.Info("Setting {TypeName} '{PropertyName}' parameter for server '{ServerName}'", nameof(SessionConfig), nameof(_sessionConfig.McpServers), name);
            _sessionConfig.McpServers ??= new Dictionary<string, McpServerConfig>();
            _sessionConfig.McpServers[name] = config;
            return this;
        }

        public SessionConfigBuilder WithSkipCustomInstructions(bool? skipCustomInstructions)
        {
            LogSetting(nameof(_sessionConfig.SkipCustomInstructions), skipCustomInstructions?.ToString() ?? "null");
            _sessionConfig.SkipCustomInstructions = skipCustomInstructions;
            return this;
        }

        public SessionConfigBuilder WithCustomAgentsLocalOnly(bool? customAgentsLocalOnly)
        {
            LogSetting(nameof(_sessionConfig.CustomAgentsLocalOnly), customAgentsLocalOnly?.ToString() ?? "null");
            _sessionConfig.CustomAgentsLocalOnly = customAgentsLocalOnly;
            return this;
        }

        public SessionConfigBuilder WithExcludedTools(params string[] excludedTools)
        {
            var toolList = excludedTools.ToList();
            LogCollectionSetting(nameof(_sessionConfig.ExcludedTools), toolList);
            _sessionConfig.ExcludedTools = toolList;
            return this;
        }

        public SessionConfigBuilder WithEnableSkills(bool? enableSkills)
        {
            LogSetting(nameof(_sessionConfig.EnableSkills), enableSkills?.ToString() ?? "null");
            _sessionConfig.EnableSkills = enableSkills;
            return this;
        }

        public SessionConfigBuilder WithIncludeSubAgentStreamingEvents(bool includeSubAgentStreamingEvents)
        {
            LogSetting(nameof(_sessionConfig.IncludeSubAgentStreamingEvents), includeSubAgentStreamingEvents);
            _sessionConfig.IncludeSubAgentStreamingEvents = includeSubAgentStreamingEvents;
            return this;
        }

        public SessionConfigBuilder WithSkillDirectories(params string[] skillDirectories)
        {
            var directoryList = skillDirectories.ToList();
            LogCollectionSetting(nameof(_sessionConfig.SkillDirectories), directoryList);
            _sessionConfig.SkillDirectories = directoryList;
            return this;
        }

        public SessionConfigBuilder WithInstructionDirectories(params string[] instructionDirectories)
        {
            var directoryList = instructionDirectories.ToList();
            LogCollectionSetting(nameof(_sessionConfig.InstructionDirectories), directoryList);
            _sessionConfig.InstructionDirectories = directoryList;
            return this;
        }

        public SessionConfigBuilder WithDisabledSkills(params string[] disabledSkills)
        {
            var skillList = disabledSkills.ToList();
            LogCollectionSetting(nameof(_sessionConfig.DisabledSkills), skillList);
            _sessionConfig.DisabledSkills = skillList;
            return this;
        }

        public SessionConfigBuilder WithEnableConfigDiscovery(bool? enableConfigDiscovery)
        {
            LogSetting(nameof(_sessionConfig.EnableConfigDiscovery), enableConfigDiscovery?.ToString() ?? "null");
            _sessionConfig.EnableConfigDiscovery = enableConfigDiscovery;
            return this;
        }

        public SessionConfigBuilder WithOrganizationCustomInstructions(string? organizationCustomInstructions)
        {
            LogSetting(nameof(_sessionConfig.OrganizationCustomInstructions), $"Content={organizationCustomInstructions?.TruncateWithCount(50) ?? "null"}");
            _sessionConfig.OrganizationCustomInstructions = organizationCustomInstructions;
            return this;
        }

        public SessionConfigBuilder WithEnableOnDemandInstructionDiscovery(bool? enableOnDemandInstructionDiscovery)
        {
            LogSetting(nameof(_sessionConfig.EnableOnDemandInstructionDiscovery), enableOnDemandInstructionDiscovery?.ToString() ?? "null");
            _sessionConfig.EnableOnDemandInstructionDiscovery = enableOnDemandInstructionDiscovery;
            return this;
        }

        public SessionConfigBuilder WithIsolation()
        {
            _logger.Info("Applying isolation configuration to {TypeName}", nameof(SessionConfig));
            return WithSkipCustomInstructions(true)
                .WithCustomAgentsLocalOnly(true)
                .WithEnableSkills(false)
                .WithEnableConfigDiscovery(false)
                .WithEnableOnDemandInstructionDiscovery(false)
                .WithIncludeSubAgentStreamingEvents(false);
        }

        public SessionConfigBuilder SetPermission(string identifier, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>> permissionFunc)
        {
            ArgumentNullException.ThrowIfNull(identifier);
            ArgumentNullException.ThrowIfNull(permissionFunc);

            _logger.Info("Setting {TypeName} permission decision for '{Identifier}'", nameof(SessionConfig), identifier);
            _permissionDecisions[identifier] = async (request, invocation) =>
            {
                var decision = await permissionFunc(request, invocation);

                return decision;
            };

            return this;
        }

        public SessionConfigBuilder WithPermission(string identifier)
        {
            ArgumentNullException.ThrowIfNull(identifier);

            _logger.Info("Setting {TypeName} permission '{Identifier}' from collection", nameof(SessionConfig), identifier);
            var permissionDecision = PermissionDecisionsCollection.GetByIdentifier(identifier);
            _permissionDecisions[identifier] = permissionDecision;
            return this;
        }

        public SessionConfigBuilder WithPermissions(params string[] identifiers)
        {
            if (identifiers?.Count() > 0)
                foreach (var identifier in identifiers)
                {
                    WithPermission(identifier);
                }

            return this;
        }

        public SessionConfigBuilder WithSessionConfig(Func<SessionConfig, SessionConfig> updateSessionConfig)
        {
            ArgumentNullException.ThrowIfNull(updateSessionConfig);

            _logger.Info("Applying custom {TypeName} update", nameof(SessionConfig));
            updateSessionConfig.Invoke(_sessionConfig);
            return this;
        }

        public SessionConfig Build()
        {
            _logger.Info("Building {TypeName} Agent: {Agent}, (Model: {Model})", nameof(SessionConfig), _sessionConfig.Agent ?? "default", _sessionConfig.Model ?? "default");

            _sessionConfig.OnPermissionRequest = async (request, invocation) =>
            {
                _logger.Info($"Permission Request:\nType= {request.ToString()}\nBody = {request.ToJson()}");

                if (_permissionDecisions?.Count > 0)
                {

                    foreach (var permissionDecision in _permissionDecisions)
                    {
                        try
                        {
                            if (permissionDecision.Value != null)
                            {
                                var result = await permissionDecision.Value(request, invocation);
                                if (result != null)
                                {
                                    return result!;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error($"Error processing permission decision '{permissionDecision.Key}': {ex.Message}");
                        }
                    }

                }

                return PermissionDecision.Reject("Review the available tools and use only those permitted to complete the task. If no suitable tools are found, list all available tools and indicate that the requested tool cannot be executed.");
            };

            _logger.Info("{TypeName} built successfully Agent: {Agent}, (Model: {Model})", nameof(SessionConfig), _sessionConfig.Agent ?? "default", _sessionConfig.Model ?? "default");
            return _sessionConfig;
        }


        private void LogSetting(string propertyName, object value)
        {
            _logger.Info("Setting {TypeName} '{PropertyName}' parameter to '{Value}'", nameof(SessionConfig), propertyName, value);
        }

        private void LogCollectionSetting(string propertyName, IEnumerable<string> values)
        {
            _logger.Info("Setting {TypeName} '{PropertyName}' parameter to '[{Value}]'", nameof(SessionConfig), propertyName, string.Join(", ", values));
        }

        private void LogSystemMessage(SystemMessageMode mode, string? content)
        {
            _logger.Info(
                "Setting {TypeName} '{PropertyName}' parameter to 'Mode={Mode}, Content={Content}'",
                nameof(SessionConfig),
                nameof(_sessionConfig.SystemMessage),
                mode,
                content?.TruncateWithCount(50) ?? "null");
        }
    }
}
