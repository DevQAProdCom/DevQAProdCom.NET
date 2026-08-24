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

        public GitHubCopilotAiAgentInteractor WithIsolation(string? baseDirectory = null)
        {
            baseDirectory ??= Path.Combine(
                Environment.CurrentDirectory,
                $"COPILOT_SESSSION_BASE_DIR_{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}");

            _gitHubCopilotClientService
                .WithCopilotClientOptions(builder => builder.WithBaseDirectory(baseDirectory).WithMode(CopilotClientMode.Empty));

            _sessionConfigBuilder.WithIsolation();
            return this;
        }

        public override async Task<AIAgent> GetAiAgentAsync(CancellationToken cancellationToken = default)
        {
            if (AiAgent == null)
            {
                var copilotClient = _gitHubCopilotClientService.GetGitHubCopilotClient();
                await copilotClient.StartAsync(cancellationToken);

                _sessionConfig = _sessionConfigBuilder.Build();
                AiAgent = copilotClient.AsAIAgent(_sessionConfig, ownsClient: true);
            }

            return AiAgent;
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _gitHubCopilotClientService.DisposeAsync();
        }

        private void AddDefaultContentHandlers()
        {
            var handler = new GitHubCopilotAssistantMessageEventAiContentHandler(Logger);
            WithAiContentHandlers(handler);
        }
    }
}
