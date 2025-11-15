using DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Files;
using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Selenium.Mappers;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Files;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Text;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractor;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractorsManager;
using DevQAProdCom.NET.UI.Selenium.WebDrivers.Interfaces;
using DevQAProdCom.NET.UI.Selenium.WebDrivers.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.DependencyInjection;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Files;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;
using DevQAProdCom.NET.UI.UiElements.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.UI.Selenium.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSelenium(this IServiceCollection serviceCollection, Func<IServiceProvider, ISeleniumWebDriverFactory>? customWebDriverFactoryFunc = null) //Core Di
        {
            if (customWebDriverFactoryFunc != null)
                serviceCollection.AddSingleton<ISeleniumWebDriverFactory>(customWebDriverFactoryFunc);
            else
                serviceCollection.AddSingleton<ISeleniumWebDriverFactory, DefaultSeleniumWebDriverFactory>();

            serviceCollection
                .AddSingleton<IUiPageFactoryProvider, UiPageFactoryProvider>()
                .AddTransient<IUiInteractor, SeleniumUiInteractor>()
                .AddTransient<IUiInteractorsManager, SeleniumUiInteractorsManager>()
                 .AddSingleton<IUiElementsInterfaceImplementationRegister>(provider =>
                 {

                     IUiElementsInterfaceImplementationRegister register = new UiElementsInterfaceImplementationRegister(typeof(SeleniumUiElement), typeof(SeleniumUiElementsList<>));
                     register.AddTypicalUiElementsPresetInterfaceImplementationEntries();

                     return register;
                 })
                .AddSingleton<IBehaviorProvider>(_ => new BehaviorProvider(serviceCollection))
                .AddSingleton<IUiElementBehaviorFactory, UiElementBehaviorFactory>()
                .AddSingleton<IUiPageBehaviorFactory, PageBehaviorFactory>()
                .AddSingleton<ISeleniumCookieMappers, SeleniumCookieMappers>()
                .AddUiInteractorsManagerAsyncLocalInstance();

            //It is done so that build method of service provider always returns the same instance (singleton not enough - cause in case of dynamic changes to service provider every time it builds new service collection it creates new singleton instance)
            //serviceCollection.AddSingleton<IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod>, SeleniumFindOptionSearchMethodsProvider>(); // NOT TO USE WILL FAIL BECAUSE AddSeleniumFindOptionSearchMethod  will not work
            IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> seleniumFindOptionSearcher = new SeleniumFindOptionSearchMethodsProvider();
            serviceCollection.AddSingleton<IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod>>((provider) => seleniumFindOptionSearcher);

            //ADD BEHAVIORS AUXILIARY SERVICES
            serviceCollection.AddSingleton<IKeyMatcher, SeleniumKeyMatcher>();

            //ADD DEFAULT CORE BEHAVIORS
            serviceCollection
                .AddUiInteractionBehavior<IFulfillTextBehavior, SeleniumUiElementFulfillTextBehavior>()
                .AddUiInteractionBehavior<IGetTextBehavior, SeleniumGetTextBehavior>()
                .AddUiInteractionBehavior<IUiElementMouseBehavior, SeleniumUiElementMouseBehavior>()
                .AddUiInteractionBehavior<IKeyboardBehavior, SeleniumKeyboardBehavior>()
                .AddUiInteractionBehavior<IClearTextBehavior, SeleniumClearTextBehavior>()
                .AddUiInteractionBehavior<IBaseMouseBehavior, SeleniumBaseMouseBehavior>()
                .AddUiInteractionBehavior<IUploadFilesBehavior, SeleniumUploadFilesBehavior>()
                .AddUiInteractionBehavior<IGetUploadedFilesListBehavior, SeleniumGetUploadedFilesListBehavior>()
                .AddUiInteractionBehavior<IUiElementDownloadBehavior, SeleniumUiElementDownloadBehavior>();


            return serviceCollection;
        }

        public static IServiceCollection AddSeleniumFindOptionSearchMethod<TImplementation>(this IServiceCollection serviceCollection)
            where TImplementation : class, ISeleniumFindOptionSearchMethod
        {
            IServiceProvider provider = serviceCollection.BuildServiceProvider();
            IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> searcher = provider.GetRequiredService<IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod>>();
            ISeleniumFindOptionSearchMethod? method = provider.GetService<TImplementation>(); // it is done for cases for not parametreless constructor

            if (method != null)
                searcher.RegisterFindOptionSearchMethod(method);
            else
                searcher.RegisterFindOptionSearchMethod<TImplementation>();

            return serviceCollection;
        }
    }
}
