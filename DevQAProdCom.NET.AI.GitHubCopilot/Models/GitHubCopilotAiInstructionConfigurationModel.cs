using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiInstructionConfigurationModel : BaseAiEntityConfigurationModel, IGitHubCopilotAiInstructionConfiguration
    {
        [YamlMember(Alias = "applyTo")]
        public List<string>? ApplyTo { get; set; }
    }
}
