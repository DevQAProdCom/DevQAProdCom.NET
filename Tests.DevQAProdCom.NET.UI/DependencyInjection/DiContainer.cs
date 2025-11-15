using ApplicationName.QA.Global.DependencyInjection;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.DependencyInjection;
using DevQAProdCom.NET.UI.Playwright.DependencyInjection;
using DevQAProdCom.NET.UI.Selenium.DependencyInjection;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using Microsoft.Extensions.DependencyInjection;
using Tests.DevQAProdCom.NET.UI.Configurations;
using Tests.DevQAProdCom.NET.UI.TestClasses;
using ApplicationName.QA.TestsBasis.DependencyInjection;

namespace Tests.DevQAProdCom.NET.UI.DependencyInjection
{
    internal class DiContainer : GlobalDiContainer
    {
        private static readonly Lazy<DiContainer> _instance = new Lazy<DiContainer>(() => new DiContainer());
        public static DiContainer Instance => _instance.Value;

        public static UiInteractorTechnology CurrentTechnology = UiInteractorTechnology.Playwright;

        protected override string AssemblyName => this.GetType().Assembly.GetName().Name ?? "UnknownAssembly";

        protected override void ConfigureServices()
        {
            base.ConfigureServices();
            AddUiInteractorTechnology();
        }

        protected override void ConfigureExternalDiContainers()
        {
            //Configure Service Providers of External Projects DiContainers with based on this Project Configuration
            _serviceProvider.ConfigureNUnitLoggingExtensionDiContainer();
            _serviceProvider.ConfigureApplicationNameQATestsBasisDiContainer();
        }

        private void AddUiInteractorTechnology()
        {
            //Form DI
            if (CurrentTechnology == UiInteractorTechnology.Playwright)
            {
                _serviceCollection.ConfigurePlaywright(customBrowserFactoryFunc: (provider) => { return new PlaywrightBrowserFactory(provider.GetRequiredService<ILogger>()); })
                    .AddSingleton<TestClassForDiInjection>()
                    .AddSingleton<PlaywrightCustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute>()
                    .AddPlaywrightFindOptionSearchMethod<PlaywrightCustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute>()
                    .AddPlaywrightFindOptionSearchMethod<PlaywrightCustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute>();
            }
            else if (CurrentTechnology == UiInteractorTechnology.Selenium)
            {
                _serviceCollection.ConfigureSelenium(customWebDriverFactoryFunc: (provider) => { return new SeleniumWebDriverFactory(provider.GetRequiredService<ILogger>()); })
                    .AddSingleton<TestClassForDiInjection>()
                    .AddSingleton<SeleniumCustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute>()
                    .AddSeleniumFindOptionSearchMethod<SeleniumCustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute>()
                    .AddSeleniumFindOptionSearchMethod<SeleniumCustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute>();
            }
        }
    }
}
