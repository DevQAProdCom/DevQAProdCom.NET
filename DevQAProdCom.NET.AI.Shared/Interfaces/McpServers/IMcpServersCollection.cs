namespace DevQAProdCom.NET.AI.Shared.Interfaces.McpServers
{
    public interface IMcpServersCollection : IEnumerable<IMcpServer>
    {
        public IMcpServer? GetByIdentifierOrDefault(string identifier);
        public IMcpServer AddByIdentifier(IMcpServer mcpServer);
    }
}
