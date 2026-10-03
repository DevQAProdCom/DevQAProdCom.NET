namespace DevQAProdCom.NET.AI.Shared.Interfaces.McpServers
{
    public interface IFileBasedMcpServersCollection : IIdentifierBasedEntitiesCollection<IFileBasedMcpServer>, IAddFromLocationsWithMultipleEntriesPerFile<IFileBasedMcpServer>
    {
        public List<IFileBasedMcpServer> AddFromFile(string filePath);
        public List<IFileBasedMcpServer> AddFromFiles(params string[] filesPaths);
        public List<IFileBasedMcpServer> AddFromDirectory(string directoryPath);
        public List<IFileBasedMcpServer> AddFromDirectories(params string[] directoriesPaths);
    }
}
