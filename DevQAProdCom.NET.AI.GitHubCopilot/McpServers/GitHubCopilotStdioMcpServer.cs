using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.McpServers
{
    public abstract class GitHubCopilotStdioMcpServer : McpServer<McpStdioServerConfig>
    {
        //public override string? Class { get; } = typeof(GitHubCopilotStdioMcpServer).FullName;
        //public override string Identifier { get; set; } = "stdio";

        public GitHubCopilotStdioMcpServer() : base() { }

        //[JsonPropertyName("args")]
        //public List<string> Args { get; set; } = new List<string>();

        //public void WithArgs(string[] args)
        //{
        //  
        //    McpServerConfiguration ??= new();

        //    if (McpServerConfiguration != null)
        //    {
        //        McpServerConfiguration.Args.AddRange(args);
        //    }
        //}
    }
}
