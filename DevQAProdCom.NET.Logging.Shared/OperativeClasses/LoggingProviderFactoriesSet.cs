using System.Collections.Concurrent;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.Logging.Shared.OperativeClasses
{
    public class LoggingProviderFactoriesSet: ILoggingProviderFactoriesSet
    {
        public ConcurrentDictionary<string, ILoggingProviderFactory> LoggingProviderFactories { get; } = new();
    }
}
