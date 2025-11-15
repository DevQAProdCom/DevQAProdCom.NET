using System.Text.Json;

namespace DevQAProdCom.NET.Global.Helpers
{
    public class JsonHelper
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
    }
}
