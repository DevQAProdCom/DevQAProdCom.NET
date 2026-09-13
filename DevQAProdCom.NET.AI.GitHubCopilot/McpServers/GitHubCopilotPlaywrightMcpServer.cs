using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.Shared.Constants;
using DevQAProdCom.NET.Global.Attributes;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.McpServers
{
    public class GitHubCopilotPlaywrightMcpServer : GitHubCopilotStdioMcpServer
    {
        public override string? Class { get; } = typeof(GitHubCopilotPlaywrightMcpServer).FullName;

        [ConfigurationPropertyIgnore]
        public override string Identifier { get; set; } = SharedAiConstants.McpServers.Identifiers.PLAYWRIGHT;

        public GitHubCopilotPlaywrightMcpServer(ILogger logger)
        {
            McpServerConfiguration = new McpStdioServerConfigBuilder(logger)
                .WithCommand("npx")
                .WithArgs(new string[] { "@playwright/mcp@latest" })
                .WithTools("*")
                .Build();
        }
    }
}
