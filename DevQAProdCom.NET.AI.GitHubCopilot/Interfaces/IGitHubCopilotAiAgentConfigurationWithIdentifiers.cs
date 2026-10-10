using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotAiAgentConfigurationWithIdentifiers : IAiEntityIdentifiers, IAiAgentConfigurationStandardDataWithoutAiEntityIdentifiers
    {
        [YamlMember(Alias = "custom-metadata")]
        public IAiAgentConfigurationCustomMetadataModel? CustomMetadata { get; set; }
    }
}
