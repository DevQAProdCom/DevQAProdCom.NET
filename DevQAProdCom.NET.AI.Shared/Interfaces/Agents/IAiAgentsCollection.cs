namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiEntityWithTYamlConfigurationTypesCollection<TAiAgentYamlConfiguration> where TAiAgentYamlConfiguration : IBaseAiAgentYamlConfiguration, new()
    {
        public IAiEntityWithTYamlConfigurationType<TAiAgentYamlConfiguration> GetAgentData(string agentIdentifier);
        public bool TryGetAgentData(string agentIdentifier, out IAiEntityWithTYamlConfigurationType<TAiAgentYamlConfiguration>? agent);
        public IAiEntityWithTYamlConfigurationType<TAiAgentYamlConfiguration> AddAgentData(string filePath);
    }
}
