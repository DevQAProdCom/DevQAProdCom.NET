using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class BaseAiAgentYamlConfigurationModel
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }

        [YamlMember(Alias = "tools")]
        public IList<string>? Tools { get; set; }

        [YamlMember(Alias = "skills")]
        public IList<string>? Skills { get; set; }

        [YamlMember(Alias = "model")]
        public string? Model { get; set; }
    }
}
