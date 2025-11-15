namespace DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces
{
    public interface ILogEnricher
    {
        public ILogMessage Enrich(ILogMessage logMessage);
    }
}
