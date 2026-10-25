using System.Text.Json;
using System.Text.Json.Serialization;
using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.Shared.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses
{
    public class GitHubCopilotAiAgentInteractor : MicrosoftAiAgentInteractorT<GitHubCopilotAiAgentInteractor>
    {
        private readonly SessionConfigBuilder _sessionConfigBuilder;
        private SessionConfig? _sessionConfig;
        private IGitHubCopilotClientService _gitHubCopilotClientService;

        private JsonSerializerOptions _jsonSerializationOptions => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public GitHubCopilotAiAgentInteractor(
            IGitHubCopilotClientService gitHubCopilotClientService,
            IMicrosoftAiAgentInteractor microsoftAiAgentInteractor,
            ILogger logger,

            // Agents
            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>? agentsSearcher = null,
            ILocationsProvider? agentsDefaultLocationsProvider = null,
            IAiAgentsConfigurationsMappers<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? agentsConfigurationsMappers = null,
            ISearchEntitiesInLocations<IAiAgentConfiguration>? agentsConfigurationsSearcher = null,
            ILocationsProvider? agentsDefaultConfigurationsLocationsProvider = null,
            IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? allAgentsCollection = null,
            IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? sessionAgentsCollection = null,

            // Instructions
            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiInstructionConfiguration>>? instructionsSearcher = null,
            ILocationsProvider? instructionsDefaultLocationsProvider = null,
            IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>? allInstructionsCollection = null,
            IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>? sessionInstructionsCollection = null,

            // Skills
            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiSkillConfiguration>>? skillsSearcher = null,
            ILocationsProvider? skillsDefaultLocationsProvider = null,
            IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>? allSkillsCollection = null,
            IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>? sessionSkillsCollection = null,

            // Hooks
            IFileBasedHooksSearcher? fileBasedHooksSearcher = null,
            ILocationsProvider? fileBasedHooksDefaultLocationsProvider = null,
            IFileBasedHooksCollection? allFileBasedHooksCollection = null,
            IFileBasedHooksCollection? sessionFileBasedHooksCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? allSdkBasedSessionHooksCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? sessionSdkBasedSessionHooksCollection = null,

            // MCP Servers
            IFileBasedMcpServersSearcher? fileBasedMcpServersSearcher = null,
            ILocationsProvider? fileBasedMcpServersDefaultLocationsProvider = null,
            IFileBasedMcpServersCollection? allFileBasedMcpServersCollection = null,
            IFileBasedMcpServersCollection? sessionFileBasedMcpServersCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>? allSdkBasedMcpServersCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>? sessionSdkBasedMcpServersCollection = null,

            // GitHub Copilot Mappers
            IGitHubCopilotMappers? gitHubCopilotMappers = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>? allPermissionDecisionsCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>? sessionPermissionDecisionsCollection = null,
            CopilotClientMode copilotClientMode = CopilotClientMode.Empty) : base(microsoftAiAgentInteractor, logger)
        {
            _sessionConfigBuilder = new SessionConfigBuilder(
                logger,

                //Agents
                agentsSearcher: agentsSearcher,
                agentsDefaultLocationsProvider: agentsDefaultLocationsProvider,
                agentsConfigurationsMappers: agentsConfigurationsMappers,
                agentsConfigurationsSearcher: agentsConfigurationsSearcher,
                agentsDefaultConfigurationsLocationsProvider: agentsDefaultConfigurationsLocationsProvider,
                allAgentsCollection: allAgentsCollection,
                sessionAgentsCollection: sessionAgentsCollection,

                //Instructions
                instructionsSearcher: instructionsSearcher,
                instructionsDefaultLocationsProvider: instructionsDefaultLocationsProvider,
                allInstructionsCollection: allInstructionsCollection,
                sessionInstructionsCollection: sessionInstructionsCollection,

                //Skills
                skillsSearcher: skillsSearcher,
                skillsDefaultLocationsProvider: skillsDefaultLocationsProvider,
                allSkillsCollection: allSkillsCollection,
                sessionSkillsCollection: sessionSkillsCollection,

                //Hooks
                fileBasedHooksSearcher: fileBasedHooksSearcher,
                fileBasedHooksDefaultLocationsProvider: fileBasedHooksDefaultLocationsProvider,
                allFileBasedHooksCollection: allFileBasedHooksCollection,
                sessionFileBasedHooksCollection: sessionFileBasedHooksCollection,
                allSdkBasedSessionHooksCollection: allSdkBasedSessionHooksCollection,
                sessionSdkBasedSessionHooksCollection: sessionSdkBasedSessionHooksCollection,

                //MCP Servers
                fileBasedMcpServersSearcher: fileBasedMcpServersSearcher,
                fileBasedMcpServersDefaultLocationsProvider: fileBasedMcpServersDefaultLocationsProvider,
                allFileBasedMcpServersCollection: allFileBasedMcpServersCollection,
                sessionFileBasedMcpServersCollection: sessionFileBasedMcpServersCollection,
                allSdkBasedMcpServersCollection: allSdkBasedMcpServersCollection,
                sessionSdkBasedMcpServersCollection: sessionSdkBasedMcpServersCollection,

                gitHubCopilotMappers: gitHubCopilotMappers,
                allPermissionDecisionsCollection: allPermissionDecisionsCollection,
                sessionPermissionDecisionsCollection: sessionPermissionDecisionsCollection,
                copilotClientMode: copilotClientMode);

            _gitHubCopilotClientService = gitHubCopilotClientService;
        }

        public GitHubCopilotAiAgentInteractor WithPrimaryAgent(string agentIdentifier, string configuration = SharedAiConstants.Configurations.DEFAULT)
        {
            _sessionConfigBuilder.WithPrimaryAgent(agentIdentifier, configuration);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithPrimaryAgentFromFile(string filePath, string configuration = SharedAiConstants.Configurations.DEFAULT)
        {
            _sessionConfigBuilder.WithPrimaryAgentFromFile(filePath, configuration);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithCopilotClientWorkingDirectory(string workingDirectory)
        {
            _gitHubCopilotClientService.WithCopilotClientOptions(builder => builder.WithWorkingDirectory(workingDirectory));
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithCopilotClientBaseDirectory(string baseDirectory)
        {
            _gitHubCopilotClientService.WithCopilotClientOptions(builder => builder.WithBaseDirectory(baseDirectory));
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithSessionWorkingDirectory(string workingDirectory)
        {
            _sessionConfigBuilder.WithWorkingDirectory(workingDirectory);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithWorkingDirectory(string workingDirectory)
        {
            WithCopilotClientWorkingDirectory(workingDirectory);
            WithSessionWorkingDirectory(workingDirectory);

            return this;
        }

        public GitHubCopilotAiAgentInteractor WithSessionConfig(SessionConfig sessionConfig)
        {
            _sessionConfig = sessionConfig;
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithSessionConfig(Func<SessionConfigBuilder, SessionConfigBuilder> updateSessionConfigFunc)
        {
            updateSessionConfigFunc(_sessionConfigBuilder);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithCopilotClientOptions(Func<CopilotClientOptionsBuilder, CopilotClientOptionsBuilder> updateCopilotClientOptionsFunc)
        {
            _gitHubCopilotClientService.WithCopilotClientOptions(updateCopilotClientOptionsFunc);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithDefaultContentHandlers()
        {
            AddDefaultContentHandlers();
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithFullIsolation(string? baseDirectory = null)
        {
            Logger.Info($"Creating isolated GitHub Copilot session with base directory: {baseDirectory}");

            //_sessionConfigBuilder.WithBaseDirectory(baseDirectory);

            _gitHubCopilotClientService
                .WithCopilotClientOptions(builder => builder
                .WithBaseDirectory(baseDirectory)
                .WithMode(CopilotClientMode.Empty));

            _sessionConfigBuilder
                .WithFullIsolation()
                .WithClientMode(CopilotClientMode.Empty);

            return this;
        }

        public GitHubCopilotAiAgentInteractor WithSelectiveIsolation(string? baseDirectory = null)
        {
            Logger.Info($"Creating isolated GitHub Copilot session with base directory: {baseDirectory}");

            _gitHubCopilotClientService
                .WithCopilotClientOptions(builder => builder
                .WithBaseDirectory(baseDirectory)
                .WithMode(CopilotClientMode.Empty));

            _sessionConfigBuilder
                .WithSelectiveIsolation()
                .WithClientMode(CopilotClientMode.Empty);

            return this;
        }

        public override async Task<AIAgent> GetAiAgentAsync(CancellationToken cancellationToken = default)
        {
            if (AiAgent == null)
            {
                var directoryForInteractionConfigurationData = SetupDirectoryForInteractionConfigurationData();

                var copilotClient = _gitHubCopilotClientService.GetGitHubCopilotClient();
                await copilotClient.StartAsync(cancellationToken);

                _sessionConfig = _sessionConfigBuilder
                    .WithDirectoryForInteractionConfigurationData(directoryForInteractionConfigurationData)
                    .Build();

                var copilotClientOptions = _gitHubCopilotClientService.GetCopilotClientOptions();
                copilotClientOptions.ToJsonFile(Path.Combine(directoryForInteractionConfigurationData, $"{nameof(CopilotClientOptions)}.json"), _jsonSerializationOptions);

                var agentRunOptions = MicrosoftAiAgentInteractor.GetAgentRunOptions();
                agentRunOptions.ToJsonFile(Path.Combine(directoryForInteractionConfigurationData, $"{nameof(AgentRunOptions)}.json"), _jsonSerializationOptions);

                AiAgent = copilotClient.AsAIAgent(_sessionConfig, ownsClient: true);
            }

            return AiAgent;
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _gitHubCopilotClientService.DisposeAsync();
            _sessionConfigBuilder?.Dispose();
        }

        private void AddDefaultContentHandlers()
        {
            var handler = new GitHubCopilotAssistantMessageEventAiContentHandler(Logger);
            WithAiContentHandlers(handler);
        }
    }
}
