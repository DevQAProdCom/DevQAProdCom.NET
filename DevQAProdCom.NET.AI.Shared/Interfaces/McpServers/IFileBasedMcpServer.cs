using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Interfaces.McpServers
{
    public interface IFileBasedMcpServer : IHaveStringIdentifier, IHaveDescription
    {
        public string? FilePath { get; set; }
        public string? ContentValue { get; set; }
    }
}
