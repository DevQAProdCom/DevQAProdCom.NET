using System.Collections.Concurrent;

namespace DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces
{
    public interface ILoggingProviderFactoriesSet
    {
        //Is used for use cases, when we want to have several logging providers at the same time. For instance Serilog, log4net, Logging to Cloud Providers Services.
        ConcurrentDictionary<string, ILoggingProviderFactory> LoggingProviderFactories { get; }
    }
}
