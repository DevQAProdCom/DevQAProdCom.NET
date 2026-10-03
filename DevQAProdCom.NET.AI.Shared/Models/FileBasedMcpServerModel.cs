using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class FileBasedMcpServerModel : IFileBasedMcpServer
    {
        public string? Identifier { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public string? ContentValue { get; set; }
    }
}
