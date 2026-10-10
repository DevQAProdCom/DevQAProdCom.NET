using DevQAProdCom.NET.AI.Shared.Interfaces;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class BaseAiEntityConfigurationModel : IAiEntityIdentifiers
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }
    }
}
