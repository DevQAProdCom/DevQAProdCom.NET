using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiSkillConfigurationModel : BaseAiEntityConfigurationModel, IGitHubCopilotAiSkillConfiguration
    {
        [YamlMember(Alias = "allowed-tools")]
        public IList<string>? AllowedTools { get; set; }
    }
}
