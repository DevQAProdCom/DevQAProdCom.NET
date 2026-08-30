using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiSkillYamlConfigurationModel : BaseAiEntityYamlConfigurationModel, IGitHubCopilotAiSkillYamlConfiguration
    {
        [YamlMember(Alias = "allowed-tools")]
        public IList<string>? AllowedTools { get; set; }
    }
}
