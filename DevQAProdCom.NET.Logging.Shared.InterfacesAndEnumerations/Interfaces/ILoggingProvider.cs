namespace DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces
{
    public interface ILoggingProvider : IBaseLogger
    {
        public long LastLogEntryOrderNumber { get; }
    }
}
