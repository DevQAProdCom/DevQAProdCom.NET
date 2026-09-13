namespace DevQAProdCom.NET.AI.Shared.Interfaces.McpServers
{
    public interface IMcpServer
    {
        public string? Class { get; }
        public string? FilePath { get; set; }

        public string Identifier { get; set; }
        public string? Type { get; set; }
        public List<string>? Tools { get; set; }

        public void WithConfigurationFromJson(string configuration);
        public void WithConfigurationFromYaml(string configuration);
        //public void AssignToOrDefault<TMcpServer>(List<TMcpServer> list) where TMcpServer : class;
        public string ToJson();
        public bool TryGet<TMcpServer>(out TMcpServer? mcpServer) where TMcpServer : class;
    }
}
