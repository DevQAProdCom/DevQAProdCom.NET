using System.Text.Json;
using System.Text.Json.Nodes;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public class GitHubCopilotFileBasedHooksSearcher : IFileBasedHooksSearcher
    {
        private readonly ILogger _logger;

        public GitHubCopilotFileBasedHooksSearcher(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual List<IFileBasedHook> Search(string path, bool useExtendedSearch = true)
        {
            var hooks = new List<IFileBasedHook>();

            if (IoUtils.FileExists(path))
            {
                var hooksFromFile = SearchInFile(path);
                hooks.AddRange(hooksFromFile);
                return hooks;
            }

            if (IoUtils.DirectoryExists(path))
            {
                var files = IoUtils.GetJsonFilesInDirectory(path);

                foreach (var file in files)
                {
                    hooks.AddRange(SearchInFile(file));
                }

                return hooks;
            }

            return hooks;
        }

        private List<IFileBasedHook> SearchInFile(string filePath)
        {
            var hooks = new List<IFileBasedHook>();

            try
            {
                var json = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return hooks;
                }

                using var document = JsonDocument.Parse(json);
                var root = document.RootElement;

                if (!root.TryGetProperty("hooks", out var hooksElement) || hooksElement.ValueKind != JsonValueKind.Object)
                {
                    return hooks;
                }

                foreach (var hookGroup in hooksElement.EnumerateObject())
                {
                    var eventTriggerName = hookGroup.Name;

                    if (hookGroup.Value.ValueKind != JsonValueKind.Array)
                    {
                        continue;
                    }

                    foreach (var hookEntry in hookGroup.Value.EnumerateArray())
                    {
                        var hook = CreateHookModel(hookEntry, eventTriggerName, filePath);
                        if (hook != null)
                        {
                            hooks.Add(hook);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.Error("[{TypeName}] Error processing hooks file '{FilePath}': {ErrorMessage}", nameof(GitHubCopilotFileBasedHooksSearcher), filePath, exception.Message);
            }

            return hooks;
        }

        private IFileBasedHook? CreateHookModel(JsonElement hookEntry, string eventTriggerName, string filePath)
        {
            var hookModel = new HookModel
            {
                EventTriggerName = eventTriggerName,
                FilePath = filePath
            };

            string? identifier = null;
            string? description = null;
            List<IDirectoryFilesData>? data = null;

            var hookObject = JsonNode.Parse(hookEntry.GetRawText())!.AsObject();

            if (hookObject.TryGetPropertyValue("custom-metadata", out var customMetadataNode) && customMetadataNode is JsonObject customMetadataObject)
            {
                if (customMetadataObject.TryGetPropertyValue(nameof(HookModel.Identifier).ToLower(), out var identifierNode))
                {
                    identifier = identifierNode?.GetValue<string?>();
                }

                if (customMetadataObject.TryGetPropertyValue(nameof(HookModel.Description).ToLower(), out var descriptionNode))
                {
                    description = descriptionNode?.GetValue<string?>();
                }

                if (customMetadataObject.TryGetPropertyValue("data", out var dataNode))
                {
                    try
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        data = dataNode?.Deserialize<List<DirectoryFilesDataModel>>(options)?.Cast<IDirectoryFilesData>().ToList();
                    }
                    catch (Exception exception)
                    {
                        _logger.Error("[{TypeName}] Error deserializing hook data in hooks file '{FilePath}': {ErrorMessage}", nameof(GitHubCopilotFileBasedHooksSearcher), filePath, exception.Message);
                    }
                }

                hookObject.Remove("custom-metadata");
            }

            hookModel.Identifier = identifier;
            hookModel.Description = description;
            hookModel.Data = data;
            hookModel.ContentValue = hookObject.ToJsonString();

            return hookModel;
        }
    }
}
