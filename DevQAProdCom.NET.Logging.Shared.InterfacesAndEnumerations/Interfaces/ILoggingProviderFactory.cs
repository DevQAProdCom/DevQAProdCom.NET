using Microsoft.Extensions.Configuration;

namespace DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces
{
    public interface ILoggingProviderFactory
    {
        ILoggingProvider CreateLoggingProvider(IConfiguration? configParameters = null);
    }
}
