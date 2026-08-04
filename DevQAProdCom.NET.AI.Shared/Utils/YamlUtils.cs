using DevQAProdCom.NET.AI.Shared.Interfaces;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Utils
{
    public static class YamlUtils
    {
        public static TEntity SplitEntityDataAndYamlMetaData<TEntity, TYamlConfig>(string filePath)
            where TEntity : IAiEntityWithTYamlConfigurationType<TYamlConfig>, new()
            where TYamlConfig : IBaseAiAgentYamlConfiguration, new()
        {
            string? fileContent = null;

            try
            {
                fileContent = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading file '{filePath}': {ex.Message}");
            }

            var yamlDelimiter = "---";
            if (fileContent.StartsWith(yamlDelimiter))
            {
                int firstIdx = fileContent.IndexOf(yamlDelimiter);
                int startOfSearch = firstIdx + yamlDelimiter.Length;
                int secondIdx = fileContent.IndexOf(yamlDelimiter, startOfSearch);

                if (secondIdx != -1)
                {
                    string yamlHeader = fileContent.Substring(startOfSearch, secondIdx - startOfSearch).Trim();
                    string propmt = fileContent.Substring(secondIdx + yamlDelimiter.Length).Trim();

                    var deserializer = new DeserializerBuilder()
                        .IgnoreUnmatchedProperties()
                        .Build();

                    try
                    {
                        var entity = new TEntity();

                        entity.ConfigurationData = deserializer.Deserialize<TYamlConfig>(yamlHeader);
                        entity.Prompt = propmt;
                        return entity;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error deserializing YAML header in file '{filePath}': {ex.Message}");
                    }
                }
            }

            return new TEntity { Prompt = fileContent };
        }
    }
}
