using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractor : IHaveIdentifiers, IOperateWithCookies, IUiInteractorMakeScreenshots, IAddBehavior, IHaveNativeObjects
    {
        public IUiInteractorsManager UiInteractorsManager { get; }

        public void Launch();
        public bool IsInteractable();
        public void Dispose();

        #region Tabs

        public IUiInteractorTab GetTab(string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void CloseTab(string tabName = SharedUiConstants.DefaultUiInteractorTab);

        public void GoTo(string url, string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void GoTo(Uri uri, string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public string GetTabUrl(string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public string GetTabTitle(string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void NavigateBack(string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void NavigateForward(string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void RefreshTab(string tabName = SharedUiConstants.DefaultUiInteractorTab);

        public TUiPage GetPage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiPage : IUiPage;

        public TUiElement Find<TUiElement>(string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement;
        public TUiElement Find<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement;
        public TUiElement Find<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> FindAll<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> FindAll<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement;

        public TUiPageService GetSingleUiPageService<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiPageService : ISingleUiPageService;
        public TUiPageService GetMultipleUiPagesService<TUiPageService>(string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiPageService : IMultipleUiPagesService;
        public TUiPageService Interact<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, string tabName = SharedUiConstants.DefaultUiInteractorTab, params KeyValuePair<string, string>[] urlPlaceholderValues) where TUiPageService : ISingleUiPageService;
        public TNativeTab GetNativeTab<TNativeTab>(string tabName = SharedUiConstants.DefaultUiInteractorTab) where TNativeTab : class;

        #endregion Tabs

        #region Actions

        #region KeyboardActions

        public void KeysDown(string tabName = SharedUiConstants.DefaultUiInteractorTab, params string[] keys);
        public void KeysUp(string tabName = SharedUiConstants.DefaultUiInteractorTab, params string[] keys);
        public void PressKeysSequentially(string tabName = SharedUiConstants.DefaultUiInteractorTab, params string[] keys);
        public void PressKeysSimultaneously(string keysCombination, string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void PressKeysSimultaneously(string tabName = SharedUiConstants.DefaultUiInteractorTab, params string[] keys);

        #endregion KeyboardActions

        #region MouseActions

        public void MouseMove(float x, float y, string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void MouseMoveJs(float x, float y, string tabName = SharedUiConstants.DefaultUiInteractorTab);

        public void MouseScroll(float deltaX, float deltaY, string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void MouseScroll(float deltaX, float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec,
            string tabName = SharedUiConstants.DefaultUiInteractorTab);

        public void MouseScrollVertically(float deltaY, string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void MouseScrollVertically(float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec,
            string tabName = SharedUiConstants.DefaultUiInteractorTab);

        public void MouseScrollHorizontally(float deltaX, string tabName = SharedUiConstants.DefaultUiInteractorTab);
        public void MouseScrollHorizontally(float deltaX, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec,
            string tabName = SharedUiConstants.DefaultUiInteractorTab);

        #endregion MouseActions

        #endregion Actions

        public string? DownloadsDefaultDirectory { get; set; }
        public Dictionary<string, string> GetLocalStorageData(string tabName = SharedUiConstants.DefaultUiInteractorTab);
    }
}
