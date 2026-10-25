namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiAgentConfigurationWithCustomMetadata : IAiEntityIdentifiers, IAiAgentConfigurationStandardDataPropertiesWithoutAiEntityIdentifiers
    {
        public IAiAgentConfigurationCustomMetadata? CustomMetadata { get; set; }
    }
}
