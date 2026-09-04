using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;

namespace DevQAProdCom.NET.AI.GitHubCopilot.McpServers
{
    public abstract class GitHubCopilotMcpServerConfig : IMcpServer
    {
        public abstract string? Class { get; set; }
        public string Type { get; set; }
        public abstract string Identifier { get; set; }
        public IList<string>? Tools { get; set; }
        public abstract string ToJson();
    }
}
