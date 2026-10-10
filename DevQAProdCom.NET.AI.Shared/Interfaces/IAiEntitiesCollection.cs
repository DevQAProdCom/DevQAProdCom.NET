namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntitiesCollection<TAiEntityYamlConfiguration> : 
        IEnumerable<IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration>> where TAiEntityYamlConfiguration : IAiEntityConfiguration, new()

    {
        public string CollectionIdentifier { get; }

        public IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration> Add(IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration> entity);
        public List<IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration>> Add(params IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration>[] entities);

        public IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration> AddFromFile(string filePath);
        public List<IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration>> AddFromFiles(params string[] filesPaths);
        public List<IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration>> AddFromDirectory(string directoryPath);
        public List<IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration>> AddFromDirectories(params string[] directoriesPaths);

        public IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration> GetByIdentifier(string entityIdentifier);
        public bool TryGetByIdentifier(string entityIdentifier, out IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration>? entity);

        public IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration> GetByFilePath(string filePath);
        public bool TryGetByFilePath(string filePath, out IAiEntityWithTConfigurationType<TAiEntityYamlConfiguration>? entity);
    }
}
