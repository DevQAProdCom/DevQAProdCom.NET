namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntitiesCollection<TAiEntityYamlConfiguration> where TAiEntityYamlConfiguration : IAiEntityYamlConfiguration, new()
    {
        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetEntityDataByIdentifier(string entityIdentifier);
        public bool TryGetEntityDataByIdentifier(string entityIdentifier, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entity);
        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddEntityDataFromFile(string filePath);
        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>[] AddEntitiesDataFromFiles(params string[] filePaths);
        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>[] AddEntitiesDataFromDirectories(params string[] directoriesPaths);
        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetEntityDataByFilePath(string filePath);
        public bool TryGetEntityDataByFilePath(string filePath, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entity);
    }
}
