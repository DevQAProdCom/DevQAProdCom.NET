using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Models;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Builders
{
    public class GitHubCopilotAiAgentInteractorBuilder : MicrosoftAiAgentInteractorBuilder
    {
        private readonly SessionConfigBuilder _sessionConfigBuilder;
        private CopilotClient? _copilotClient;
        private readonly CopilotClientOptionsBuilder _copilotClientOptionsBuilder = new();
        private CopilotClientOptions? _copilotClientOptions;

        public GitHubCopilotAiAgentInteractorBuilder(ILogger logger) : base(logger)
        {
            _sessionConfigBuilder = new SessionConfigBuilder(logger);

        }

        public GitHubCopilotAiAgentInteractorBuilder(string agentIdentifier, ILogger logger) : this(logger)
        {
            WithAgent(agentIdentifier);
        }

        public GitHubCopilotAiAgentInteractorBuilder(string agentIdentifier, string workingDirectory, ILogger logger) : this(agentIdentifier, logger)
        {
            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
        }

        public GitHubCopilotAiAgentInteractorBuilder(FileInfo agentMdFilePath, string workingDirectory, ILogger logger) : this(logger)
        {
            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
        }


        public GitHubCopilotAiAgentInteractorBuilder WithAgent(string agentIdentifier)
        {
            ArgumentNullException.ThrowIfNull(agentIdentifier);

            _sessionConfigBuilder.WithPrimaryAgent(agentIdentifier);
            return this;
        }

        public GitHubCopilotAiAgentInteractorBuilder WithAgent(FileInfo agentMdFilePath)
        {
            ArgumentNullException.ThrowIfNull(agentMdFilePath);

            _sessionConfigBuilder.WithPrimaryAgentFromFile(agentMdFilePath.FullName);
            return this;
        }

        public GitHubCopilotAiAgentInteractorBuilder WithWorkingDirectory(string workingDirectory)
        {
            ArgumentNullException.ThrowIfNull(workingDirectory);

            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
            return this;
        }

        public GitHubCopilotAiAgentInteractorBuilder WithSessionConfig(Func<SessionConfigBuilder, SessionConfigBuilder> updateSessionConfigFunc)
        {
            ArgumentNullException.ThrowIfNull(updateSessionConfigFunc);

            updateSessionConfigFunc(_sessionConfigBuilder);
            return this;
        }

        public GitHubCopilotAiAgentInteractorBuilder WithCopilotClientOptions(Func<CopilotClientOptionsBuilder, CopilotClientOptionsBuilder> updateCopilotClientOptionsFunc)
        {
            ArgumentNullException.ThrowIfNull(updateCopilotClientOptionsFunc);

            updateCopilotClientOptionsFunc(_copilotClientOptionsBuilder);
            return this;
        }

        public GitHubCopilotAiAgentInteractorBuilder WithIsolation(string? baseDirectory = null)
        {
            baseDirectory ??= Path.Combine(
                Environment.CurrentDirectory,
                $"COPILOT_SESSSION_BASE_DIR_{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}");

            _copilotClientOptionsBuilder.WithBaseDirectory(baseDirectory);
            _sessionConfigBuilder.WithIsolation();
            _copilotClientOptionsBuilder.WithMode(CopilotClientMode.Empty);
            return this;
        }

        public async Task<IMicrosoftAiAgent> BuildAsync(CancellationToken cancellationToken = default)
        {
            _copilotClientOptions = _copilotClientOptionsBuilder.Build();
            _copilotClient = new CopilotClient(_copilotClientOptions);
            await _copilotClient.StartAsync(cancellationToken);

            var sessionConfig = _sessionConfigBuilder.Build();
            AIAgent aiAgent = _copilotClient.AsAIAgent(sessionConfig, ownsClient: true);

            var microsoftAiAgent = new MicrosoftAiAgent
            {
                AiAgent = aiAgent,
                DisposeProviderResourcesAsync = DisposeAsync
            };

            return microsoftAiAgent;
        }

        public async ValueTask DisposeAsync()
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
