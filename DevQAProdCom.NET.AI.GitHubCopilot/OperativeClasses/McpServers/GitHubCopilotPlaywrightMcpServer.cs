using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.Shared.Constants;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers
{
    public class GitHubCopilotPlaywrightMcpServer : GitHubCopilotSdkBasedStdioMcpServer
    {
        public override string? Identifier { get; set; } = SharedAiConstants.McpServers.Identifiers.PLAYWRIGHT;

        public GitHubCopilotPlaywrightMcpServer(ILogger logger)
        {
            McpServerConfiguration = new McpStdioServerConfigBuilder(logger)
                .WithCommand("npx")
                .WithArgs("@playwright/mcp@latest")
                .WithTools("*")
                .Build();
        }
    }
}
