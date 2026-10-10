using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotAiSkillConfiguration : IAiEntityConfiguration
    {
        public IList<string>? AllowedTools { get; set; }
    }
}
