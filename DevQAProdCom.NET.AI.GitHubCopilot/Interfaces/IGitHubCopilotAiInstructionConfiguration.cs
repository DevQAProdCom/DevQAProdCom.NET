using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotAiInstructionConfiguration : IAiEntityIdentifiers
    {
        public List<string>? ApplyTo { get; set; }
    }
}
