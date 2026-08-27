using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using MicrosoftLogger = Microsoft.Extensions.Logging.ILogger;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class CopilotClientOptionsBuilder(ILogger logger)
    {
        private readonly CopilotClientOptions _options = new();

        public CopilotClientOptionsBuilder WithMode(CopilotClientMode mode)
        {
            _options.Mode = mode;
            return this;
        }

        public CopilotClientOptionsBuilder WithConnection(RuntimeConnection? connection)
        {
            _options.Connection = connection;
            return this;
        }

        public CopilotClientOptionsBuilder WithWorkingDirectory(string? workingDirectory)
        {
            _options.WorkingDirectory = workingDirectory;
            return this;
        }

        public CopilotClientOptionsBuilder WithBaseDirectory(string? baseDirectory = null)
        {
            baseDirectory ??= Path.Combine(Path.GetTempPath(), $"COPILOT_SESSSION_BASE_DIR_{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}");
            _options.BaseDirectory = baseDirectory;
            return this;
        }

        public CopilotClientOptionsBuilder WithLogLevel(CopilotLogLevel? logLevel)
        {
            _options.LogLevel = logLevel;
            return this;
        }

        public CopilotClientOptionsBuilder WithEnvironment(IReadOnlyDictionary<string, string>? environment)
        {
            _options.Environment = environment;
            return this;
        }

        public CopilotClientOptionsBuilder WithLogger(MicrosoftLogger? logger)
        {
            _options.Logger = logger;
            return this;
        }

        public CopilotClientOptionsBuilder WithGitHubToken(string? gitHubToken)
        {
            _options.GitHubToken = gitHubToken;
            return this;
        }

        public CopilotClientOptionsBuilder WithUseLoggedInUser(bool? useLoggedInUser)
        {
            _options.UseLoggedInUser = useLoggedInUser;
            return this;
        }

        public CopilotClientOptionsBuilder WithOnListModels(Func<CancellationToken, Task<IList<ModelInfo>>>? onListModels)
        {
            _options.OnListModels = onListModels;
            return this;
        }

        public CopilotClientOptionsBuilder WithSessionFs(SessionFsConfig? sessionFs)
        {
            _options.SessionFs = sessionFs;
            return this;
        }

        public CopilotClientOptionsBuilder WithRequestHandler(CopilotRequestHandler? requestHandler)
        {
            _options.RequestHandler = requestHandler;
            return this;
        }

        public CopilotClientOptionsBuilder WithTelemetry(TelemetryConfig? telemetry)
        {
            _options.Telemetry = telemetry;
            return this;
        }

        public CopilotClientOptionsBuilder WithSessionIdleTimeoutSeconds(int? sessionIdleTimeoutSeconds)
        {
            _options.SessionIdleTimeoutSeconds = sessionIdleTimeoutSeconds;
            return this;
        }

        public CopilotClientOptionsBuilder WithEnableRemoteSessions(bool enableRemoteSessions)
        {
            _options.EnableRemoteSessions = enableRemoteSessions;
            return this;
        }

        public CopilotClientOptionsBuilder WithCopilotClientOptions(Func<CopilotClientOptions, CopilotClientOptions> updateOptions)
        {
            updateOptions.Invoke(_options);
            return this;
        }

        public CopilotClientOptions Build()
        {
            return _options;
        }
    }
}
