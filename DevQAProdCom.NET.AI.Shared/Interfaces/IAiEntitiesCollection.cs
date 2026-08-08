namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntitiesCollection<TAiEntityYamlConfiguration> where TAiEntityYamlConfiguration : IAiEntityYamlConfiguration, new()
    {
        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetEntityData(string entityIdentifier);
        public bool TryGetEntityData(string entityIdentifier, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entity);
        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddEntityData(string filePath);
    }
}
