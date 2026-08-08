using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiAgentModel<TAiAgentYamlConfiguration> : AiEntityWithTYamlConfigurationType<TAiAgentYamlConfiguration>, IAiAgent<TAiAgentYamlConfiguration>
    {
    }
}
