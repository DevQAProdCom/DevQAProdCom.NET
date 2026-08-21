using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiInstructionYamlConfigurationModel : IGitHubCopilotAiInstructionYamlConfiguration
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; } = string.Empty;

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }
    }
}
