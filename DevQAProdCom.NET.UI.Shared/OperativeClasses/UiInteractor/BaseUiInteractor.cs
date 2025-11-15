using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public abstract class BaseUiInteractor : IUiInteractor
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public IUiInteractorsManager UiInteractorsManager { get; }

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

        public BaseUiInteractor(ILogger log, IUiInteractorsManager uiInteractorsManager)
        {
            _log = log;
            UiInteractorsManager = uiInteractorsManager;
            _tabs = new();
        }

        #region Tabs

        public IUiInteractorTab GetTab(string name = SharedUiConstants.DefaultTab)
        {
            var tab = _tabs.SingleOrDefault(x => x.Name == name);

            if (tab == null)
            {
                tab = CreateTab(name);
                _tabs.Add(tab);
            }

            return tab;
        }

        protected abstract IUiInteractorTab CreateTab(string name = SharedUiConstants.DefaultTab);
        protected abstract void CloseTab(IUiInteractorTab tab);

        public void CloseTab(string name = SharedUiConstants.DefaultTab)
        {
            var tab = GetTab(name);
            CloseTab(tab);
            _tabs.Remove(tab);
        }
        public TUiPageService GetSingleUiPageService<TUiPageService>(string tabName = SharedUiConstants.DefaultTab, string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPageService : ISingleUiPageService
        {
            var tab = GetTab(tabName);
            var service = tab.GetSingleUiPageService<TUiPageService>(applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri);
            return service;
        }

        public TUiPageService GetMultipleUiPagesService<TUiPageService>(string tabName = SharedUiConstants.DefaultTab) where TUiPageService : IMultipleUiPagesService
        {
            var tab = GetTab(tabName);
            var service = tab.GetMultipleUiPagesService<TUiPageService>();
            return service;
        }

        public TUiPageService Interact<TUiPageService>(string tabName = SharedUiConstants.DefaultTab, string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, params KeyValuePair<string, string>[] urlPlaceholderValues) where TUiPageService : ISingleUiPageService
        {
            var tab = GetTab(tabName);
            var service = tab.GetSingleUiPageService<TUiPageService>(applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri);
            service.GoToPage(urlPlaceholderValues);
            return service;
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

        public abstract void LaunchInteractor();
        public abstract bool IsInteractorInteractable();
        public abstract void DisposeInteractor(); //TerminateInteractor

        #region Screenshots

        public void MakeScreenshot(string? directoryPath = null, string? fileNamePrefix = null)
        {
            foreach (IUiInteractorTab tab in _tabs)
                tab.MakeScreenshot(directoryPath, fileNamePrefix);
        }

        #endregion Screenshots
    }
}
