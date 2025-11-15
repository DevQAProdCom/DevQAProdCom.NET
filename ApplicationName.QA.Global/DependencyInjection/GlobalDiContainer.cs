using ApplicationName.QA.Global.Models;
using DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses;
using DevQAProdCom.NET.Logging.Providers.Serilog;
using DevQAProdCom.NET.Logging.Providers.Serilog.DependencyInjection;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.Shared.OperativeClasses;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.DependencyInjection;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.Models;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationName.QA.Global.DependencyInjection
{
    public class GlobalDiContainer : MicrosoftDiContainerWithDefaultServices
    {
        public AppSettings AppSettings { get; set; }
        public ITestLogger Log { get; set; }

        protected override void ConfigureServices()
        {
            _serviceCollection.AddAppSettings();
            AddLogger();
        }

        protected override void InitializeRequiredServices()
        {
            base.InitializeRequiredServices();
            AppSettings = GetRequiredService<AppSettings>();

            //Get Singleton instance of ITestLogger
            Log = GetRequiredService<ITestLogger>();
        }

        private void AddLogger()
        {
            var nUnitTestLoggerConfigParameters = new NUnitTestLoggerConfigParameters()
            {
                TestRunId = $"RunId: {Guid.NewGuid().ToString().Substring(0, 5)}",
                TestRunName = "RunName: RegressionTestRun",
                VersionUnderTest = "Version: v0.3",
            };

            //DiContainer.Instance.Log.SetTestRunLoggingInfo(testRunId: $"RunId: {Guid.NewGuid().ToString().Substring(0, 5)}",
            //    testRunName: "RunName: RegressionTestRun",
            //    versionUnderTest: "Version: v0.3");


            _serviceCollection
                .AddSerilogLoggingProviderFactory()
                .AddSingleton<ILoggingProviderFactoriesSet>(provider =>
                {
                    ILoggingProviderFactoriesSet loggingProviderFactoriesSet = new LoggingProviderFactoriesSet();
                    ILoggingProviderFactory serilogLoggingProviderFactory = provider.GetRequiredService<SerilogLoggingProviderFactory>();

                    loggingProviderFactoriesSet.LoggingProviderFactories.TryAdd(typeof(SerilogLoggingProviderFactory).FullName!, serilogLoggingProviderFactory);

                    return loggingProviderFactoriesSet;
                })
                .AddNUnitTestLogger(nUnitTestLoggerConfigParameters);
        }
    }
}
