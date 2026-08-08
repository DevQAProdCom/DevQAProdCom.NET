namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IBaseAiAgentYamlConfiguration : IBaseAiEntityYamlConfiguration
    {
        public IList<string>? Tools { get; set; }
        public IList<string>? Skills { get; set; }
        public string? Model { get; set; }
    }
}
