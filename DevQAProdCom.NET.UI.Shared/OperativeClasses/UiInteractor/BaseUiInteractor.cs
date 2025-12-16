using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public abstract class BaseUiInteractor : IUiInteractor
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public IUiInteractorsManager UiInteractorsManager { get; }
        protected readonly IUiInteractorBehaviorFactory UiInteractorBehaviorFactory;
        protected readonly IUiInteractorTabBehaviorFactory UiInteractorTabBehaviorFactory;

        //TODO Take from configuration when created
        public DateTime? Created { get; set; }
        public DateTime? ExpirationTime => Created.HasValue && TimeToLive.HasValue ? Created.Value.Add(TimeToLive.Value) : null;
        public TimeSpan? TimeToLive { get; set; } = TimeSpan.FromMinutes(10);
        public Dictionary<string, object>? Data { get; set; }

        protected List<IUiInteractorTab> _tabs;
        protected ILogger _log;

        public string? _downloadsDefaultDirectory;
        public string? DownloadsDefaultDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_downloadsDefaultDirectory))
                    throw new Exception("Downloads Default Directory of UiInteractor is not set.");

                if (!string.IsNullOrEmpty(_downloadsDefaultDirectory) && !Directory.Exists(_downloadsDefaultDirectory))
                    Directory.CreateDirectory(_downloadsDefaultDirectory);

                return _downloadsDefaultDirectory;
            }

            set
            {
                _downloadsDefaultDirectory = value;
            }
        }

        private System.Timers.Timer? _keepAliveTimer;

        // Is added to keep remotes session alive. Cause they may have max expiration time and idle timeout. This one will call IsInteractable every 30 seconds to reset idle timeout. 
        private void StartKeepAliveTimer()
        {
            if (_keepAliveTimer != null)
                return;

            _keepAliveTimer = new System.Timers.Timer(30000); // 30 seconds
            _keepAliveTimer.Elapsed += (s, e) =>
            {
                try
                {
                    IsInteractable();
                }
                catch
                {
                    // Optionally log or handle exceptions
                }
            };
            _keepAliveTimer.AutoReset = true;
            _keepAliveTimer.Start();
        }

        public BaseUiInteractor(ILogger log, IUiInteractorsManager uiInteractorsManager, IUiInteractorBehaviorFactory uiInteractorBehaviorFactory, IUiInteractorTabBehaviorFactory uiInteractorTabBehaviorFactory)
        {
            _log = log;
            UiInteractorsManager = uiInteractorsManager;
            _tabs = new();
            UiInteractorBehaviorFactory = uiInteractorBehaviorFactory;
            UiInteractorTabBehaviorFactory = uiInteractorTabBehaviorFactory;
            StartKeepAliveTimer();//TODO Add Config with parameter IsRemote
        }

        #region Tabs

        public IUiInteractorTab GetTab(string name = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = _tabs.SingleOrDefault(x => x.Name == name);

            if (tab == null)
            {
                tab = CreateTab(name);
                _tabs.Add(tab);
            }

            return tab;
        }

        protected abstract IUiInteractorTab CreateTab(string name = SharedUiConstants.DefaultUiInteractorTab);
        protected abstract void CloseTab(IUiInteractorTab tab);

        public void CloseTab(string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            CloseTab(tab);
            _tabs.Remove(tab);
        }

        public void GoTo(string url, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.GoTo(url);
        }

        public void GoTo(Uri uri, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.GoTo(uri);
        }

        public string GetTabUrl(string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            return tab.GetTabUriAsString();
        }

        public string GetTabTitle(string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            return tab.GetTabTitle();
        }

        public void NavigateBack(string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.NavigateBack();
        }

        public void NavigateForward(string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.NavigateForward();
        }

        public void RefreshTab(string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.Refresh();
        }

        public TUiPage GetPage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiPage : IUiPage
        {
            var tab = GetTab(tabName);
            var page = tab.GetPage<TUiPage>(applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri);
            return page;
        }

        public TUiElement Find<TUiElement>(string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement
        {
            var tab = GetTab(tabName);
            return tab.Find<TUiElement>(name: name);
        }

        public TUiElement Find<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement
        {
            var tab = GetTab(tabName);
            return tab.Find<TUiElement>(method, criteria, parentUiElement, name: name);
        }

        public TUiElement Find<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement
        {
            var tab = GetTab(tabName);
            return tab.Find<TUiElement>(findOptions, parentUiElement, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement
        {
            var tab = GetTab(tabName);
            return tab.FindAll<TUiElement>(name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement
        {
            var tab = GetTab(tabName);
            return tab.FindAll<TUiElement>(method, criteria, parentUiElement, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiElement : IUiElement
        {
            var tab = GetTab(tabName);
            return tab.FindAll<TUiElement>(findOptions, parentUiElement, name: name);
        }

        public TUiPageService GetSingleUiPageService<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiPageService : ISingleUiPageService
        {
            var tab = GetTab(tabName);
            var service = tab.GetSingleUiPageService<TUiPageService>(applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri);
            return service;
        }

        public TUiPageService GetMultipleUiPagesService<TUiPageService>(string tabName = SharedUiConstants.DefaultUiInteractorTab) where TUiPageService : IMultipleUiPagesService
        {
            var tab = GetTab(tabName);
            var service = tab.GetMultipleUiPagesService<TUiPageService>();
            return service;
        }

        public TUiPageService Interact<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, string tabName = SharedUiConstants.DefaultUiInteractorTab, params KeyValuePair<string, string>[] urlPlaceholderValues) where TUiPageService : ISingleUiPageService
        {
            var tab = GetTab(tabName);
            var service = tab.GetSingleUiPageService<TUiPageService>(applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri);
            service.GoToPage(urlPlaceholderValues);
            return service;
        }

        public TNativeTab GetNativeTab<TNativeTab>(string tabName = SharedUiConstants.DefaultUiInteractorTab) where TNativeTab : class
        {
            var tab = GetTab(tabName);
            return tab.GetNativeTab<TNativeTab>();
        }

        #endregion Tabs

        #region Cookies

        public abstract void SetCookie(string name, string value, string domain, string? path = SharedUiConstants.DefaultCookiePathValue);
        public abstract void SetCookie(IUiInteractorCookie cookie);
        public void SetCookies(params IUiInteractorCookie[] cookies)
        {
            foreach (var cookie in cookies)
                SetCookie(cookie);
        }

        public abstract IUiInteractorCookie? GetCookie(string name);
        public abstract List<IUiInteractorCookie> GetAllCookies();

        public abstract void ClearCookies(params string[] names);
        public abstract void ClearAllCookies();

        #endregion Cookies

        public abstract void Launch();
        public abstract bool IsInteractable();
        public abstract void Dispose();  //TerminateInteractor
        public void RecreateOrCurrent()
        {
            if (DateTime.UtcNow >= ExpirationTime)
                Recreate();
        }

        public abstract void Recreate();

        #region Screenshots

        public List<IUiInteractorTabScreenshotModel> MakeScreenshots(string? directoryPath = null, string? fileNamePrefix = null)
        {
            List<IUiInteractorTabScreenshotModel> screenshots = new();

            foreach (IUiInteractorTab tab in _tabs)
            {
                var screenshot = tab.MakeScreenshot(directoryPath, fileNamePrefix);
                screenshots.Add(screenshot);
            }

            return screenshots;
        }

        #endregion Screenshots

        #region Actions

        #region KeyboardActions

        public void KeysDown(string tabName = SharedUiConstants.DefaultUiInteractorTab, params string[] keys)
        {
            var tab = GetTab(tabName);
            tab.KeysDown(keys);
        }

        public void KeysUp(string tabName = SharedUiConstants.DefaultUiInteractorTab, params string[] keys)
        {
            var tab = GetTab(tabName);
            tab.KeysUp(keys);
        }

        public void PressKeysSequentially(string tabName = SharedUiConstants.DefaultUiInteractorTab, params string[] keys)
        {
            var tab = GetTab(tabName);
            tab.PressKeysSequentially(keys);
        }

        public void PressKeysSimultaneously(string keysCombination, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.PressKeysSequentially(keysCombination);
        }

        public void PressKeysSimultaneously(string tabName = SharedUiConstants.DefaultUiInteractorTab, params string[] keys)
        {
            var tab = GetTab(tabName);
            tab.PressKeysSimultaneously(keys);
        }

        #endregion KeyboardActions

        #region MouseActions

        public void MouseMove(float x, float y, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.MouseMove(x, y);
        }

        public void MouseMoveJs(float x, float y, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.MouseMoveJs(x, y);
        }

        public void MouseScroll(float deltaX, float deltaY, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.MouseScroll(deltaX, deltaY);
        }

        public void MouseScroll(float deltaX, float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec,
            string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.MouseScroll(deltaX, deltaY, untilCondition, timeoutSec: timeoutSec, pollingIntervalSec: pollingIntervalSec);
        }

        public void MouseScrollVertically(float deltaY, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.MouseScrollVertically(deltaY);
        }

        public void MouseScrollVertically(float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec,
            string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.MouseScrollVertically(deltaY, untilCondition, timeoutSec: timeoutSec, pollingIntervalSec: pollingIntervalSec);
        }

        public void MouseScrollHorizontally(float deltaX, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.MouseScrollHorizontally(deltaX);
        }

        public void MouseScrollHorizontally(float deltaX, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec,
            string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            tab.MouseScrollHorizontally(deltaX, untilCondition, timeoutSec: timeoutSec, pollingIntervalSec: pollingIntervalSec);
        }

        #endregion MouseActions

        #endregion Actions

        public Dictionary<string, string> GetLocalStorageData(string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var tab = GetTab(tabName);
            return tab.GetLocalStorageData();
        }

        public Dictionary<string, object> NativeObjects { get; internal set; } = new();
        public T AddBehavior<T>(params KeyValuePair<string, object>[]? auxiliaryParams) where T : IBehavior
        {
            return UiInteractorBehaviorFactory.Create<T>(this, auxiliaryParams.Union(NativeObjects).ToArray());
        }
    }
}
