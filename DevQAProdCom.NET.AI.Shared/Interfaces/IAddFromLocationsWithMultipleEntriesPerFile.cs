namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAddFromLocationsWithMultipleEntriesPerFile<T>
    {
        //TODO Refactor to single method - AddFromLocations(string[] locations);
        public List<T> AddFromFile(string filePath);
        public List<T> AddFromFiles(params string[] filesPaths);
        public List<T> AddFromDirectory(string directoryPath);
        public List<T> AddFromDirectories(params string[] directoriesPaths);
    }
}
