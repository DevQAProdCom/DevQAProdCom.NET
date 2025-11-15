using DevQAProdCom.NET.Logging.Providers.Serilog.Interfaces;
using DevQAProdCom.NET.Logging.Shared.Enumerations;
using Serilog.Events;

namespace DevQAProdCom.NET.Logging.Providers.Serilog.Mappers
{
    public class SerilogLoggingProviderMappers : ISerilogLoggingProviderMappers
    {
        public LogLevel? ToLogLevel(LogEventLevel? logEventLevel)
        {
            return logEventLevel switch
            {
                LogEventLevel.Verbose => LogLevel.Verbose,
                LogEventLevel.Debug => LogLevel.Debug,
                LogEventLevel.Information => LogLevel.Info,
                LogEventLevel.Warning => LogLevel.Warning,
                LogEventLevel.Error => LogLevel.Error,
                LogEventLevel.Fatal => LogLevel.Fatal,
                _ => throw new NotSupportedException($"No mapping exists for Serilog Logging Provider log event level '{logEventLevel.ToString()}' to log level of Framework logger.")
            };
        }
    }
}
