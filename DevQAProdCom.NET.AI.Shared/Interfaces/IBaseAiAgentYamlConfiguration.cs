namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IBaseAiAgentYamlConfiguration
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public IList<string>? Tools { get; set; }
        public IList<string>? Skills { get; set; }
        public string? Model { get; set; }
    }
}
