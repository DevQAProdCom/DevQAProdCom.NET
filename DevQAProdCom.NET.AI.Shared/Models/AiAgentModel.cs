using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiAgentModel<TAiAgentYamlConfiguration> : IAiAgent<TAiAgentYamlConfiguration>
    {
        public string? FilePath { get; set; }
        public TAiAgentYamlConfiguration? ConfigurationData { get; set; }
        public string Prompt { get; set; }
    }
}
