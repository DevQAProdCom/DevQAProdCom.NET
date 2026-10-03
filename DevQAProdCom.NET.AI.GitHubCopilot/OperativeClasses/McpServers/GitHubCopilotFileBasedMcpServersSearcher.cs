using System.Text.Json;
using DevQAProdCom.NET.AI.Shared.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using CopilotIoUtils = DevQAProdCom.NET.AI.GitHubCopilot.Utils.IoUtils;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers
{
    public class GitHubCopilotFileBasedMcpServersSearcher : IFileBasedMcpServersSearcher
    {
        private readonly ILogger _logger;

        public GitHubCopilotFileBasedMcpServersSearcher(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual List<IFileBasedMcpServer> Search(string path, bool useExtendedSearch = true)
        {
            var mcpServers = new List<IFileBasedMcpServer>();

            //If path is a file, search in that file
            if (GlobalIoUtils.FileExists(path))
            {
                mcpServers.AddRange(SearchInFile(path));
                return mcpServers;
            }

            //If path is a directory, search in all files in that directory
            if (GlobalIoUtils.DirectoryExists(path))
            {
                var files = CopilotIoUtils.GetFilesWithCopilotMcpServers(path, useExtendedSearch);

                foreach (var file in files)
                {
                    mcpServers.AddRange(SearchInFile(file));
                }

                return mcpServers;
            }

            return mcpServers;
        }

        private List<IFileBasedMcpServer> SearchInFile(string filePath)
        {
            var mcpServers = new List<IFileBasedMcpServer>();

            try
            {
                var json = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return mcpServers;
                }

                var options = new JsonDocumentOptions
                {
                    AllowTrailingCommas = true,
                    CommentHandling = JsonCommentHandling.Skip
                };

                using var document = JsonDocument.Parse(json, options);
                var root = document.RootElement;

                if (root.ValueKind != JsonValueKind.Object)
                {
                    return mcpServers;
                }

                JsonElement serversElement;

                if (root.TryGetProperty("mcpServers", out var mcpServersElement) && mcpServersElement.ValueKind == JsonValueKind.Object)
                {
                    serversElement = mcpServersElement;
                }
                else
                {
                    serversElement = root;
                }

                foreach (var serverProperty in serversElement.EnumerateObject())
                {
                    if (serverProperty.Value.ValueKind != JsonValueKind.Object)
                    {
                        continue;
                    }

                    var identifier = serverProperty.Name;
                    var contentValue = serverProperty.Value.GetRawText();

                    string? description = null;
                    if (serverProperty.Value.TryGetProperty(SharedAiConstants.JsonElementNames.CustomMetadata, out var customMetadataElement) && customMetadataElement.ValueKind == JsonValueKind.Object &&
                        customMetadataElement.TryGetProperty(SharedAiConstants.JsonElementNames.Description, out var descriptionElement) && descriptionElement.ValueKind == JsonValueKind.String)
                    {
                        description = descriptionElement.GetString();
                    }

                    mcpServers.Add(new FileBasedMcpServerModel
                    {
                        Identifier = identifier,
                        Description = description,
                        FilePath = filePath,
                        ContentValue = contentValue
                    });
                }
            }
            catch (Exception exception)
            {
                _logger.Error("[{TypeName}] Error processing MCP servers file '{FilePath}': {ErrorMessage}", nameof(GitHubCopilotFileBasedMcpServersSearcher), filePath, exception.Message);
            }

            return mcpServers;
        }
    }
}
