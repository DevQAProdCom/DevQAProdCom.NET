using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;


namespace DevQAProdCom.NET.AI.GitHubCopilot.McpServers
{
    public class GitHubCopilotPlaywrightMcpServer : McpServer<McpStdioServerConfig>
    {
        public override string? Class { get; set; } = typeof(GitHubCopilotPlaywrightMcpServer).FullName;
        public override string Identifier { get; set; } = "playwright";

        public GitHubCopilotPlaywrightMcpServer(ILogger logger)
        {
            McpServer = new McpStdioServerConfigBuilder(logger)
                .WithCommand("npx")
                .WithArgs(new string[] { "@playwright/mcp@latest" })
                .Build();
        }

        public override void WithConfigurationFromJson(string configuration)
        {

        }

        public override void WithConfigurationFromYaml(string configuration)
        {

        }
    }
}
