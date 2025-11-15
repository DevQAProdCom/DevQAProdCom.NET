using System.Text.Json;
using Microsoft.Extensions.Configuration.Json;

namespace DevQAProdCom.NET.Configurations.OperativeClasses
{
    public class CustomJsonConfigurationProvider : JsonConfigurationProvider
    {
        public CustomJsonConfigurationProvider(JsonConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            using var jsonDoc = JsonDocument.Parse(stream);
            var root = jsonDoc.RootElement;

            LoadJsonElement(root, parentPath: null);
        }

        private void LoadJsonElement(JsonElement element, string? parentPath)
        {
            foreach (var property in element.EnumerateObject())
            {
                var key = parentPath == null ? property.Name : $"{parentPath}:{property.Name}";

                if (property.Value.ValueKind == JsonValueKind.Null)
                {
                    Data[key] = null;
                }
                else if (property.Value.ValueKind == JsonValueKind.Object)
                {
                    LoadJsonElement(property.Value, key);
                }
                else if (property.Value.ValueKind == JsonValueKind.Array)
                {
                    if (property.Value.GetArrayLength() == 0)
                    {
                        Data[$"{key}:-1"] = "[]"; // Handle empty array
                    }
                    else
                    {
                        int index = 0;
                        foreach (var arrayElement in property.Value.EnumerateArray())
                        {
                            if (arrayElement.ValueKind == JsonValueKind.Object)
                            {
                                LoadJsonElement(arrayElement, $"{key}:{index}");
                            }
                            else
                            {
                                Data[$"{key}:{index}"] = arrayElement.ToString();
                            }
                            index++;
                        }
                    }
                }
                else
                {
                    Data[key] = property.Value.ToString();
                }
            }
        }
    }
}
