using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers
{
    public abstract class GitHubCopilotSdkBasedMcpServer<T> : IGitHubCopilotSdkBasedMcpServer
    {
        public void ApplyTo(List<McpServerConfig> mcpServerConfigs)
        {
        }

        public string ToJson()
        {
        }
    }
}
