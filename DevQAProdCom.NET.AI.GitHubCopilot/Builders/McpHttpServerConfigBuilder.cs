using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class McpHttpServerConfigBuilder
    {
        private readonly McpHttpServerConfig _config = new();

        private readonly ILogger _logger;

        public McpHttpServerConfigBuilder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public McpHttpServerConfigBuilder WithUrl(string url)
        {
            LogSetting(nameof(_config.Url), url);
            _config.Url = url;
            return this;
        }

        public McpHttpServerConfigBuilder WithHeaders(IDictionary<string, string>? headers)
        {
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    _logger.Info("{TypeName} Setting '{PropertyName}' parameter entry '{Key}' to '{Value}'.", $"[{nameof(McpHttpServerConfigBuilder)}]", nameof(_config.Headers), kvp.Key, kvp.Value);
                }
            }

            _config.Headers = headers;
            return this;
        }

        public McpHttpServerConfigBuilder WithHeader(string key, string value)
        {
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter entry '{Key}' to '{Value}'.", $"[{nameof(McpHttpServerConfigBuilder)}]", nameof(_config.Headers), key, value);
            _config.Headers ??= new Dictionary<string, string>();
            _config.Headers[key] = value;
            return this;
        }

        public McpHttpServerConfigBuilder WithOauthClientId(string? oauthClientId)
        {
            LogSetting(nameof(_config.OauthClientId), oauthClientId ?? "null");
            _config.OauthClientId = oauthClientId;
            return this;
        }

        public McpHttpServerConfigBuilder WithOauthPublicClient(bool? oauthPublicClient)
        {
            LogSetting(nameof(_config.OauthPublicClient), oauthPublicClient?.ToString() ?? "null");
            _config.OauthPublicClient = oauthPublicClient;
            return this;
        }

        public McpHttpServerConfigBuilder WithOauthGrantType(McpHttpServerConfigOauthGrantType? oauthGrantType)
        {
            LogSetting(nameof(_config.OauthGrantType), oauthGrantType?.ToString() ?? "null");
            _config.OauthGrantType = oauthGrantType;
            return this;
        }

        public McpHttpServerConfigBuilder WithTools(params string[]? tools)
        {
            var toolList = tools?.ToList() ?? new List<string>();
            LogCollectionSetting(nameof(_config.Tools), toolList);
            _config.Tools = toolList;
            return this;
        }

        public McpHttpServerConfigBuilder WithTimeout(int? timeout)
        {
            LogSetting(nameof(_config.Timeout), timeout?.ToString() ?? "null");
            _config.Timeout = timeout;
            return this;
        }

        public McpHttpServerConfigBuilder WithConfig(Func<McpHttpServerConfig, McpHttpServerConfig> updateConfig)
        {
            ArgumentNullException.ThrowIfNull(updateConfig);

            _logger.Info("{TypeName} Applying custom {TypeName} update.", $"[{nameof(McpHttpServerConfigBuilder)}]");
            updateConfig.Invoke(_config);
            return this;
        }

        public McpHttpServerConfig Build()
        {
            _logger.Info("{TypeName} Building HTTP MCP server configuration (Url: {Url}).", $"[{nameof(McpHttpServerConfigBuilder)}]", _config.Url ?? "default");
            return _config;
        }

        private void LogSetting(string propertyName, object value)
        {
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '{Value}'.", $"[{nameof(McpHttpServerConfigBuilder)}]", propertyName, value);
        }

        private void LogCollectionSetting(string propertyName, IEnumerable<string> values)
        {
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '[{Value}]'.", $"[{nameof(McpHttpServerConfigBuilder)}]", propertyName, string.Join(", ", values));
        }
    }
}
