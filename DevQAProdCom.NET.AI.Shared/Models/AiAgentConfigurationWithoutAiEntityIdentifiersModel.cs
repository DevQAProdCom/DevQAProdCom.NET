using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiAgentConfigurationWithoutAiEntityIdentifiersModel : IAiAgentConfigurationWithoutAiEntityIdentifiers
    {
        public string? ConfigurationIdentifier { get; set; }
        public IList<string>? Tools { get; set; }
        public string? Model { get; set; }
        public List<string>? Permissions { get; set; }
        public List<string>? Instructions { get; set; }
        public List<string>? Skills { get; set; }
        public List<string>? Subagents { get; set; }
        public List<string>? Hooks { get; set; }
        public List<string>? McpServers { get; set; }
    }
}
