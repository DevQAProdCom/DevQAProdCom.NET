using System.Collections;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class McpServersCollection : IMcpServersCollection
    {
        public List<IMcpServer> McpServers { get; set; } = new List<IMcpServer>();

        public virtual IMcpServer? GetByIdentifierOrDefault(string identifier)
        {
            return McpServers.SingleOrDefault(x => x.Identifier == identifier);
        }

        public virtual IMcpServer AddByIdentifier(IMcpServer mcpServer)
        {
            var existingServer = GetByIdentifierOrDefault(mcpServer.Identifier);

            if (existingServer != null)
                return existingServer;

            McpServers.Add(mcpServer);
            return mcpServer;
        }

        public IEnumerator<IMcpServer> GetEnumerator()
        {
            return McpServers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
