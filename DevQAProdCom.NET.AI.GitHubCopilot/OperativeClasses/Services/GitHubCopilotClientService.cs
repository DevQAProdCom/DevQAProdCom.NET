using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Services
{
    public class GitHubCopilotClientService : IGitHubCopilotClientService
    {
        private CopilotClientOptionsBuilder? _copilotClientOptionsBuilder;
        private CopilotClientOptions? _copilotClientOptions;
        private CopilotClient? _copilotClient;
        private ILogger _logger;

        public GitHubCopilotClientService(ILogger logger)
        {
            _logger = logger;
        }

        public GitHubCopilotClientService(ILogger logger, CopilotClientOptions copilotClientOptions) : this(logger)
        {
            _copilotClientOptions = copilotClientOptions;
        }

        public GitHubCopilotClientService WithCopilotClientOptions(Func<CopilotClientOptionsBuilder, CopilotClientOptionsBuilder> updateCopilotClientOptionsFunc)
        {
            _copilotClientOptionsBuilder ??= new CopilotClientOptionsBuilder(_logger);
            updateCopilotClientOptionsFunc(_copilotClientOptionsBuilder);
            return this;
        }

        public CopilotClient GetGitHubCopilotClient()
        {
            if (_copilotClient == null)
            {
                _copilotClientOptions = GetCopilotClientOptions();
                _copilotClient = new CopilotClient(_copilotClientOptions);
            }
            return _copilotClient;
        }

        private CopilotClientOptions GetCopilotClientOptions()
        {
            if (_copilotClientOptions != null)
            {
                return _copilotClientOptions;
            }
            if (_copilotClientOptionsBuilder != null)
            {
                _copilotClientOptions = _copilotClientOptionsBuilder.Build();
                return _copilotClientOptions;
            }
            throw new InvalidOperationException("Copilot client options have not been set. Please configure the options before getting the client.");
        }

        public async ValueTask DisposeAsync()
        {
            if (_copilotClient != null)
            {
                await _copilotClient.DisposeAsync();
                _copilotClient = null;
            }

            //if (_copilotClientOptions?.BaseDirectory != null)
            //{
            //    Directory.Delete(_copilotClientOptions.BaseDirectory, recursive: true);
            //}
        }
    }
}
