using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class BaseAiEntityYamlConfigurationModel
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }
    }
}
