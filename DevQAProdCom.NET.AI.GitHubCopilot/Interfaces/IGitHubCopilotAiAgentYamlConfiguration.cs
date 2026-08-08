using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotAiAgentYamlConfiguration : IBaseAiAgentYamlConfiguration
    {
        public List<string>? CustomPermissions { get; set; }
    }
}
