using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class BaseAiAgentConfigurationModel: BaseAiEntityConfigurationModel, IAiAgentConfigurationStandardDataWithoutAiEntityIdentifiers
    {
        [YamlMember(Alias = "tools")]
        public IList<string>? Tools { get; set; }

        [YamlMember(Alias = "model")]
        public string? Model { get; set; }
    }
}
