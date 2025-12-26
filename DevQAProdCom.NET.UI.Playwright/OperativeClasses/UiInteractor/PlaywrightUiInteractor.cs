using System.Data;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Browsers.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Browsers.OperativeClasses;
using DevQAProdCom.NET.UI.Playwright.Constants;
using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using Microsoft.Playwright;
using PlaywrightCookie = Microsoft.Playwright.Cookie;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiInteractor
{
    public class PlaywrightUiInteractor : BaseUiInteractor, IPlaywrightUiInteractor
    {
        private readonly IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> _findOptionSearchMethodsProvider;
        private readonly IPlaywrightCookieMappers _cookieMappers;
        private readonly IPlaywrightBrowserFactory _browserFactory;
        private readonly IUiPageFactoryProvider _uiPageFactoryProvider;
        private IPlaywrightUiInteractorConfiguration? _uiInteractorConfiguration;


        //Can be set/substituted in tests in case each test needs browser with different configuration
        public IBrowser? Browser { get; set; }
        public IBrowserContext? BrowserContext { get; set; }

        public PlaywrightUiInteractor(ILogger log, IUiInteractorsManager uiInteractorsManager, IPlaywrightBrowserFactory browserFactory,
            IUiPageFactoryProvider uiPageFactoryProvider, IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> findOptionSearchMethodsProvider,
            IPlaywrightCookieMappers cookieMappers, IUiInteractorBehaviorFactory uiInteractorBehaviorFactory, IUiInteractorTabBehaviorFactory uiInteractorTabBehaviorFactory)
            : base(log, uiInteractorsManager, uiInteractorBehaviorFactory, uiInteractorTabBehaviorFactory)
        {
            _browserFactory = browserFactory;
            _findOptionSearchMethodsProvider = findOptionSearchMethodsProvider;
            _cookieMappers = cookieMappers;
            _uiPageFactoryProvider = uiPageFactoryProvider;
        }

        protected override IUiInteractorTab CreateTab(string aliasId = SharedUiConstants.DefaultUiInteractorTab)
        {
            if (_tabs.Any(x => x.Name == aliasId))
                throw new DuplicateNameException($"Tab with identifier '{aliasId}' already exists. Unique name should be specified for the new tab.");

            var nativeTab = CreateNativeTab();

            IUiElementSearchConfiguration uiElementSearchConfiguration;

            if (_uiInteractorConfiguration != null)
                uiElementSearchConfiguration = _uiInteractorConfiguration;
            else
                uiElementSearchConfiguration = new PlaywrightUiInteractorConfiguration(); //if not passed from outside, set default values

            var tab = new PlaywrightUiInteractorTab(_log, this, uiElementSearchConfiguration, aliasId, nativeTab, _uiPageFactoryProvider, _findOptionSearchMethodsProvider, UiInteractorTabBehaviorFactory, NativeObjects);

            return tab;
        }

        protected override void CloseTab(IUiInteractorTab tab)
        {
            var nativeTab = tab.GetNativeTab<IPage>();
            nativeTab.CloseAsync().Wait();
        }

        public override void Launch()
        {
            Created = DateTime.UtcNow;
            (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) data = _browserFactory.GetBrowser();
            Browser = data.Browser;
            _uiInteractorConfiguration = data.Configuration;
            FillConfiguration(data.Configuration);

            BrowserNewContextOptions contextOptions = _browserFactory.BrowserNewContextOptions;
            BrowserContext = Browser.NewContextAsync(contextOptions).Result;

            NativeObjects.Add(SharedUiConstants.IBrowser, Browser);
            NativeObjects.Add(ProjectConst.IBrowserContext, BrowserContext);
        }

        public override bool IsInteractable()
        {
            if (Browser == null)
                return false;

            try
            {
                return Browser.IsConnected;
            }
            catch
            {
                return false;
            }
        }

        public override void Dispose()
        {
            DisposeForRecreation();

            if (!string.IsNullOrEmpty(DownloadsDefaultDirectory))
                Directory.Delete(DownloadsDefaultDirectory, true);

            NativeObjects.Clear();

            //TODO Check if according to configuration only single element of Browser Instance is created per run - then disposal should happen at the end of run, otherwise each time interactor is disposed
            //if (Browser != null)
            //    Browser.CloseAsync().Wait();
        }

        public override void Recreate()
        {
            DisposeForRecreation();

            foreach (var tab in _tabs)
            {
                var nativeTab = CreateNativeTab();
                (tab as PlaywrightUiInteractorTab).NativeTab = nativeTab;
            }
        }

        private IPage CreateNativeTab()
        {
            if (!IsInteractable())
                Launch();

            IPage page = BrowserContext.NewPageAsync().Result;

            return page;
        }

        private void DisposeForRecreation()
        {
            try
            {
                if (BrowserContext != null)
                    BrowserContext.CloseAsync().Wait();
            }
            catch
            {
                //Add Warning Log
            }
        }

        public T GetNativeInteractor<T>()
        {
            return (T)Browser;
        }

        #region Cookies

        public override void SetCookie(string name, string value, string domain, string? path = SharedUiConstants.DefaultCookiePathValue)
        {
            PlaywrightCookie playwrightCookie = new() { Name = name, Value = value, Domain = domain, Path = path };
            BrowserContext.AddCookiesAsync(new[] { playwrightCookie }).Wait();
        }

        public override void SetCookie(IUiInteractorCookie cookie)
        {
            PlaywrightCookie playwrightCookie = _cookieMappers.ToPlaywrightCookie(cookie);
            BrowserContext.AddCookiesAsync(new[] { playwrightCookie }).Wait();
        }

        public override IUiInteractorCookie? GetCookie(string name)
        {
            BrowserContextCookiesResult? playwrightCookie = BrowserContext.CookiesAsync().Result.FirstOrDefault(x => x.Name == name);

            if (playwrightCookie == null)
                return null;

            return _cookieMappers.ToUiInteractorCookie(playwrightCookie);
        }

        public override List<IUiInteractorCookie> GetAllCookies()
        {
            return BrowserContext.CookiesAsync().Result.Select(x => _cookieMappers.ToUiInteractorCookie(x)).ToList();
        }

        public override void ClearCookies(params string[] names)
        {
            foreach (var name in names)
                BrowserContext.ClearCookiesAsync(options: new BrowserContextClearCookiesOptions { Name = name }).Wait();
        }

        public override void ClearAllCookies()
        {
            BrowserContext.ClearCookiesAsync().Wait();
        }

        #endregion Cookies
    }
}
