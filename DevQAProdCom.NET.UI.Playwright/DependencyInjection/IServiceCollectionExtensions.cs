using DevQAProdCom.NET.UI.Playwright.Browsers.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Browsers.OperativeClasses;
using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Mappers;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Files;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Text;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiInteractor;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiInteractorsManager;
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

namespace DevQAProdCom.NET.UI.Playwright.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigurePlaywright(this IServiceCollection serviceCollection, Func<IServiceProvider, IPlaywrightBrowserFactory>? customBrowserFactoryFunc = null) //Core Di
        {
            if (customBrowserFactoryFunc != null)
                serviceCollection.AddSingleton<IPlaywrightBrowserFactory>(customBrowserFactoryFunc);
            else
                serviceCollection.AddSingleton<IPlaywrightBrowserFactory, DefaultPlaywrightBrowserFactory>();

            serviceCollection.AddSingleton<IUiPageFactoryProvider, UiPageFactoryProvider>()
                .AddTransient<IUiInteractor, PlaywrightUiInteractor>()
                .AddTransient<IUiInteractorsManager, PlaywrightUiInteractorsManager>()
                .AddSingleton<IUiElementsInterfaceImplementationRegister>(provider =>
                {
                    IUiElementsInterfaceImplementationRegister register = new UiElementsInterfaceImplementationRegister(typeof(PlaywrightUiElement), typeof(PlaywrightUiElementsList<>));
                    register.AddTypicalUiElementsPresetInterfaceImplementationEntries();

                    return register;
                })
                .AddSingleton<IBehaviorProvider>(_ => new BehaviorProvider(serviceCollection))
                .AddSingleton<IUiElementBehaviorFactory, UiElementBehaviorFactory>()
                .AddSingleton<IUiPageBehaviorFactory, PageBehaviorFactory>()
                .AddSingleton<IPlaywrightCookieMappers, PlaywrightCookieMappers>()
                .AddUiInteractorsManagerAsyncLocalInstance();

            //It is done so that build method of service provider always returns the same instance (singleton not enough - cause in case of dynamic changes to service provider every time it builds new service collection it creates new singleton instance)
            IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> playwrightFindOptionSearcher = new PlaywrightFindOptionSearchMethodsProvider();
            serviceCollection.AddSingleton<IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod>>((provider) => playwrightFindOptionSearcher);

            //ADD DEFAULT CORE BEHAVIORS
            serviceCollection.AddSingleton<IKeyMatcher, PlaywrightKeyMatcher>();

            serviceCollection
                .AddUiInteractionBehavior<IFulfillTextBehavior, PlaywrightUiElementFulfillTextBehavior>()
                .AddUiInteractionBehavior<IGetTextBehavior, PlaywrightGetTextBehavior>()
                .AddUiInteractionBehavior<IUiElementMouseBehavior, PlaywrightUiElementMouseBehavior>()
                .AddUiInteractionBehavior<IKeyboardBehavior, PlaywrightKeyboardBehavior>()
                .AddUiInteractionBehavior<IClearTextBehavior, PlaywrightClearTextBehavior>()
                .AddUiInteractionBehavior<IBaseMouseBehavior, PlaywrightBaseMouseBehavior>()
                .AddUiInteractionBehavior<IUploadFilesBehavior, PlaywrightUploadFilesBehavior>()
                .AddUiInteractionBehavior<IGetUploadedFilesListBehavior, PlaywrightGetUploadedFilesListBehavior>()
                .AddUiInteractionBehavior<IUiElementDownloadBehavior, PlaywrightUiElementDownloadBehavior>();

            return serviceCollection;
        }

        public static IServiceCollection AddPlaywrightFindOptionSearchMethod<TImplementation>(this IServiceCollection serviceCollection)
            where TImplementation : class, IPlaywrightFindOptionSearchMethod
        {
            IServiceProvider provider = serviceCollection.BuildServiceProvider();
            IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> searcher = provider.GetRequiredService<IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod>>();
            IPlaywrightFindOptionSearchMethod? method = provider.GetService<TImplementation>(); // it is done for cases for not parametreless constructor

            if (method != null)
                searcher.RegisterFindOptionSearchMethod(method);
            else
                searcher.RegisterFindOptionSearchMethod<TImplementation>();

            return serviceCollection;
        }
    }
}
