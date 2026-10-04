using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotMcpServersMappers
    {
        McpServerConfig ToMcpServerConfig(IFileBasedMcpServer fileBasedMcpServer);
    }
}
