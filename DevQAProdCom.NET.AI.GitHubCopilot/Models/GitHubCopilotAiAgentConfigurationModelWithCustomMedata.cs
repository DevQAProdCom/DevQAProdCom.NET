using System.Text.Json.Serialization;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.Models;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiAgentConfigurationModelWithCustomMedata : AiAgentConfigurationWithCustomMetadataModel, IGitHubCopilotAiAgentConfigurationWithCustomMetadata
    {
        [JsonPropertyName("custom-metadata")]
        [YamlMember(Alias = "custom-metadata")]
        public override IAiAgentConfigurationCustomMetadata? CustomMetadata { get; set; } = new GitHubCopilotAiAgentConfigurationCustomMetadataModel();
    }
}
