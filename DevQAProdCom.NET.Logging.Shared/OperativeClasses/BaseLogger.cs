using System.Collections.Concurrent;
using DevQAProdCom.NET.Logging.Shared.Enumerations;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.Logging.Shared.OperativeClasses
{
    public class BaseLogger : ILogger
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public LogLevel? MinimumLogLevel
        {
            get
            {
                var minimumLogLevel = LoggingProviders.Select(x => x.Value.MinimumLogLevel).OrderBy(x => x).FirstOrDefault();

                if (minimumLogLevel == null || minimumLogLevel == LogLevel.NotSet)
                    throw new Exception("None of Logging Providers has Minimum Log Level set.");

                return minimumLogLevel;
            }
        }

        public const string DefaultLoggingProvidersSet = "DefaultLoggingProvidersSet";
        protected string CurrentThreadId => Thread.CurrentThread.ManagedThreadId.ToString();

        protected ConcurrentDictionary<string, ILoggingProviderFactory> LoggingProvidersFactories { get; } = new();

        /// <summary>
        /// This can be used to add several logging providers to the logger - like Serilog + log4net or else at the same time.
        ///It can be used for cases, when we want to change the logging provider gradually or just use features of several at the same time.
        ///For instance some logging provider supports some enrichers, and another one others.
        ///Is supposed to be initialized once during DI container initialization.
        /// </summary>
        protected ConcurrentDictionary<string, ILoggingProvider> LoggingProviders { get; } = new();

        private List<Func<ILogMessage, ILogMessage>> _enrichers = new();

        protected object _loggingProvidersAccessLock = new();

        public BaseLogger(ILoggingProviderFactoriesSet loggingProviderFactoriesSet)
        {
            LoggingProvidersFactories = loggingProviderFactoriesSet.LoggingProviderFactories;
        }

        protected virtual void InitializeDefaultLoggingProviders()
        {
            foreach (var factory in LoggingProvidersFactories)
            {
                var loggingProvider = factory.Value.CreateLoggingProvider();

                if (loggingProvider != null)
                    LoggingProviders.TryAdd($"{DefaultLoggingProvidersSet}:{loggingProvider.Name}", loggingProvider);
            }
        }

        #region Log Levels Methods

        public void Verbose(string message, params object[]? values)
        {
            WriteLogMessage(LogLevel.Verbose, message, values, (loggingProvider, template) => { loggingProvider.Verbose(template.Message, template.Values); });
        }

        public void Verbose(ILogMessage logMessage)
        {
            Verbose(logMessage.Message, logMessage.Values);
        }

        public void Debug(string message, params object[]? values)
        {
            WriteLogMessage(LogLevel.Debug, message, values, (loggingProvider, template) => { loggingProvider.Debug(template.Message, template.Values); });
        }

        public void Debug(ILogMessage logMessage)
        {
            Debug(logMessage.Message, logMessage.Values);
        }

        public void Info(string message, params object[]? values)
        {
            WriteLogMessage(LogLevel.Info, message, values, (loggingProvider, template) => { loggingProvider.Info(template.Message, template.Values); });
        }

        public void Info(ILogMessage logMessage)
        {
            Info(logMessage.Message, logMessage.Values);
        }

        public void Info(List<ILoggingProvider> loggingProviders, ILogMessage logMessage)
        {
            if (MinimumLogLevel <= LogLevel.Info)
                foreach (ILoggingProvider loggingProvider in loggingProviders)
                    loggingProvider.Info(logMessage.Message, logMessage.Values);
        }

        public void Warning(string message, params object[]? values)
        {
            WriteLogMessage(LogLevel.Warning, message, values, (loggingProvider, template) => { loggingProvider.Warning(template.Message, template.Values); });
        }

        public void Warning(ILogMessage logMessage)
        {
            Warning(logMessage.Message, logMessage.Values);
        }

        public void Error(string message, params object[]? values)
        {
            WriteLogMessage(LogLevel.Error, message, values, (loggingProvider, template) => { loggingProvider.Error(template.Message, template.Values); });
        }

        public void Error(ILogMessage logMessage)
        {
            Error(logMessage.Message, logMessage.Values);
        }

        public void Fatal(string message, params object[]? values)
        {
            WriteLogMessage(LogLevel.Fatal, message, values, (loggingProvider, template) => { loggingProvider.Fatal(template.Message, template.Values); });
        }

        public void Fatal(ILogMessage logMessage)
        {
            Fatal(logMessage.Message, logMessage.Values);
        }

        #endregion Log Levels Methods

        #region Enricher

        public ILogMessage Enrich(string message, params object[] values)
        {
            ILogMessage logMessage = new LogMessage(message, values);

            foreach (var enricher in _enrichers)
                logMessage = enricher(logMessage);

            return logMessage;
        }

        public ILogger AddEnricher(ILogEnricher enricher)
        {
            if (enricher != null)
                _enrichers.Add(enricher.Enrich);

            return this;
        }

        public ILogger AddEnricher(Func<ILogMessage, ILogMessage> enricher)
        {
            if (enricher != null)
                _enrichers.Add(enricher);

            return this;
        }

        #endregion Enricher

        #region Logging Providers

        public virtual List<ILoggingProvider> GetLoggingProviders(string? loggingProvidersSetIdentifier = null)
        {
            List<ILoggingProvider> loggingProviders = new();

            lock (_loggingProvidersAccessLock)
            {
                loggingProvidersSetIdentifier ??= DefaultLoggingProvidersSet;
                loggingProviders = LoggingProviders.Where(x => x.Key.StartsWith(loggingProvidersSetIdentifier)).Select(x => x.Value).ToList();

                if (!loggingProviders.Any())
                {
                    foreach (var factory in LoggingProvidersFactories)
                    {
                        ILoggingProvider loggingProvider = factory.Value.CreateLoggingProvider();
                        string loggingProviderIdentifier = GetLoggingProviderIdentifier(loggingProvider, loggingProvidersSetIdentifier);
                        LoggingProviders.TryAdd($"{loggingProviderIdentifier}", loggingProvider);
                        loggingProviders.Add(loggingProvider);
                    }
                }
            }

            if (loggingProviders.Count == 0)
                throw new Exception("No logging providers found.");

            return loggingProviders;
        }

        #endregion Logging Providers

        #region Auxiliary

        protected void WriteLogMessage(LogLevel logLevel, string message, object[]? values, Action<ILoggingProvider, ILogMessage> action)
        {
            try
            {
                //Preserve order. At first, logging providers initialization, then MinimumLogLevel initialization, cause it is calculated dynamically as Min from Minimum Log Levels of all logging providers
                var loggingProviders = GetLoggingProviders();

                if (MinimumLogLevel <= logLevel)
                {
                    var template = Enrich(message, values);

                    foreach (var loggingProvider in loggingProviders)
                        action.Invoke(loggingProvider, template);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to log message '{message}'. Error message: '{ex.Message}'. Stack trace: {ex.StackTrace}.");
            }
        }

        private string GetLoggingProviderIdentifier(ILoggingProvider loggingProvider, string loggingProvidersSetIdentifier = DefaultLoggingProvidersSet)
        {
            if (string.IsNullOrEmpty(loggingProvider.Name))
                loggingProvider.Name ??= loggingProvider.GetType().FullName ?? loggingProvider.Id.ToString();

            return $"{loggingProvidersSetIdentifier}:{loggingProvider.Name}";
        }

        #endregion Auxiliary

        #region IDisposable

        private bool _disposed = false; // To detect redundant calls

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                foreach (var loggingProvider in LoggingProviders.Values)
                    loggingProvider.Dispose();
            }

            _disposed = true;
        }

        // Finalizer
        ~BaseLogger()
        {
            Dispose(false);
        }

        #endregion IDisposable

    }
}
