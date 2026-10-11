namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiAgentConfigurationWithoutAiEntityIdentifiers : IAiAgentConfigurationStandardDataPropertiesWithoutAiEntityIdentifiers, IAiAgentConfigurationCustomDataProperties
    {
        public string? ConfigurationIdentifier { get; set; }
    }
}
