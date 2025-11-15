using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public class UiPageFactoryProvider(ILogger log, IUiPageBehaviorFactory uiPageBehaviorFactory, IUiElementBehaviorFactory uiElementBehaviorFactory,
            IUiElementsInterfaceImplementationRegister uiElementsInterfaceImplementationRegister) : IUiPageFactoryProvider
    {
        public IUiPageFactory ConstructNewPageFactory(IUiInteractorTab uiInteractorTab, INativeElementsSearcher nativeElementsSearcher, IExecuteJavaScript javaScriptExecutor,
            string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null,
            Dictionary<string, object>? nativeObjects = null)
        {
            return new UiPageFactory(
                log: log,
                 uiInteractorTab: uiInteractorTab,

                 uiPageBehaviorFactory: uiPageBehaviorFactory,
                 uiElementBehaviorFactory: uiElementBehaviorFactory,

                 nativeElementsSearcher: nativeElementsSearcher,
                 javaScriptExecutor: javaScriptExecutor,
                 uiElementsInterfaceImplementationRegister: uiElementsInterfaceImplementationRegister,

                 applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri,
                 nativeObjects: nativeObjects
                );
        }
    }
}
