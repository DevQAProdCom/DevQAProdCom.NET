using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.Models;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiAgentConfigurationModel : BaseAiAgentConfigurationModel, IGitHubCopilotAiAgentConfigurationWithIdentifiers
    {
        [YamlMember(Alias = "custom-metadata")]
        public IAiAgentConfigurationCustomMetadataModel? CustomMetadata { get; set; } = new GitHubCopilotAiAgentConfigurationCustomMetadataModel();
    }
}
