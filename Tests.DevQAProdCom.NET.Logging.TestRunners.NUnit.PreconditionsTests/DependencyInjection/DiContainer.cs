using DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses;
using DevQAProdCom.NET.Logging.Providers.Serilog;
using DevQAProdCom.NET.Logging.Providers.Serilog.DependencyInjection;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.Shared.OperativeClasses;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.DependencyInjection;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.DependencyInjection
{
    internal class DiContainer : MicrosoftDiContainerWithDefaultServices
    {
        private static readonly Lazy<DiContainer> _instance = new Lazy<DiContainer>(() => new DiContainer());
        public static DiContainer Instance => _instance.Value;

        public ITestLogger Log { get; private set; }

        protected override void ConfigureServices()
        {
            AddLogger();
        }

        protected override void InitializeRequiredServices()
        {
            //Get Singleton instance of ITestLogger
            Log = GetRequiredService<ITestLogger>();
        }

        protected override void ConfigureExternalDiContainers()
        {
            //Configure Service Providers of External Projects DiContainers with based on this Project Configuration
            _serviceProvider.ConfigureNUnitLoggingExtensionDiContainer();
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
