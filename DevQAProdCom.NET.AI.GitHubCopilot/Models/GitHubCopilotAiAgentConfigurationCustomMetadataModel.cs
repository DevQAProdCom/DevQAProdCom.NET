using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiAgentConfigurationCustomMetadataModel: IAiAgentConfigurationCustomMetadataModel
    {
        [YamlMember(Alias = "configurations")]
        public List<IAiAgentConfigurationWithoutAiEntityIdentifiers>? Configurations { get; set; }
    }
}
