namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntityWithTConfigurationType<TConfigurationType> : IAiEntity
    {
        public TConfigurationType ConfigurationData { get; set; }
    }
}
