using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class FileBasedMcpServersCollection : IdentifierBasedEntitiesCollection<IFileBasedMcpServer>, IFileBasedMcpServersCollection
    {
        private readonly IFileBasedMcpServersSearcher _mcpServersSearcher;

        public FileBasedMcpServersCollection(ILogger logger, IFileBasedMcpServersSearcher mcpServersSearcher, string? collectionIdentifier = null) : base(logger, collectionIdentifier)
        {
            _mcpServersSearcher = mcpServersSearcher ?? throw new ArgumentNullException(nameof(mcpServersSearcher));
        }

        public FileBasedMcpServersCollection(ILogger logger, IFileBasedMcpServersSearcher mcpServersSearcher, ILocationsProvider? defaultLocationsProvider, string collectionIdentifier) : this(logger, mcpServersSearcher, collectionIdentifier)
        {
            InitializeCollectionFromDefaultLocations(defaultLocationsProvider);
        }

        public List<IFileBasedMcpServer> AddFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);

            Logger.Info("{CollectionIdentifier} Adding MCP servers from file: {FilePath}", $"[{CollectionIdentifier}]", filePath);
            var mcpServers = _mcpServersSearcher.Search(filePath);

            foreach (var mcpServer in mcpServers)
            {
                mcpServer.FilePath = filePath;
                Add(mcpServer);
            }

            return mcpServers;
        }

        public List<IFileBasedMcpServer> AddFromFiles(params string[] filesPaths)
        {
            var mcpServers = new List<IFileBasedMcpServer>();

            foreach (var filePath in filesPaths)
            {
                mcpServers.AddRange(AddFromFile(filePath));
            }

            return mcpServers;
        }

        public List<IFileBasedMcpServer> AddFromDirectory(string directoryPath)
        {
            IoUtils.CheckDirectoryMustExist(directoryPath);
            var mcpServers = _mcpServersSearcher.Search(directoryPath);
            return Add(mcpServers.ToArray());
        }

        public List<IFileBasedMcpServer> AddFromDirectories(params string[] directoriesPaths)
        {
            var mcpServers = new List<IFileBasedMcpServer>();

            foreach (var directoryPath in directoriesPaths)
            {
                mcpServers.AddRange(AddFromDirectory(directoryPath));
            }

            return mcpServers;
        }

        private void InitializeCollectionFromDefaultLocations(ILocationsProvider? defaultLocationsProvider = null)
        {
            if (defaultLocationsProvider != null)
            {
                foreach (var location in defaultLocationsProvider)
                {
                    var mcpServers = _mcpServersSearcher.Search(location);
                    Add(mcpServers.ToArray());
                }
            }
        }
    }
}
