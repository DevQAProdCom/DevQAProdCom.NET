using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiAgentConfigurationModel : BaseAiEntityConfigurationModel, IAiAgentConfiguration
    {
        public string? ConfigurationIdentifier { get; set; }

        #region Standard Data Properties

        public IList<string>? Tools { get; set; }
        public string? Model { get; set; }

        #endregion Standard Data Properties

        #region Custom Data Properties

        public List<string>? Permissions { get; set; }
        public List<string>? Instructions { get; set; }
        public List<string>? Skills { get; set; }
        public List<string>? Subagents { get; set; }
        public List<string>? Hooks { get; set; }
        public List<string>? McpServers { get; set; }

        #endregion Custom Data Properties
    }
}
