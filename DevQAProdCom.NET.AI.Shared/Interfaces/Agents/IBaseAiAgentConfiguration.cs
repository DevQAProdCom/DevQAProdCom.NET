namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IBaseAiAgentConfiguration : IAiEntityConfiguration
    {
        public IList<string>? Tools { get; set; }
        public string? Model { get; set; }
    }
}
