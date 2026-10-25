namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiAgentConfigurationCustomMetadata
    {
        public List<IAiAgentConfigurationWithoutAiEntityIdentifiers>? Configurations { get; set; }
        public IAiAgentConfigurationWithoutAiEntityIdentifiers GetConfiguration(string? identifier = null);
        public bool TryGetConfiguration(string? identifier, out IAiAgentConfigurationWithoutAiEntityIdentifiers? configuration);
        public IAiAgentConfigurationWithoutAiEntityIdentifiers? GetDefaultConfiguration();
    }
}
