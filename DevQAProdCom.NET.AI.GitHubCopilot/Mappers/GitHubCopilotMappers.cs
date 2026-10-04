using System.Text.Json;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Mappers
{
    public class GitHubCopilotMappers(ILogger logger): IGitHubCopilotMappers
    {
        public McpServerConfig ToMcpServerConfig(IFileBasedMcpServer fileBasedMcpServer)
        {
            ArgumentNullException.ThrowIfNull(fileBasedMcpServer);

            if (string.IsNullOrWhiteSpace(fileBasedMcpServer.ContentValue))
            {
                throw new ArgumentException($"{nameof(IFileBasedMcpServer.ContentValue)} of file-based MCP server '{fileBasedMcpServer.Identifier}' is null or empty.", nameof(fileBasedMcpServer));
            }

            string type;

            try
            {
                using var document = JsonDocument.Parse(fileBasedMcpServer.ContentValue);
                var root = document.RootElement;

                if (!root.TryGetProperty("type", out var typeElement) || typeElement.ValueKind != JsonValueKind.String)
                {
                    throw new InvalidOperationException($"File-based MCP server '{fileBasedMcpServer.Identifier}' does not have a valid 'type' property.");
                }

                type = typeElement.GetString()!;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to parse file-based MCP server '{fileBasedMcpServer.Identifier}' configuration.", ex);
            }

            try
            {
                return CreateMcpServerConfig(type, fileBasedMcpServer);
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to map file-based MCP server '{fileBasedMcpServer.Identifier}' of type '{type}' to configuration.", ex);
            }
        }

        public CustomAgentConfig ToCustomAgentConfig(IAiEntityWithTYamlConfigurationType<GitHubCopilotAiAgentYamlConfigurationModel> aiAgent,
            IFileBasedMcpServersCollection fileBasedMcpServersCollection,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer> sdkBasedMcpServersCollection)
        {
            var config = new CustomAgentConfig();

            config.Name = aiAgent.ConfigurationData.Name;
            config.DisplayName = aiAgent.ConfigurationData.Name; //TODO Add Custom YAML Attribute for DisplayName
            config.Description = aiAgent.ConfigurationData.Description;
            config.Prompt = aiAgent.Prompt;
            config.Tools = aiAgent.ConfigurationData.Tools;
            config.Skills = aiAgent.ConfigurationData.CustomMetadata?.Skills;
            config.Model = aiAgent.ConfigurationData.Model;

            if (aiAgent.ConfigurationData?.CustomMetadata?.McpServers?.Count() > 0)
            {
                foreach (var mcpServer in aiAgent.ConfigurationData.CustomMetadata.McpServers)
                {
                    var isSdkBasedMcpServerWithIdentifierExists = sdkBasedMcpServersCollection.TryGetByIdentifier(mcpServer, out var sdkBasedMcpServer);
                    var isFileBasedMcpServerWithIdentifierExists = fileBasedMcpServersCollection.TryGetByIdentifier(mcpServer, out var fileBasedMcpServer);

                    if (!isSdkBasedMcpServerWithIdentifierExists && !isFileBasedMcpServerWithIdentifierExists)
                    {
                        throw new InvalidOperationException($"MCP server '{mcpServer}' specified in agent '{aiAgent.ConfigurationData.Name}' not found in either SDK-based or file-based collections.");
                    }

                    if (isSdkBasedMcpServerWithIdentifierExists && isFileBasedMcpServerWithIdentifierExists)
                    {
                        throw new InvalidOperationException($"MCP server '{mcpServer}' specified in agent '{aiAgent.ConfigurationData.Name}' found in both SDK-based and file-based collections. MCP server collection should not contain duplicated identifiers.");
                    }

                    config.McpServers ??= new Dictionary<string, McpServerConfig>();

                    if (isSdkBasedMcpServerWithIdentifierExists)
                    {
                        sdkBasedMcpServer.AddTo(config.McpServers);
                    }

                    if (isFileBasedMcpServerWithIdentifierExists)
                    {
                        var mcpServerConfig = ToMcpServerConfig(fileBasedMcpServer);
                        config.McpServers.Add(fileBasedMcpServer.Identifier, mcpServerConfig);
                    }
                }
            }

            config.Infer = true;

            return config;
        }

        private McpServerConfig CreateMcpServerConfig(string type, IFileBasedMcpServer fileBasedMcpServer)
        {
            if (string.IsNullOrEmpty(fileBasedMcpServer.ContentValue))
            {
                throw new ArgumentException($"{nameof(IFileBasedMcpServer.ContentValue)} of file-based MCP server '{fileBasedMcpServer.Identifier}' is null or empty.", nameof(fileBasedMcpServer));
            }
            else if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException($"Type of file-based MCP server '{fileBasedMcpServer.Identifier}' is null or empty.", nameof(type));
            }

            return type.ToLowerInvariant() switch
            {
                "stdio" or "local" => fileBasedMcpServer.ContentValue.FromJson<McpStdioServerConfig>()!,
                "http" or "sse" => fileBasedMcpServer.ContentValue.FromJson<McpHttpServerConfig>()!,
                _ => throw new NotSupportedException($"MCP server type '{type}' is not supported for file-based MCP server '{fileBasedMcpServer.Identifier}'. Supported types are: stdio, local, http, sse.")
            };
        }
    }
}
