using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class BaseAiEntityConfigurationModel
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }
    }
}
