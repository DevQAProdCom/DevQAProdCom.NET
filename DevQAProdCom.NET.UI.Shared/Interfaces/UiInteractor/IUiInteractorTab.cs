using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Other;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Scroll;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Traits.Mouse;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractorTab :
        IHaveIdentifiers,
        IHaveNativeObjects,
        IUiInteractorTabMakeScreenshot,
        IFindUiElementsWithParentParameter,
        IFindUiElementsFromTheBeginningOfDom,
        IExecuteJavaScript,
        IAddBehavior,

        IUiInteractionTraitKeysDown,
        IUiInteractionTraitKeysUp,
        IUiInteractionTraitPressKey,
        IUiInteractionTraitPressKeysSequentially,
        IUiInteractionTraitPressKeysSimultaneously,

        IUiPageTraitMouseMove,
        IUiPageTraitMouseMoveJs,
        IUiPageTraitMouseScroll,
        IUiPageTraitMouseScrollHorizontally,
        IUiPageTraitMouseScrollVertically,

        IUiInteractionTraitScrollToTopSmoothlyJs,

        IUiInteractionTraitRefresh
    {
        public IUiInteractor UiInteractor { get; }
        public void SwitchTo();
        public void GoTo(string url);
        public void GoTo(Uri uri);
        public string GetTabUriAsString();
        public Uri GetTabUri();
        public string GetTabTitle();
        public void NavigateBack();
        public void NavigateForward();

        //public void CloseTab(); //TODO or NOT TODO? Implemented on UiInteractor level

        public TUiPage GetPage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPage : IUiPage;
        public TUiPageService GetSingleUiPageService<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPageService : ISingleUiPageService;
        public TUiPageService GetMultipleUiPagesService<TUiPageService>() where TUiPageService : IMultipleUiPagesService;
        public TUiPageService Interact<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, params KeyValuePair<string, string>[] urlPlaceholderValues) where TUiPageService : ISingleUiPageService;

        public TNativeTab GetNativeTab<TNativeTab>() where TNativeTab : class;
        public Dictionary<string, string> GetLocalStorageData();
    }
}
