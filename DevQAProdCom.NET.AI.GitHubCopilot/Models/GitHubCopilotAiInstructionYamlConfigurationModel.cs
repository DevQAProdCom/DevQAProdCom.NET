using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiInstructionYamlConfigurationModel : BaseAiEntityYamlConfigurationModel, IGitHubCopilotAiInstructionYamlConfiguration
    {
        [YamlMember(Alias = "applyTo")]
        public List<string>? ApplyTo { get; set; }
    }
}
