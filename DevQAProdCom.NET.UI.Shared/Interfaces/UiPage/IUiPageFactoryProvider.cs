using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface IUiPageFactoryProvider
    {
        public IUiPageFactory ConstructNewPageFactory(IUiInteractorTab uiInteractorTab, INativeElementsSearcher nativeElementsSearcher, IExecuteJavaScript javaScriptExecutor,
            string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null,
            Dictionary<string, object>? nativeObjects = null);
    }
}
