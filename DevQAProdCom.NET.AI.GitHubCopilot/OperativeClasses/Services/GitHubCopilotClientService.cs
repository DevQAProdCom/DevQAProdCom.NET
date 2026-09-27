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

        public CopilotClient GetGitHubCopilotClient(CopilotClientOptions? copilotClientOptions = null)
        {
            if (_copilotClient == null)
            {
                if (copilotClientOptions != null)
                    _copilotClientOptions = copilotClientOptions;
                else
                    _copilotClientOptions = GetCopilotClientOptions();

                _copilotClient = new CopilotClient(_copilotClientOptions);
            }

            return _copilotClient;
        }

        public CopilotClientOptions GetCopilotClientOptions()
        {
            //Set directly without Builder
            if (_copilotClientOptions != null)
                return _copilotClientOptions;

            //Set using Builder
            if (_copilotClientOptionsBuilder != null)
            {
                return _copilotClientOptionsBuilder.Build();
            }

            //Default
            return new CopilotClientOptions();
        }

        public async ValueTask DisposeAsync()
        {
            if (_copilotClient != null)
            {
                await _copilotClient.DisposeAsync();
                _copilotClient = null;
            }

            if (!string.IsNullOrEmpty(_copilotClientOptions?.BaseDirectory))
            {
                Directory.Delete(_copilotClientOptions.BaseDirectory, recursive: true);
            }
        }
    }
}
