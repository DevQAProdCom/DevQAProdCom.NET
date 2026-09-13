using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using DevQAProdCom.NET.Global.Attributes;

namespace DevQAProdCom.NET.Global.Utils
{
    public class JsonUtils
    {
        public static void UpdateJsonValue(string filePath, string key, string newValue)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"The file at path {filePath} does not exist.");
                }

                var json = File.ReadAllText(filePath);
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement.Clone();
                var jsonObj = root.EnumerateObject().ToDictionary(p => p.Name, p => p.Value);
                jsonObj[key] = JsonDocument.Parse($"\"{newValue}\"").RootElement;
                var updatedJson = JsonSerializer.Serialize(jsonObj);
                File.WriteAllText(filePath, updatedJson);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Log the exception (logging method not implemented in this example)
                Console.WriteLine($"Access to the path '{filePath}' is denied. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception (logging method not implemented in this example)
                Console.WriteLine($"An error occurred while updating the JSON value: {ex.Message}");
                throw;
            }
        }

        public static void PopulateConfigurationProperties(object target, string sourceJsonModel)
        {
            if (string.IsNullOrWhiteSpace(sourceJsonModel))
            {
                return;
            }

            if (target == null)
            {
                throw new ArgumentNullException("Unable to populate configuration properties on a null target object.", nameof(target));
            }

            using var document = JsonDocument.Parse(sourceJsonModel);

            var properties = target.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<ConfigurationPropertyAttribute>() != null)
                .Where(p => p.GetCustomAttribute<ConfigurationPropertyIgnoreAttribute>() == null)
                .Where(p => p.CanWrite);

            foreach (var property in properties)
            {
                var jsonName = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? property.Name;

                if (!TryGetPropertyOrdinalIgnoreCase(document.RootElement, jsonName, out var element))
                {
                    continue;
                }

                var value = JsonSerializer.Deserialize(element, property.PropertyType);
                property.SetValue(target, value);
            }
        }

        public static bool TryGetPropertyOrdinalIgnoreCase(JsonElement element, string propertyName, out JsonElement value)
        {
            value = default;
            var foundCount = 0;
            JsonElement foundElement = default;

            foreach (var property in element.EnumerateObject())
            {
                if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    foundCount++;
                    foundElement = property.Value;
                }
            }

            if (foundCount > 1)
            {
                throw new InvalidOperationException($"Multiple JSON properties match '{propertyName}' using ordinal ignore case comparison.");
            }

            if (foundCount == 1)
            {
                value = foundElement;
                return true;
            }

            return false;
        }
    }
}
