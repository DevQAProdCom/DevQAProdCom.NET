namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntitiesCollection<TAiEntityYamlConfiguration> : 
        IEnumerable<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> where TAiEntityYamlConfiguration : IAiEntityYamlConfiguration, new()

    {
        public string CollectionIdentifier { get; }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> Add(IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> entity);
        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> Add(params IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>[] entities);

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddFromFile(string filePath);
        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddFromFiles(params string[] filesPaths);
        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddFromDirectory(string directoryPath);
        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddFromDirectories(params string[] directoriesPaths);

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetByIdentifier(string entityIdentifier);
        public bool TryGetByIdentifier(string entityIdentifier, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entity);

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetByFilePath(string filePath);
        public bool TryGetByFilePath(string filePath, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entity);
    }
}
