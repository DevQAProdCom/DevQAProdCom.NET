using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotAiSkillConfiguration : IAiEntityIdentifiers
    {
        public IList<string>? AllowedTools { get; set; }
    }
}
