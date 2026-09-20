using System.Text.Json;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations.Files;
using CopilotIoUtils = DevQAProdCom.NET.AI.GitHubCopilot.Utils.IoUtils;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses
{
    public class GitHubCopilotHookSearcher : IHookSearcher
    {
        public List<string>? Locations { get; set; }

        public GitHubCopilotHookSearcher(List<string> locations)
        {
            Locations = locations ?? new List<string>();
        }

        public virtual List<IHook> SearchInDefaultLocations(bool useExtendedSearch = true)
        {
            var hooks = new List<IHook>();

            if (Locations == null || Locations.Count == 0)
            {
                var defaultLocations = CopilotIoUtils.GetCopilotHooks(Directory.GetCurrentDirectory(), useExtendedSearch);
                Locations = new List<string>(defaultLocations);
            }

            foreach (var location in Locations)
            {
                hooks.AddRange(Search(location, useExtendedSearch));
            }

            return hooks;
        }

        public virtual List<IHook> Search(string path, bool useExtendedSearch = true)
        {
            var hooks = new List<IHook>();

            if (GlobalIoUtils.FileExists(path))
            {
                hooks.AddRange(SearchInFile(path));
                return hooks;
            }

            if (GlobalIoUtils.DirectoryExists(path))
            {
                var files = GlobalIoUtils.GetFilesInDirectory(path, $"*{FileExtension.Json.GetDescriptionAttributeValue()}", SearchOption.TopDirectoryOnly);

                foreach (var file in files)
                {
                    hooks.AddRange(SearchInFile(file.FullName));
                }

                return hooks;
            }

            return hooks;
        }

        private List<IHook> SearchInFile(string filePath)
        {
            var hooks = new List<IHook>();

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
            catch (JsonException)
            {
            }
            catch (IOException)
            {
            }

            return hooks;
        }

        private IHook? CreateHookModel(JsonElement hookEntry, string eventTriggerName, string filePath)
        {
            var hookModel = new HookModel
            {
                EventTriggerName = eventTriggerName,
                FilePath = filePath
            };

            var customMetadata = new Dictionary<string, string>();
            var hookProperties = new Dictionary<string, JsonElement>();

            foreach (var property in hookEntry.EnumerateObject())
            {
                if (property.NameEquals("custom-metadata"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Object)
                    {
                        foreach (var metadataProperty in property.Value.EnumerateObject())
                        {
                            customMetadata[metadataProperty.Name] = metadataProperty.Value.ValueKind == JsonValueKind.String
                                ? metadataProperty.Value.GetString() ?? metadataProperty.Value.ToString()
                                : metadataProperty.Value.ToString();
                        }
                    }
                }
                else
                {
                    hookProperties[property.Name] = property.Value;
                }
            }

            if (customMetadata.TryGetValue("identifier", out var identifier) && !string.IsNullOrEmpty(identifier))
            {
                hookModel.Identifier = identifier;
            }
            else
            {
                hookModel.Identifier = $"{eventTriggerName}.{hookModel.NameFromFile ?? Guid.NewGuid().ToString()}";
            }

            if (customMetadata.TryGetValue("description", out var description) && !string.IsNullOrEmpty(description))
            {
                hookModel.Description = description;
            }

            hookModel.Hook = SerializeHookProperties(hookProperties);

            return hookModel;
        }

        private static List<string> ExtractStringValues(JsonElement element)
        {
            var values = new List<string>();

            if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    values.Add(item.ToString());
                }
            }
            else
            {
                values.Add(element.ToString());
            }

            return values;
        }

        private static string SerializeHookProperties(Dictionary<string, JsonElement> hookProperties)
        {
            if (hookProperties.Count == 0)
            {
                return string.Empty;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var dictionary = hookProperties.ToDictionary(p => p.Key, p => JsonSerializer.Deserialize<object>(p.Value.GetRawText()));
            return JsonSerializer.Serialize(dictionary, options);
        }
    }
}
