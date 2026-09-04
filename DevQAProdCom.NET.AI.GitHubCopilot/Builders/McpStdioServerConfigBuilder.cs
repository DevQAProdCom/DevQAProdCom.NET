using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class McpStdioServerConfigBuilder
    {
        private readonly McpStdioServerConfig _config = new();

        private readonly ILogger _logger;

        public McpStdioServerConfigBuilder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public McpStdioServerConfigBuilder WithCommand(string command)
        {
            LogSetting(nameof(_config.Command), command);
            _config.Command = command;
            return this;
        }

        public McpStdioServerConfigBuilder WithArgs(params string[]? args)
        {
            var argsList = args?.ToList() ?? new List<string>();
            LogCollectionSetting(nameof(_config.Args), argsList);
            _config.Args = argsList;
            return this;
        }

        public McpStdioServerConfigBuilder WithEnv(IDictionary<string, string>? env)
        {
            if (env != null)
            {
                foreach (var kvp in env)
                {
                    _logger.Info("{TypeName} Setting '{PropertyName}' parameter entry '{Key}' to '{Value}'.", $"[{nameof(McpStdioServerConfigBuilder)}]", nameof(_config.Env), kvp.Key, kvp.Value);
                }
            }

            _config.Env = env;
            return this;
        }

        public McpStdioServerConfigBuilder WithEnv(string key, string value)
        {
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter entry '{Key}' to '{Value}'.", $"[{nameof(McpStdioServerConfigBuilder)}]", nameof(_config.Env), key, value);
            _config.Env ??= new Dictionary<string, string>();
            _config.Env[key] = value;
            return this;
        }

        public McpStdioServerConfigBuilder WithWorkingDirectory(string? workingDirectory)
        {
            LogSetting(nameof(_config.WorkingDirectory), workingDirectory ?? "null");
            _config.WorkingDirectory = workingDirectory;
            return this;
        }

        public McpStdioServerConfigBuilder WithTools(params string[]? tools)
        {
            var toolList = tools?.ToList() ?? new List<string>();
            LogCollectionSetting(nameof(_config.Tools), toolList);
            _config.Tools = toolList;
            return this;
        }

        public McpStdioServerConfigBuilder WithTimeout(int? timeout)
        {
            LogSetting(nameof(_config.Timeout), timeout?.ToString() ?? "null");
            _config.Timeout = timeout;
            return this;
        }

        public McpStdioServerConfigBuilder WithConfig(Func<McpStdioServerConfig, McpStdioServerConfig> updateConfig)
        {
            ArgumentNullException.ThrowIfNull(updateConfig);

            _logger.Info("{TypeName} Applying custom {TypeName} update.", $"[{nameof(McpStdioServerConfigBuilder)}]");
            updateConfig.Invoke(_config);
            return this;
        }

        public McpStdioServerConfig Build()
        {
            _logger.Info("{TypeName} Building stdio MCP server configuration (Command: {Command}).", $"[{nameof(McpStdioServerConfigBuilder)}]", _config.Command ?? "default");
            return _config;
        }

        private void LogSetting(string propertyName, object value)
        {
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '{Value}'.", $"[{nameof(McpStdioServerConfigBuilder)}]", propertyName, value);
        }

        private void LogCollectionSetting(string propertyName, IEnumerable<string> values)
        {
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '[{Value}]'.", $"[{nameof(McpStdioServerConfigBuilder)}]", propertyName, string.Join(", ", values));
        }
    }
}
