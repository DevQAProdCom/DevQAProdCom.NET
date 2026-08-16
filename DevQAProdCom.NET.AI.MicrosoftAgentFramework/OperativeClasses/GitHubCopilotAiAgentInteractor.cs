using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses
{
    public class GitHubCopilotAiAgentInteractor : MicrosoftAiAgentInteractor
    {
        private readonly SessionConfigBuilder _sessionConfigBuilder;
        private CopilotClient? _copilotClient;
        private readonly CopilotClientOptionsBuilder _copilotClientOptionsBuilder = new();
        private CopilotClientOptions _copilotClientOptions;

        public GitHubCopilotAiAgentInteractor(ILogger logger) : base(logger)
        {
            _sessionConfigBuilder = new SessionConfigBuilder(logger);

            var handler = new GitHubCopilotAssistantMessageEventAiContentHandler(logger);
            WithAiContentHandlers(handler);
        }

        public GitHubCopilotAiAgentInteractor(string agentIdentifier, ILogger logger) : this(logger)
        {
            WithAgent(agentIdentifier);
        }

        public GitHubCopilotAiAgentInteractor(string agentIdentifier, string workingDirectory, ILogger logger) : this(agentIdentifier, logger)
        {
            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
        }

        public GitHubCopilotAiAgentInteractor(FileInfo agentMdFilePath, string workingDirectory, ILogger logger) : this(logger)
        {
            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
        }


        public override GitHubCopilotAiAgentInteractor WithAgent(string agentIdentifier)
        {
            ArgumentNullException.ThrowIfNull(agentIdentifier);

            _sessionConfigBuilder.WithPrimaryAgent(agentIdentifier);
            return this;
        }

        public override GitHubCopilotAiAgentInteractor WithAgent(FileInfo agentMdFilePath)
        {
            ArgumentNullException.ThrowIfNull(agentMdFilePath);

            _sessionConfigBuilder.WithPrimaryAgentFromFile(agentMdFilePath.FullName);
            return this;
        }

        public override GitHubCopilotAiAgentInteractor WithWorkingDirectory(string workingDirectory)
        {
            ArgumentNullException.ThrowIfNull(workingDirectory);

            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithSessionConfig(Func<SessionConfigBuilder, SessionConfigBuilder> updateSessionConfigFunc)
        {
            ArgumentNullException.ThrowIfNull(updateSessionConfigFunc);

            updateSessionConfigFunc(_sessionConfigBuilder);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithCopilotClientOptions(Func<CopilotClientOptionsBuilder, CopilotClientOptionsBuilder> updateCopilotClientOptionsFunc)
        {
            ArgumentNullException.ThrowIfNull(updateCopilotClientOptionsFunc);

            updateCopilotClientOptionsFunc(_copilotClientOptionsBuilder);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithIsolation(string? baseDirectory = null)
        {
            baseDirectory ??= Path.Combine(
                Environment.CurrentDirectory,
                $"COPILOT_SESSSION_BASE_DIR_{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}");

            _copilotClientOptionsBuilder.WithBaseDirectory(baseDirectory);
            _sessionConfigBuilder.WithIsolation();
            _copilotClientOptionsBuilder.WithMode(CopilotClientMode.Empty);
            return this;
        }

        protected override async Task<AIAgent> BuildAiAgentAsync(CancellationToken cancellationToken = default)
        {
            if (AiAgent != null)
            {
                return AiAgent;
            }

            _copilotClientOptions = _copilotClientOptionsBuilder.Build();
            _copilotClient = new CopilotClient(_copilotClientOptions);
            await _copilotClient.StartAsync(cancellationToken);

            var sessionConfig = _sessionConfigBuilder.Build();
            AIAgent agent = _copilotClient.AsAIAgent(sessionConfig, ownsClient: true);
            return agent;
        }

        public override async ValueTask DisposeAsync()
        {
            if (_copilotClient != null)
            {
                await _copilotClient.DisposeAsync();
                _copilotClient = null;
            }

            if (_copilotClientOptions?.BaseDirectory != null)
            {
                Directory.Delete(_copilotClientOptions.BaseDirectory, recursive: true);
            }
        }
    }
}
