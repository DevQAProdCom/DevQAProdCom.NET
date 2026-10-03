using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class FileBasedMcpServersCollection : IdentifierBasedEntitiesCollection<IFileBasedMcpServer>
    {
        public FileBasedMcpServersCollection(ILogger logger, string? collectionIdentifier = null) : base(logger, collectionIdentifier)
        {
        }



        public List<T> AddFromFile(string filePath);
        public List<T> AddFromFiles(params string[] filesPaths);
        public List<T> AddFromDirectory(string directoryPath);
        public List<T> AddFromDirectories(params string[] directoriesPaths);
    }
}
