using System.Text.Json.Serialization;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiAgentConfigurationWithCustomMetadataModel : IAiAgentConfigurationWithCustomMetadata
    {
        [JsonPropertyName("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [YamlMember(Alias = "description")]
        public string? Description { get; set; }

        [JsonPropertyName("tools")]
        [YamlMember(Alias = "tools")]
        public IList<string>? Tools { get; set; }


        [JsonPropertyName("model")]
        [YamlMember(Alias = "model")]
        public string? Model { get; set; }

        [JsonPropertyName("custom-metadata")]
        [YamlMember(Alias = "custom-metadata")]
        public virtual IAiAgentConfigurationCustomMetadata? CustomMetadata { get; set; }
    }
}
