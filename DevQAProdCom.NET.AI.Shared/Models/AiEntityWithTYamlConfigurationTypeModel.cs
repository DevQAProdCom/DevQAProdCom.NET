using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiEntityWithTYamlConfigurationType<TYamlConfigurationType> : AiEntityModel, IAiEntityWithTYamlConfigurationType<TYamlConfigurationType>
    {
        public TYamlConfigurationType ConfigurationData { get; set; }
    }
}
