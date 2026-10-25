namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiAgentsCollectionWithConfigurations<TAiAgentConfiguration> : IAiEntitiesCollection<TAiAgentConfiguration>
        where TAiAgentConfiguration : IAiAgentConfigurationWithCustomMetadata
    {
        IAiEntityWithTConfigurationType<TAiAgentConfiguration> GetByConfiguration(string configurationIdentifier);
        bool TryGetByConfiguration(string configurationIdentifier, out IAiEntityWithTConfigurationType<TAiAgentConfiguration>? entity);

        IAiEntityWithTConfigurationType<TAiAgentConfiguration> GetByIdentifierAndConfiguration(string entityIdentifier, string configurationIdentifier);
        bool TryGetByIdentifierAndConfiguration(string entityIdentifier, string configurationIdentifier, out IAiEntityWithTConfigurationType<TAiAgentConfiguration>? entity);

        IAiEntityWithTConfigurationType<TAiAgentConfiguration> GetByFilePathAndConfiguration(string filePath, string configurationIdentifier);
        bool TryGetByFilePathAndConfiguration(string filePath, string configurationIdentifier, out IAiEntityWithTConfigurationType<TAiAgentConfiguration>? entity);
    }
}
