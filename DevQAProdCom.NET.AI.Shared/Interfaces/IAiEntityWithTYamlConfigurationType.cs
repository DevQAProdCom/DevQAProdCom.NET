namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntityWithTYamlConfigurationType<TYamlConfigurationType> : IAiEntity
    {
        public TYamlConfigurationType ConfigurationData { get; set; }
    }
}
