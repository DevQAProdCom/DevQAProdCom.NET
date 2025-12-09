using DevQAProdCom.NET.UI.Playwright.Browsers.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Browsers.OperativeClasses;
using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Mappers;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Files;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Text;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiInteractor;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiInteractorsManager;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiPage.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiPage.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.DependencyInjection;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Extensions;
using DevQAProdCom.NET.UI.UiElements.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.UI.Playwright.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigurePlaywright(this IServiceCollection serviceCollection, Func<IServiceProvider, IPlaywrightBrowserFactory>? customBrowserFactoryFunc = null,
            Func<string>? getCurrentTestIdentifierFunc = null, Func<string>? getCurrentFeatureIdentifierFunc = null) //Core Di
        {
            serviceCollection.AddBaseUiInteractionServices(getCurrentTestIdentifierFunc: getCurrentTestIdentifierFunc, getCurrentFeatureIdentifierFunc: getCurrentFeatureIdentifierFunc);

            if (customBrowserFactoryFunc != null)
                serviceCollection.AddSingleton<IPlaywrightBrowserFactory>(customBrowserFactoryFunc);
            else
                serviceCollection.AddSingleton<IPlaywrightBrowserFactory, DefaultPlaywrightBrowserFactory>();

            serviceCollection
                .AddTransient<IUiInteractor, PlaywrightUiInteractor>()
                .AddTransient<IUiInteractorsManager, PlaywrightUiInteractorsManager>()
                .AddSingleton<IUiElementsInterfaceImplementationRegister>(provider =>
                {
                    IUiElementsInterfaceImplementationRegister register = new UiElementsInterfaceImplementationRegister(typeof(PlaywrightUiElement), typeof(PlaywrightUiElementsList<>));
                    register.AddTypicalUiElementsPresetInterfaceImplementationEntries();
                    register.RegisterUiElementImplementationType<ISelect, PlaywrightUiElementSelect>();

                    return register;
                })
                .AddSingleton<IPlaywrightCookieMappers, PlaywrightCookieMappers>();

            //It is done so that build method of service provider always returns the same instance (singleton not enough - cause in case of dynamic changes to service provider every time it builds new service collection it creates new singleton instance)
            IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> playwrightFindOptionSearcher = new PlaywrightFindOptionSearchMethodsProvider();
            serviceCollection.AddSingleton<IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod>>((provider) => playwrightFindOptionSearcher);

            //ADD DEFAULT CORE BEHAVIORS
            serviceCollection.AddSingleton<IKeyMatcher, PlaywrightKeyMatcher>();

            serviceCollection
                //Files Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorDownloadFile, PlaywrightUiElementBehaviorDownloadFile>()
                .AddUiInteractionBehavior<IUiElementBehaviorGetUploadedFilesList, PlaywrightUiElementBehaviorGetUploadedFilesList>()
                .AddUiInteractionBehavior<IUiElementBehaviorUploadFiles, PlaywrightUiElementBehaviorUploadFiles>()
                //Text Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorClearText, PlaywrightUiElementBehaviorClearText>()
                .AddUiInteractionBehavior<IUiElementBehaviorGetInputText, PlaywrightUiElementBehaviorGetInputText>()
                .AddUiInteractionBehavior<IUiElementBehaviorSetText, PlaywrightUiElementBehaviorSetText>()
                .AddUiInteractionBehavior<IUiElementBehaviorTypeText, PlaywrightUiElementBehaviorTypeText>()
                //Mouse Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorClick, PlaywrightUiElementBehaviorClick>()
                .AddUiInteractionBehavior<IUiElementBehaviorContextClick, PlaywrightUiElementBehaviorContextClick>()
                .AddUiInteractionBehavior<IUiElementBehaviorDoubleClick, PlaywrightUiElementBehaviorDoubleClick>()
                .AddUiInteractionBehavior<IUiElementBehaviorDragAndDrop, PlaywrightUiElementBehaviorDragAndDrop>()
                .AddUiInteractionBehavior<IUiElementBehaviorDragAndDropByOffset, PlaywrightUiElementBehaviorDragAndDropByOffset>()
                .AddUiInteractionBehavior<IUiElementBehaviorMouseDown, PlaywrightUiElementBehaviorMouseDown>()
                .AddUiInteractionBehavior<IUiElementBehaviorMouseHover, PlaywrightUiElementBehaviorMouseHover>()
                .AddUiInteractionBehavior<IUiElementBehaviorMouseUp, PlaywrightUiElementBehaviorMouseUp>()

                .AddUiInteractionBehavior<IUiPageBehaviorMouseMove, PlaywrightUiPageBehaviorMouseMove>()
                .AddUiInteractionBehavior<IUiPageBehaviorMouseScroll, PlaywrightUiPageBehaviorMouseScroll>()

                //Keyboard Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorSendText, PlaywrightUiElementBehaviorSendText>()

                .AddUiInteractionBehavior<IUiPageBehaviorKeysDown, PlaywrightUiPageBehaviorKeysDown>()
                .AddUiInteractionBehavior<IUiPageBehaviorKeysUp, PlaywrightUiPageBehaviorKeysUp>()
                .AddUiInteractionBehavior<IUiPageBehaviorPressKeysSequentially, PlaywrightUiPageBehaviorPressKeysSequentially>();

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
