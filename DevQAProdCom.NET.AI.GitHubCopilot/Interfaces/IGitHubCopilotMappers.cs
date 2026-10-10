using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotMappers
    {
        CustomAgentConfig ToCustomAgentConfig(IAiEntityWithTConfigurationType<GitHubCopilotAiAgentConfigurationModel> aiAgent,
           IFileBasedMcpServersCollection fileBasedMcpServersCollection,
           IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer> sdkBasedMcpServersCollection);

        McpServerConfig ToMcpServerConfig(IFileBasedMcpServer fileBasedMcpServer);
    }
}
