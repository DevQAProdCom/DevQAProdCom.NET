namespace DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces
{
    public interface ILogger : IBaseLogger
    {
        #region Enrichers

        public ILogger AddEnricher(ILogEnricher enricher);
        public ILogger AddEnricher(Func<ILogMessage, ILogMessage> enricher);
        public ILogMessage Enrich(string message, params object[] values);

        #endregion Enrichers

        #region Logging Providers

        public List<ILoggingProvider> GetLoggingProviders(string? loggingProvidersSetIdentifier = null);

        #endregion Logging Providers
    }
}
