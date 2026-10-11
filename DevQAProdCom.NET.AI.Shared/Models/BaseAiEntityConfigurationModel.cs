using System.Text.Json.Serialization;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class BaseAiEntityConfigurationModel : IAiEntityIdentifiers
    {
        [JsonPropertyName("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [YamlMember(Alias = "description")]
        public string? Description { get; set; }
    }
}
