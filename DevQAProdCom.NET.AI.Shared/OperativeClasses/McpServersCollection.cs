using System.Collections;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class McpServersCollection : IMcpServersCollection
    {
        public string CollectionIdentifier { get; }
        protected List<IMcpServer> McpServers { get; set; } = new List<IMcpServer>();

        protected ILogger Logger;

        public McpServersCollection(ILogger logger, bool initializeFromDefaultLocations = false, string? collectionIdentifier = null, bool useExtendedSearch = false)
        {
            Logger = logger;
            CollectionIdentifier = collectionIdentifier ?? Guid.NewGuid().ToString();

            if (initializeFromDefaultLocations)
            {
                InitializeCollectionFromDefaultLocations();
            }
        }

        public virtual IMcpServer GetByIdentifier(string identifier)
        {
            if (TryGetByIdentifierOrDefault(identifier, out var mcpServer))
            {
                return mcpServer!;
            }
            else
            {
                throw new KeyNotFoundException($"MCP Server with identifier '{identifier}' is not found in the collection.");
            }
        }

        public virtual bool TryGetByIdentifierOrDefault(string identifier, out IMcpServer? mcpServer)
        {
            mcpServer = McpServers.SingleOrDefault(x => x.Identifier == identifier);
            return mcpServer != null;
        }

        public virtual IMcpServer Add(IMcpServer mcpServer)
        {
            McpServers.Add(mcpServer);
            return mcpServer;
        }

        protected virtual void InitializeCollectionFromDefaultLocations() { }

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
