using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.Models;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiAgentConfigurationModelWithCustomMedata : AiAgentConfigurationWithCustomMetadataModel
    {
        [YamlMember(Alias = "custom-metadata")]
        public new IAiAgentConfigurationCustomMetadata? CustomMetadata { get; set; } = new GitHubCopilotAiAgentConfigurationCustomMetadataModel();
    }
}
