using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiAgentYamlConfigurationModel : BaseAiAgentYamlConfigurationModel, IGitHubCopilotAiAgentYamlConfiguration
    {
        [YamlMember(Alias = "custom-permissions")]
        public List<string>? CustomPermissions { get; set; }

        [YamlMember(Alias = "custom-instructions")]
        public List<string>? CustomInstructions { get; set; }

        [YamlMember(Alias = "custom-skills")]
        public List<string>? CustomSkills { get; set; }

        [YamlMember(Alias = "custom-subagents")]
        public List<string>? CustomSubagents { get; set; }
    }
}
