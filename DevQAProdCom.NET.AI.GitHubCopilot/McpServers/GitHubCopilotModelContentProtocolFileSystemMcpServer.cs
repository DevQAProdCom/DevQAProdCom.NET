//using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
//using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
//using GitHub.Copilot;

//namespace DevQAProdCom.NET.AI.GitHubCopilot.McpServers
//{
//    /// <summary>
//    /// https://mcpservers.org/servers/modelcontextprotocol/filesystem
//    /// </summary>
//    public class GitHubCopilotModelContentProtocolFileSystemMcpServer : GitHubCopilotMcpServerConfig
//    {
//        public override string? Class { get; set; } = typeof(GitHubCopilotModelContentProtocolFileSystemMcpServer).FullName;
//        public override string Identifier { get; set; } = "filesystem";
//        public List<string> FilePaths { get; set; }
//        private McpStdioServerConfig McpServerConfig2 { get; set; }

//        public GitHubCopilotModelContentProtocolFileSystemMcpServer(ILogger logger, params string[] filePaths)
//        { 
//            FilePaths = filePaths.ToList();

//            McpServerConfig2 = new McpStdioServerConfigBuilder(logger)
//                .WithCommand("npx")
//                .WithArgs(new List<string> { "-y", "@modelcontextprotocol/server-filesystem" }.Concat(filePaths).ToArray())
//                .Build();
//        }


//    }
//}
