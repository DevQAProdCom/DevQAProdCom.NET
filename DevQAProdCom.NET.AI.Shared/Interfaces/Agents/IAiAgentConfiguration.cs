namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    //Is supposed to be treated as Generic Provider Agnostic Agent Configuration Interface, which can be used to configure any type of AI Agent, regardless of the specific provider or implementation.
    public interface IAiAgentConfiguration : IAiEntityIdentifiers, IAiAgentConfigurationWithoutAiEntityIdentifiers
    {
    }
}
