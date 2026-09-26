using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiAgentYamlConfigurationModel : BaseAiAgentYamlConfigurationModel, IGitHubCopilotAiAgentYamlConfiguration
    {
        [YamlMember(Alias = "custom-metadata")]
        public GitHubCopilotAiAgentCustomMetadataConfigurationModel? CustomMetadata { get; set; } = new();
    }
}
