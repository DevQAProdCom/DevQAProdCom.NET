namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiAgentsConfigurationsMappers<TAiEntityConfiguration> where TAiEntityConfiguration : IAiAgentConfigurationWithCustomMetadata
    {
        IAiEntityWithTConfigurationType<TAiEntityConfiguration> ToAiAgentWithConfiguration(IAiEntityWithTConfigurationType<TAiEntityConfiguration> aiAgent, IAiAgentConfigurationWithoutAiEntityIdentifiers? configuration = null);
        IAiEntityWithTConfigurationType<TAiEntityConfiguration> ToAiAgentWithConfiguration(IAiEntityWithTConfigurationType<TAiEntityConfiguration> aiAgent, IAiAgentConfiguration configuration);
        IAiAgentConfiguration ToIAiAgentConfigurationFromAgentWithTConfigurationTypeConfigurations(IAiEntityWithTConfigurationType<TAiEntityConfiguration> aiAgent, IAiAgentConfigurationWithoutAiEntityIdentifiers configuration);
    }
}
