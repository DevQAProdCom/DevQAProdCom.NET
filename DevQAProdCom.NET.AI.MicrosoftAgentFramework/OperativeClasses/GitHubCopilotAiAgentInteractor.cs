using System.Text.Json;
using System.Text.Json.Serialization;
using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses
{
    public class GitHubCopilotAiAgentInteractor : MicrosoftAiAgentInteractorT<GitHubCopilotAiAgentInteractor>
    {
        private readonly SessionConfigBuilder _sessionConfigBuilder;
        private SessionConfig _sessionConfig;
        private IGitHubCopilotClientService _gitHubCopilotClientService;

        private JsonSerializerOptions _jsonSerializationOptions => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public GitHubCopilotAiAgentInteractor(IGitHubCopilotClientService gitHubCopilotClientService, IMicrosoftAiAgentInteractor microsoftAiAgentInteractor, ILogger logger) : base(microsoftAiAgentInteractor, logger)
        {
            _sessionConfigBuilder = new SessionConfigBuilder(logger);
            _gitHubCopilotClientService = gitHubCopilotClientService;
        }

        public GitHubCopilotAiAgentInteractor WithPrimaryAgent(string agentIdentifier)
        {
            _sessionConfigBuilder.WithPrimaryAgent(agentIdentifier);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithPrimaryAgentFromFile(string filePath)
        {
            _sessionConfigBuilder.WithPrimaryAgentFromFile(filePath);
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
