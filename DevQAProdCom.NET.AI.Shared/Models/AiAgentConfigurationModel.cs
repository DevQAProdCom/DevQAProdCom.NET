using System.Text.Json.Serialization;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiAgentConfigurationModel : BaseAiEntityConfigurationModel, IAiAgentConfiguration
    {
        [JsonPropertyName("configurationIdentifier")]
        [YamlMember(Alias = "configurationIdentifier")]
        public string? ConfigurationIdentifier { get; set; }

        #region Standard Data Properties

        [JsonPropertyName("tools")]
        [YamlMember(Alias = "tools")]
        public IList<string>? Tools { get; set; }

        [JsonPropertyName("model")]
        [YamlMember(Alias = "model")]
        public string? Model { get; set; }

        #endregion Standard Data Properties

        #region Custom Data Properties

        [JsonPropertyName("permissions")]
        [YamlMember(Alias = "permissions")]
        public List<string>? Permissions { get; set; }

        [JsonPropertyName("instructions")]
        [YamlMember(Alias = "instructions")]
        public List<string>? Instructions { get; set; }

        [JsonPropertyName("skills")]
        [YamlMember(Alias = "skills")]
        public List<string>? Skills { get; set; }

        [JsonPropertyName("subagents")]
        [YamlMember(Alias = "subagents")]
        public List<string>? Subagents { get; set; }

        [JsonPropertyName("hooks")]
        [YamlMember(Alias = "hooks")]
        public List<string>? Hooks { get; set; }

        [JsonPropertyName("mcpServers")]
        [YamlMember(Alias = "mcpServers")]
        public List<string>? McpServers { get; set; }

        #endregion Custom Data Properties
    }
}
