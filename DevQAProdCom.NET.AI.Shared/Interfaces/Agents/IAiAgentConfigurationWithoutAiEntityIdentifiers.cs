namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiAgentConfigurationWithoutAiEntityIdentifiers : IAiAgentConfigurationStandardDataWithoutAiEntityIdentifiers, IAiAgentConfigurationCustomConfigurationData
    {
        public string Identifier { get; set; }
    }
}
