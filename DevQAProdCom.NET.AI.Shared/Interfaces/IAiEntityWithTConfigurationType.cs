namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntityWithTConfigurationType<TYamlConfigurationType> : IAiEntity
    {
        public TYamlConfigurationType ConfigurationData { get; set; }
    }
}
