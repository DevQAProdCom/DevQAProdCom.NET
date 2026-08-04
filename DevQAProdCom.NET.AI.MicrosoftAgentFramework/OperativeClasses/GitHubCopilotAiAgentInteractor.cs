using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
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
            _sessionConfigBuilder.WithAgent(agentIdentifier);
            return this;
        }

        public override GitHubCopilotAiAgentInteractor WithAgent(FileInfo agentMdFilePath)
        {
            _sessionConfigBuilder.WithAgent(agentMdFilePath);
            return this;
        }

        public override GitHubCopilotAiAgentInteractor WithWorkingDirectory(string workingDirectory)
        {
            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithSessionConfig(Func<SessionConfigBuilder, SessionConfigBuilder> updateSessionConfigFunc)
        {
            updateSessionConfigFunc(_sessionConfigBuilder);
            return this;
        }

        public GitHubCopilotAiAgentInteractor WithCopilotClientOptions(Func<CopilotClientOptionsBuilder, CopilotClientOptionsBuilder> updateCopilotClientOptionsFunc)
        {
            updateCopilotClientOptionsFunc(_copilotClientOptionsBuilder);
            return this;
        }

        protected override async Task<AIAgent> BuildAiAgentAsync(CancellationToken cancellationToken = default)
        {
            if(AiAgent!=null)
            {
                return AiAgent;
            }

            var copilotClientOptions = _copilotClientOptionsBuilder.Build();
            _copilotClient = new CopilotClient(copilotClientOptions);
            await _copilotClient.StartAsync(cancellationToken);

            var sessionConfig = _sessionConfigBuilder.Build();
            AIAgent agent = _copilotClient.AsAIAgent(sessionConfig, ownsClient:true);
            return agent;
        }

        public override async ValueTask DisposeAsync()
        {
            if (_copilotClient != null)
            {
                await _copilotClient.DisposeAsync();
                _copilotClient = null;
            }
        }
    }
}
