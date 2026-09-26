using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAiAgentCustomMetadataConfigurationModel
    {
        [YamlMember(Alias = "permissions")]
        public List<string>? Permissions { get; set; }

        [YamlMember(Alias = "instructions")]
        public List<string>? Instructions { get; set; }

        [YamlMember(Alias = "skills")]
        public List<string>? Skills { get; set; }

        [YamlMember(Alias = "subagents")]
        public List<string>? Subagents { get; set; }
    }
}
