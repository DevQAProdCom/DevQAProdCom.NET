namespace DevQAProdCom.NET.AI.Shared.Interfaces.Agents
{
    public interface IAiAgentConfigurationCustomConfigurationData
    {
        public List<string>? Permissions { get; set; }
        public List<string>? Instructions { get; set; }
        public List<string>? Skills { get; set; }
        public List<string>? Subagents { get; set; }
        public List<string>? Hooks { get; set; }
        public List<string>? McpServers { get; set; }
    }
}
