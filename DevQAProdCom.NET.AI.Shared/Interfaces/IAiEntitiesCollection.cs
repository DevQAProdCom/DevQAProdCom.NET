namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntitiesCollection<TAiEntityYamlConfiguration> where TAiEntityYamlConfiguration : IAiEntityYamlConfiguration, new()
    {
        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddEntityData(IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> entity);
        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddEntitiesData(params IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>[] entities);

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddEntityDataFromFile(string filePath);
        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddEntitiesDataFromFiles(params string[] filesPaths);
        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddEntitiesDataFromDirectory(string directoryPath);
        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddEntitiesDataFromDirectories(params string[] directoriesPaths);

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetEntityDataByIdentifier(string entityIdentifier);
        public bool TryGetEntityDataByIdentifier(string entityIdentifier, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entity);

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetEntityDataByFilePath(string filePath);
        public bool TryGetEntityDataByFilePath(string filePath, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entity);
    }
}
