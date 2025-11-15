using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractorTab : IHaveIdentifiers, IMakeScreenshot
    {
        public IUiInteractor UiInteractor { get; }
        public void SwitchTo();
        public void GoTo(string url);
        public void GoTo(Uri uri);
        public string GetTabUrl();
        public string GetTabTitle();
        public void NavigateBack();
        public void NavigateForward();
        public void RefreshTab();

        //TODO public void CloseTab();

        public TUiPage GetPage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPage : IUiPage;
        public TUiPageService GetSingleUiPageService<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPageService : ISingleUiPageService;
        public TUiPageService GetMultipleUiPagesService<TUiPageService>() where TUiPageService : IMultipleUiPagesService;
        public TUiPageService Interact<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, params KeyValuePair<string, string>[] urlPlaceholderValues) where TUiPageService : ISingleUiPageService;

        public TNativeTab GetNativeTab<TNativeTab>() where TNativeTab : class;
    }
}
