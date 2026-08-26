using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.Global.Extensions;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiEntityWithTYamlConfigurationTypeModel<TYamlConfigurationType> : AiEntityModel, IAiEntityWithTYamlConfigurationType<TYamlConfigurationType>
    {
        public TYamlConfigurationType ConfigurationData { get; set; }

        public override string ToMdFileContent()
        {
            var yaml = ConfigurationData?.ToYaml() ?? string.Empty;
            var prompt = Prompt ?? string.Empty;

            return $"---\n{yaml}---\n\n{prompt}";
        }
    }
}
