namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiAgentConfigurationCustomMetadataModel
    {
        public List<IAiAgentConfigurationWithoutAiEntityIdentifiers>? Configurations { get; set; }
    }
}
