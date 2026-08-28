using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotAiInstructionYamlConfiguration : IAiEntityYamlConfiguration
    {
        public List<string>? ApplyTo { get; set; }
    }
}
