using DevQAProdCom.NET.Logging.Shared.Enumerations;
using Serilog.Events;

namespace DevQAProdCom.NET.Logging.Providers.Serilog.Interfaces
{
    public interface ISerilogLoggingProviderMappers
    {
        public LogLevel? ToLogLevel(LogEventLevel? logEventLevel);
    }
}
