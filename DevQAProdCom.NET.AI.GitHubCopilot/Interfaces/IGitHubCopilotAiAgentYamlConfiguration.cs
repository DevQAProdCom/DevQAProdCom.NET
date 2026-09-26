using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotAiAgentYamlConfiguration : IBaseAiAgentYamlConfiguration
    {
        [YamlMember(Alias = "custom-metadata")]
        public GitHubCopilotAiAgentCustomMetadataConfigurationModel? CustomMetadata { get; set; }
    }
}
