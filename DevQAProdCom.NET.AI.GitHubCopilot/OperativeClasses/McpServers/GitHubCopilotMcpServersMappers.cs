using System.Text.Json;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers
{
    public class GitHubCopilotMcpServersMappers : IGitHubCopilotMcpServersMappers
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

        private static McpServerConfig CreateMcpServerConfig(string type, IFileBasedMcpServer fileBasedMcpServer)
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
