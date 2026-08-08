using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiEntityWithTYamlConfigurationTypeModel<TYamlConfigurationType> : AiEntityModel, IAiEntityWithTYamlConfigurationType<TYamlConfigurationType>
    {
        public TYamlConfigurationType ConfigurationData { get; set; }
    }
}
