namespace DevQAProdCom.NET.AI.Shared.Interfaces.McpServers
{
    public interface IMcpServersCollection : IEnumerable<IMcpServer>
    {
        public string CollectionIdentifier { get; }
        public IMcpServer GetByIdentifier(string identifier);
        public bool TryGetByIdentifierOrDefault(string identifier, out IMcpServer? mcpServer);
        public IMcpServer AddByIdentifier(IMcpServer mcpServer);
    }
}
