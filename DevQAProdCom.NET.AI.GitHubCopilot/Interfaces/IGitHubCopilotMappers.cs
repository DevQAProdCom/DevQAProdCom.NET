using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotMappers
    {
        McpServerConfig ToMcpServerConfig(IFileBasedMcpServer fileBasedMcpServer);

    }
}
