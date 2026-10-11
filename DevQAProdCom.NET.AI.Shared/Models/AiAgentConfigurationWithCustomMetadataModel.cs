using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiAgentConfigurationWithCustomMetadataModel: IAiAgentConfigurationWithCustomMetadata
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }

        [YamlMember(Alias = "tools")]
        public IList<string>? Tools { get; set; }
        
        [YamlMember(Alias = "model")]
        public string? Model { get; set; }

        [YamlMember(Alias = "custom-metadata")]
        public IAiAgentConfigurationCustomMetadata? CustomMetadata { get; set; }
    }
}
