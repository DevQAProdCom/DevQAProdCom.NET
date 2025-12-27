using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Selenium.Mappers;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Files;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Text;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractor;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractor.Behaviors;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractorsManager;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiPage.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiPage.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Selenium.WebDrivers.Interfaces;
using DevQAProdCom.NET.UI.Selenium.WebDrivers.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.DependencyInjection;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Extensions;
using DevQAProdCom.NET.UI.UiElements.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.UI.Selenium.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSelenium(this IServiceCollection serviceCollection, Func<IServiceProvider, ISeleniumWebDriverFactory>? customWebDriverFactoryFunc = null,
            Func<string>? getCurrentTestIdentifierFunc = null, Func<string>? getCurrentFeatureIdentifierFunc = null) //Core Di
        {
            serviceCollection.AddBaseUiInteractionServices(getCurrentTestIdentifierFunc: getCurrentTestIdentifierFunc, getCurrentFeatureIdentifierFunc: getCurrentFeatureIdentifierFunc);

            if (customWebDriverFactoryFunc != null)
                serviceCollection.AddSingleton<ISeleniumWebDriverFactory>(customWebDriverFactoryFunc);
            else
                serviceCollection.AddSingleton<ISeleniumWebDriverFactory, DefaultSeleniumWebDriverFactory>();

            serviceCollection
                .AddTransient<IUiInteractorsManager, SeleniumUiInteractorsManager>()
                .AddSingleton<IUiElementsInterfaceImplementationRegister>(provider =>
                 {

                     IUiElementsInterfaceImplementationRegister register = new UiElementsInterfaceImplementationRegister(typeof(SeleniumUiElement), typeof(SeleniumUiElementsList<>));
                     register.AddTypicalUiElementsPresetInterfaceImplementationEntries();
                     register.RegisterUiElementImplementationType<ISelect, SeleniumUiElementSelect>();

                     return register;
                 })
                .AddSingleton<ISeleniumCookieMappers, SeleniumCookieMappers>();

            //It is done so that build method of service provider always returns the same instance (singleton not enough - cause in case of dynamic changes to service provider every time it builds new service collection it creates new singleton instance)
            //serviceCollection.AddSingleton<IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod>, SeleniumFindOptionSearchMethodsProvider>(); // NOT TO USE WILL FAIL BECAUSE AddSeleniumFindOptionSearchMethod  will not work
            IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> seleniumFindOptionSearcher = new SeleniumFindOptionSearchMethodsProvider();
            serviceCollection.AddSingleton<IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod>>((provider) => seleniumFindOptionSearcher);

            //ADD BEHAVIORS AUXILIARY SERVICES
            serviceCollection.AddSingleton<IKeyMatcher, SeleniumKeyMatcher>();

            //ADD DEFAULT CORE BEHAVIORS
            serviceCollection
                //Files Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorDownloadFile, SeleniumUiElementBehaviorDownloadFile>()
                .AddUiInteractionBehavior<IUiElementBehaviorGetUploadedFilesList, SeleniumUiElementBehaviorGetUploadedFilesList>()
                .AddUiInteractionBehavior<IUiElementBehaviorUploadFiles, SeleniumUiElementBehaviorUploadFiles>()
                //Text Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorClearText, SeleniumUiElementBehaviorClearText>()
                .AddUiInteractionBehavior<IUiElementBehaviorGetInputText, SeleniumUiElementBehaviorGetInputText>()
                .AddUiInteractionBehavior<IUiElementBehaviorSetText, SeleniumUiElementBehaviorSetText>()
                .AddUiInteractionBehavior<IUiElementBehaviorTypeText, SeleniumUiElementBehaviorTypeText>()
                //Mouse Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorClick, SeleniumUiElementBehaviorClick>()
                .AddUiInteractionBehavior<IUiElementBehaviorContextClick, SeleniumUiElementBehaviorContextClick>()
                .AddUiInteractionBehavior<IUiElementBehaviorDoubleClick, SeleniumUiElementBehaviorDoubleClick>()
                .AddUiInteractionBehavior<IUiElementBehaviorDragAndDrop, SeleniumUiElementBehaviorDragAndDrop>()
                .AddUiInteractionBehavior<IUiElementBehaviorDragAndDropByOffset, SeleniumUiElementBehaviorDragAndDropByOffset>()
                .AddUiInteractionBehavior<IUiElementBehaviorMouseDown, SeleniumUiElementBehaviorMouseDown>()
                .AddUiInteractionBehavior<IUiElementBehaviorMouseHover, SeleniumUiElementBehaviorMouseHover>()
                .AddUiInteractionBehavior<IUiElementBehaviorMouseUp, SeleniumUiElementBehaviorMouseUp>()

                .AddUiInteractionBehavior<IUiPageBehaviorMouseMove, SeleniumUiPageBehaviorMouseMove>()
                .AddUiInteractionBehavior<IUiPageBehaviorMouseScroll, SeleniumUiPageBehaviorMouseScroll>()

                //Keyboard Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorSendText, SeleniumUiElementBehaviorSendText>()

                .AddUiInteractionBehavior<IUiPageBehaviorKeysDown, SeleniumUiPageBehaviorKeysDown>()
                .AddUiInteractionBehavior<IUiPageBehaviorKeysUp, SeleniumUiPageBehaviorKeysUp>()
                .AddUiInteractionBehavior<IUiPageBehaviorPressKeysSequentially, SeleniumUiPageBehaviorPressKeysSequentially>()
                .AddUiInteractionBehavior<IUiPageBehaviorMouseDown, SeleniumUiPageBehaviorMouseDown>()
                .AddUiInteractionBehavior<IUiPageBehaviorMouseUp, SeleniumUiPageBehaviorMouseUp>()

                .AddUiInteractionBehavior<IUiInteractorBehaviorGetRemoteSessionId, SeleniumUiInteractorBehaviorGetRemoteSessionId>();

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
