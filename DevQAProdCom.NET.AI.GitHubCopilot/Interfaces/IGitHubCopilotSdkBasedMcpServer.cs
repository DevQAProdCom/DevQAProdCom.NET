using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotSdkBasedMcpServer : IHaveStringIdentifier, IHaveDescription
    {
        public void ApplyTo(List<McpServerConfig> mcpServerConfigs);
        public string ToJson();

    }
}
