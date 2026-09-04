namespace DevQAProdCom.NET.AI.Shared.Interfaces.McpServers
{
    public interface IMcpServer
    {
        public string? Class { get; set; }
        public string Type { get; set; }
        public string Identifier { get; set; }
        public IList<string>? Tools { get; set; }
        public string ToJson();
    }
}
