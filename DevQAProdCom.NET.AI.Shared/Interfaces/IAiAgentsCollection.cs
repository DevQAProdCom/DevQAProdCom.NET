namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiAgentsCollection<TAiAgentYamlConfiguration> where TAiAgentYamlConfiguration : IBaseAiAgentYamlConfiguration, new()
    {
        public IAiAgent<TAiAgentYamlConfiguration> GetAgentData(string agentIdentifier);
        public bool TryGetAgentData(string agentIdentifier, out IAiAgent<TAiAgentYamlConfiguration>? agent);
        public IAiAgent<TAiAgentYamlConfiguration> AddAgentData(string filePath);
    }
}
