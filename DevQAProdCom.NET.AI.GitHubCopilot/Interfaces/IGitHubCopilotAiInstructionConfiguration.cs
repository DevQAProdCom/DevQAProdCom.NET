using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotAiInstructionConfiguration : IAiEntityConfiguration
    {
        public List<string>? ApplyTo { get; set; }
    }
}
