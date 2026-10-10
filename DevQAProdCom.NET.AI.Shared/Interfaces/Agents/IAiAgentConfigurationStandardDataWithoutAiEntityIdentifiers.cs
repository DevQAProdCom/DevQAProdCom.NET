namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiAgentConfigurationStandardDataWithoutAiEntityIdentifiers
    {
        public IList<string>? Tools { get; set; }
        public string? Model { get; set; }
    }
}
