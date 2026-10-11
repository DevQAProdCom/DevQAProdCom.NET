namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntitiesCollection<TAiEntityConfiguration> : 
        IEnumerable<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> where TAiEntityConfiguration : IAiEntityIdentifiers, new()

    {
        public string CollectionIdentifier { get; }

        public IAiEntityWithTConfigurationType<TAiEntityConfiguration> Add(IAiEntityWithTConfigurationType<TAiEntityConfiguration> entity);
        public List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> Add(params IAiEntityWithTConfigurationType<TAiEntityConfiguration>[] entities);

        public IAiEntityWithTConfigurationType<TAiEntityConfiguration> AddFromFile(string filePath);
        public List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> AddFromFiles(params string[] filesPaths);
        public List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> AddFromDirectory(string directoryPath);
        public List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> AddFromDirectories(params string[] directoriesPaths);

        public IAiEntityWithTConfigurationType<TAiEntityConfiguration> GetByIdentifier(string entityIdentifier);
        public bool TryGetByIdentifier(string entityIdentifier, out IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entity);

        public IAiEntityWithTConfigurationType<TAiEntityConfiguration> GetByFilePath(string filePath);
        public bool TryGetByFilePath(string filePath, out IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entity);
    }
}
