using DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses;
using DevQAProdCom.NET.Logging.Providers.Serilog;
using DevQAProdCom.NET.Logging.Providers.Serilog.DependencyInjection;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.Shared.OperativeClasses;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.DevQAProdCom.NET.Configurations.DependencyInjection
{
    internal class DiContainer : MicrosoftDiContainerWithDefaultServices
    {
        private static readonly Lazy<DiContainer> _instance = new Lazy<DiContainer>(() => new DiContainer());
        public static DiContainer Instance => _instance.Value;

        protected override void ConfigureServices()
        {
            AddLogger();
        }

        private void AddLogger()
        {
            _serviceCollection
                .AddSerilogLoggingProviderFactory()
                .AddSingleton<ILoggingProviderFactoriesSet>(provider =>
                {
                    ILoggingProviderFactoriesSet loggingProviderFactoriesSet = new LoggingProviderFactoriesSet();
                    ILoggingProviderFactory serilogLoggingProviderFactory = provider.GetRequiredService<SerilogLoggingProviderFactory>();

                    loggingProviderFactoriesSet.LoggingProviderFactories.TryAdd(typeof(SerilogLoggingProviderFactory).FullName!, serilogLoggingProviderFactory);

                    return loggingProviderFactoriesSet;
                })
                .AddNUnitTestLogger();
        }
    }
}
