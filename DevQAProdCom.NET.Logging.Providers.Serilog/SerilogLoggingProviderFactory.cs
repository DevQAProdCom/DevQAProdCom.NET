using DevQAProdCom.NET.Logging.Providers.Serilog.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DevQAProdCom.NET.Logging.Providers.Serilog
{
    public class SerilogLoggingProviderFactory : ILoggingProviderFactory
    {
        private readonly IConfiguration _configParameters;
        private readonly ISerilogLoggingProviderMappers _serilogLoggingProviderMappers;

        public SerilogLoggingProviderFactory(IConfiguration configParameters, ISerilogLoggingProviderMappers serilogLoggingProviderMappers)
        {
            _configParameters = configParameters;
            _serilogLoggingProviderMappers = serilogLoggingProviderMappers;
        }

        public ILoggingProvider CreateLoggingProvider(IConfiguration? configParameters = null)
        {
            return new SerilogLoggingProvider(configParameters ?? _configParameters, _serilogLoggingProviderMappers);
        }
    }
}
