using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers
{
    public class GitHubCopilotFilesystemMcpServer : GitHubCopilotSdkBasedStdioMcpServer
    {
        public override string? Identifier { get; set; } = "filesystem-sdk-based-mcp-server";

        public GitHubCopilotFilesystemMcpServer(ILogger logger, IEnumerable<string> allowedDirectories)
        {
            var args = new List<string> { "-y", "@modelcontextprotocol/server-filesystem" };
            args.AddRange(allowedDirectories);

            McpServerConfiguration = new McpStdioServerConfigBuilder(logger)
                .WithCommand("npx")
                .WithArgs(args.ToArray())
                .WithTools("*")
                .Build();
        }
    }
}
