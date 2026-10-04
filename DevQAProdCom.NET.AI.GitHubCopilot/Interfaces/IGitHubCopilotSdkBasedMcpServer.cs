using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotSdkBasedMcpServer : IHaveStringIdentifier, IHaveDescription
    {
        public void AddTo(IDictionary<string, McpServerConfig>? mcpServerConfigs);
        public string ToJson();
    }
}
