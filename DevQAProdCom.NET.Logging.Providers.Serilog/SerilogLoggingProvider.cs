using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Providers.Serilog.Interfaces;
using DevQAProdCom.NET.Logging.Shared.Enumerations;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DevQAProdCom.NET.Logging.Providers.Serilog
{
    public class SerilogLoggingProvider : ILoggingProvider
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; } = typeof(SerilogLoggingProvider).FullName!;

        private Int64 _lastLogEntryOrderNumber = 0;
        public Int64 LastLogEntryOrderNumber => _lastLogEntryOrderNumber;

        public LogLevel? MinimumLogLevel
        {
            get
            {
                var minimumLogLevel = _serilogLoggingProviderMappers.ToLogLevel(LoggingLevelSwitch.MinimumLevel);

                if (minimumLogLevel == null)
                    throw new ArgumentNullException("Minimum log level of Serilog Logging Provider is not set.");

                return minimumLogLevel;
            }
        }

        public LoggingLevelSwitch LoggingLevelSwitch { get; }

        private global::Serilog.Core.Logger _logger;
        private IConfiguration _configParameters;
        private readonly ISerilogLoggingProviderMappers _serilogLoggingProviderMappers;

        public SerilogLoggingProvider(IConfiguration configParameters, ISerilogLoggingProviderMappers serilogLoggingProviderMappers)
        {
            _configParameters = configParameters;
            _serilogLoggingProviderMappers = serilogLoggingProviderMappers;

            LogEventLevel minimumLogEventLevel = GetMinimumLogEventLevelFromConfiguration(_configParameters);
            LoggingLevelSwitch = new(minimumLogEventLevel);
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(_configParameters)
                .MinimumLevel.ControlledBy(LoggingLevelSwitch);

            _logger = loggerConfiguration.CreateLogger();
        }

        private LogEventLevel GetMinimumLogEventLevelFromConfiguration(IConfiguration configParameters)
        {
            string configurationPath = "Serilog:MinimumLevel:Default";
            var minimumLogLevel = configParameters.GetSection(configurationPath).Value;

            if(string.IsNullOrEmpty(minimumLogLevel))
                throw new Exception($"Unable to find Serilog Minimum Log Level inside configuration using path: '{configurationPath}'.");

            return Enum.Parse<LogEventLevel>(minimumLogLevel, ignoreCase: true);
        }

        #region Log Levels Methods

        public void Verbose(string message, params object[]? values)
        {
            if (MinimumLogLevel <= LogLevel.Verbose) //It is done so that Interlocked.Increment is not called unnecessarily
            {
                _logger.Verbose(message, values);
                Interlocked.Increment(ref _lastLogEntryOrderNumber);
            }
        }

        public void Verbose(ILogMessage logMessage)
        {
            Verbose(logMessage.Message, logMessage.Values);
        }

        public void Debug(string message, params object[]? values)
        {
            if (MinimumLogLevel <= LogLevel.Debug)
            {
                _logger.Debug(message, values);
                Interlocked.Increment(ref _lastLogEntryOrderNumber);
            }
        }

        public void Debug(ILogMessage logMessage)
        {
            Debug(logMessage.Message, logMessage.Values);
        }

        public void Info(string message, params object[]? values)
        {
            if (MinimumLogLevel <= LogLevel.Info)
            {
                _logger.Information(message, values);
                Interlocked.Increment(ref _lastLogEntryOrderNumber);
            }
        }

        public void Info(ILogMessage logMessage)
        {
            Info(logMessage.Message, logMessage.Values);
        }

        public void Warning(string message, params object[]? values)
        {
            if (MinimumLogLevel <= LogLevel.Warning)
            {
                _logger.Warning(message, values);
                Interlocked.Increment(ref _lastLogEntryOrderNumber);
            }
        }

        public void Warning(ILogMessage logMessage)
        {
            Warning(logMessage.Message, logMessage.Values);
        }

        public void Error(string message, params object[]? values)
        {
            if (MinimumLogLevel <= LogLevel.Error)
            {
                _logger.Error(message, values);
                Interlocked.Increment(ref _lastLogEntryOrderNumber);
            }
        }

        public void Error(ILogMessage logMessage)
        {
            Error(logMessage.Message, logMessage.Values);
        }

        public void Fatal(string message, params object[]? values)
        {
            if (MinimumLogLevel <= LogLevel.Fatal)
            {
                _logger.Fatal(message, values);
                Interlocked.Increment(ref _lastLogEntryOrderNumber);
            }
        }

        public void Fatal(ILogMessage logMessage)
        {
            Fatal(logMessage.Message, logMessage.Values);
        }

        #endregion Log Levels Methods

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
                _logger.Dispose();
            }

            // Free any unmanaged resources here.
            // e.g., CloseHandle(unmanagedResource);

            _disposed = true;
        }

        // Finalizer
        ~SerilogLoggingProvider()
        {
            Dispose(false);
        }

        #endregion IDisposable
    }
}
