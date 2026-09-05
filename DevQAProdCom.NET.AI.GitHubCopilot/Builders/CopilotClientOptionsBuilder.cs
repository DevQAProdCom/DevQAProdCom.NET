using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.Constans;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using MicrosoftLogger = Microsoft.Extensions.Logging.ILogger;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class CopilotClientOptionsBuilder(ILogger logger)
    {
        private readonly ILogger _logger = logger;
        private readonly CopilotClientOptions _options = new();

        public CopilotClientOptionsBuilder WithMode(CopilotClientMode mode)
        {
            _options.Mode = mode;
            LogSetting(nameof(_options.Mode), mode);
            return this;
        }

        public CopilotClientOptionsBuilder WithConnection(RuntimeConnection? connection)
        {
            _options.Connection = connection;
            LogSetting(nameof(_options.Connection), connection?.ToString() ?? "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithWorkingDirectory(string? workingDirectory)
        {
            _options.WorkingDirectory = workingDirectory;
            LogSetting(nameof(_options.WorkingDirectory), workingDirectory ?? "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithBaseDirectory(string? baseDirectory = null)
        {
            baseDirectory ??= Path.Combine(Path.GetTempPath(), $"COPILOT_SESSION_BASE_DIR_{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}");
            _options.BaseDirectory = baseDirectory;
            LogSetting(nameof(_options.BaseDirectory), baseDirectory);
            return this;
        }

        public CopilotClientOptionsBuilder WithLogLevel(CopilotLogLevel? logLevel)
        {
            _options.LogLevel = logLevel;
            LogSetting(nameof(_options.LogLevel), logLevel?.ToString() ?? "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithEnvironment(IReadOnlyDictionary<string, string>? environment)
        {
            _options.Environment = environment;
            LogSetting(nameof(_options.Environment), environment != null ? $"{environment.Count} entries" : "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithLogger(MicrosoftLogger? logger)
        {
            _options.Logger = logger;
            LogSetting(nameof(_options.Logger), logger?.GetType().Name ?? "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithGitHubToken(string? gitHubToken)
        {
            _options.GitHubToken = gitHubToken;
            LogSetting(nameof(_options.GitHubToken), gitHubToken != null ? "[set]" : "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithUseLoggedInUser(bool? useLoggedInUser)
        {
            _options.UseLoggedInUser = useLoggedInUser;
            LogSetting(nameof(_options.UseLoggedInUser), useLoggedInUser?.ToString() ?? "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithOnListModels(Func<CancellationToken, Task<IList<ModelInfo>>>? onListModels)
        {
            _options.OnListModels = onListModels;
            LogSetting(nameof(_options.OnListModels), onListModels != null ? "set" : "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithSessionFs(SessionFsConfig? sessionFs)
        {
            _options.SessionFs = sessionFs;
            LogSetting(nameof(_options.SessionFs), sessionFs != null ? "configured" : "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithRequestHandler(CopilotRequestHandler? requestHandler)
        {
            _options.RequestHandler = requestHandler;
            LogSetting(nameof(_options.RequestHandler), requestHandler?.GetType().Name ?? "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithTelemetry(TelemetryConfig? telemetry)
        {
            _options.Telemetry = telemetry;
            LogSetting(nameof(_options.Telemetry), telemetry != null ? "configured" : "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithSessionIdleTimeoutSeconds(int? sessionIdleTimeoutSeconds)
        {
            _options.SessionIdleTimeoutSeconds = sessionIdleTimeoutSeconds;
            LogSetting(nameof(_options.SessionIdleTimeoutSeconds), sessionIdleTimeoutSeconds?.ToString() ?? "null");
            return this;
        }

        public CopilotClientOptionsBuilder WithEnableRemoteSessions(bool enableRemoteSessions)
        {
            _options.EnableRemoteSessions = enableRemoteSessions;
            LogSetting(nameof(_options.EnableRemoteSessions), enableRemoteSessions);
            return this;
        }

        public CopilotClientOptionsBuilder WithCopilotClientOptions(Func<CopilotClientOptions, CopilotClientOptions> updateOptions)
        {
            updateOptions.Invoke(_options);
            LogSetting(nameof(_options), "updated via custom configuration");
            return this;
        }

        public CopilotClientOptions Build()
        {
            return _options;
        }

        private void LogSetting(string propertyName, object value)
        {
            _logger.Info("🛠️[{LogArea}] ⚙️[{TypeName}] Setting 🔧'{PropertyName}' parameter to '{Value}'.", $"{SharedLoggingConstants.Area.Config}", $"{nameof(CopilotClientOptionsBuilder)}", propertyName, value);
        }
    }
}
