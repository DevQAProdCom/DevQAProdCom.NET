using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Selenium.WebDrivers.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using OpenQA.Selenium;
using SeleniumCookie = OpenQA.Selenium.Cookie;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractor
{
    public class SeleniumUiInteractor : BaseUiInteractor, ISeleniumUiInteractor
    {
        private readonly IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> _findOptionSearchMethodsProvider;
        private readonly ISeleniumCookieMappers _cookieMappers;
        private readonly ISeleniumWebDriverFactory _webDriverFactory;
        private readonly IUiPageFactoryProvider _uiPageFactoryProvider;

        //Can be set/substituted in tests in case each test needs browser/driver with different configuration
        public IWebDriver _driver { get; set; }

        public IWebDriver GetWebDriver() => _driver;

        public SeleniumUiInteractor(ILogger log, IUiInteractorsManager uiInteractorsManager, ISeleniumWebDriverFactory webDriverFactory,
            IUiPageFactoryProvider uiPageFactoryProvider, IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> findOptionSearchMethodsProvider,
            ISeleniumCookieMappers cookieMappers, IUiInteractorBehaviorFactory uiInteractorBehaviorFactory, IUiInteractorTabBehaviorFactory uiInteractorTabBehaviorFactory)
            : base(log, uiInteractorsManager, uiInteractorBehaviorFactory, uiInteractorTabBehaviorFactory) //UIInteractorContext (IUiInteractor)
        {
            _findOptionSearchMethodsProvider = findOptionSearchMethodsProvider;
            _cookieMappers = cookieMappers;
            _webDriverFactory = webDriverFactory;
            _uiPageFactoryProvider = uiPageFactoryProvider;
        }

        public override void Launch()
        {
            Created = DateTime.UtcNow;
            (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) data = _webDriverFactory.CreateWebDriver();

            _driver = data.Driver;
            DownloadsDefaultDirectory = data.Configuration?.DownloadsDefaultDirectory;

            if (data.Configuration?.TimeToLive != null)
                TimeToLive = data.Configuration.TimeToLive.Value;

            if (data.Configuration?.Created != null)
                Created = data.Configuration.Created.Value;

            if (data.Configuration?.Data != null)
                Data = data.Configuration.Data;

            NativeObjects.Upsert(SharedUiConstants.GetIWebDriverFunc, this.GetWebDriver);
        }

        public override bool IsInteractable()
        {
            if (GetWebDriver() == null)
                return false;

            try
            {
                return !string.IsNullOrEmpty(GetWebDriver().CurrentWindowHandle);
            }
            catch
            {
                return false;
            }
        }

        public override void Dispose()
        {
            DisposeForRecreation();

            //Additional actions for full disposal
            if (!string.IsNullOrEmpty(DownloadsDefaultDirectory))
                Directory.Delete(DownloadsDefaultDirectory, true);

            _tabs.Clear();
            NativeObjects.Clear();
        }

        private void DisposeForRecreation()
        {
            try
            {
                if (GetWebDriver() != null && IsInteractable())
                    GetWebDriver().Quit();
            }
            catch
            {
                //Add Warning Log
            }
        }

        protected override IUiInteractorTab CreateTab(string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            var nativeTab = CreateNativeTab();
            var tab = new SeleniumUiInteractorTab(_log, this, tabName, nativeTab, _uiPageFactoryProvider, _findOptionSearchMethodsProvider, UiInteractorTabBehaviorFactory, NativeObjects);
            return tab;
        }

        public override void Recreate()
        {
            DisposeForRecreation();

            foreach (var tab in _tabs)
            {
                var nativeTab = CreateNativeTab();
                (tab as SeleniumUiInteractorTab).NativeTab = nativeTab;
            }
        }

        private string CreateNativeTab()
        {
            if (!IsInteractable())
                Launch();
            else
            {
                GetWebDriver().SwitchTo().NewWindow(WindowType.Tab);
            }

            return GetWebDriver().CurrentWindowHandle;
        }

        protected override void CloseTab(IUiInteractorTab tab)
        {
            tab.SwitchTo();
            var isLastTab = GetWebDriver().WindowHandles.Count() == 1;
            GetWebDriver().Close();

            //this is required because simple driver close doesn't delete chromeGetWebDriver().exe
            if (isLastTab)
                Dispose();
        }

        #region Cookies

        public override void SetCookie(string name, string value, string domain, string? path = SharedUiConstants.DefaultCookiePathValue)
        {
            SeleniumCookie seleniumCookie = new(name, value, domain, path, null);
            GetWebDriver().Manage().Cookies.AddCookie(seleniumCookie);
        }

        public override void SetCookie(IUiInteractorCookie cookie)
        {
            SeleniumCookie seleniumCookie = _cookieMappers.ToSeleniumCookie(cookie);
            GetWebDriver().Manage().Cookies.AddCookie(seleniumCookie);
        }

        public override IUiInteractorCookie? GetCookie(string name)
        {
            var cookies = GetAllCookies();
            var cookie = cookies.SingleOrDefault(x => x.Name == name);

            if (cookie == null)
                return null;

            return cookie;
        }

        public override List<IUiInteractorCookie> GetAllCookies()
        {
            List<SeleniumCookie> seleniumCookies = new();
            var currentWindowHandle = GetWebDriver().CurrentWindowHandle;

            List<SeleniumCookie> currentWindowHandleCookies = GetWebDriver().Manage().Cookies.AllCookies.ToList();
            seleniumCookies.AddRange(currentWindowHandleCookies);

            foreach (var windowHandle in GetWebDriver().WindowHandles)
            {
                if (windowHandle == currentWindowHandle)
                    continue;

                GetWebDriver().SwitchTo().Window(windowHandle);
                List<SeleniumCookie> windowHandleCookies = GetWebDriver().Manage().Cookies.AllCookies.ToList();
                seleniumCookies.AddRange(windowHandleCookies);
            }

            GetWebDriver().SwitchTo().Window(currentWindowHandle);
            return seleniumCookies.Select(x => _cookieMappers.ToUiInteractorCookie(x)).Distinct().ToList();
        }

        public override void ClearCookies(params string[] names)
        {
            //Such behavior is required because seleniul clears and sets and gets cookies only for current tab and current domain
            var currentWindowHandle = GetWebDriver().CurrentWindowHandle;
            foreach (var name in names)
                GetWebDriver().Manage().Cookies.DeleteCookieNamed(name);

            foreach (var windowHandle in GetWebDriver().WindowHandles)
            {
                if (windowHandle == currentWindowHandle)
                    continue;

                GetWebDriver().SwitchTo().Window(windowHandle);
                foreach (var name in names)
                    GetWebDriver().Manage().Cookies.DeleteCookieNamed(name);
            }

            GetWebDriver().SwitchTo().Window(currentWindowHandle);
        }

        public override void ClearAllCookies()
        {
            //Such behavior is required because seleniul clears and sets and gets cookies only for current tab and current domain
            var currentWindowHandle = GetWebDriver().CurrentWindowHandle;
            GetWebDriver().Manage().Cookies.DeleteAllCookies();

            foreach (var windowHandle in GetWebDriver().WindowHandles)
            {
                if (windowHandle == currentWindowHandle)
                    continue;

                GetWebDriver().SwitchTo().Window(windowHandle);
                GetWebDriver().Manage().Cookies.DeleteAllCookies();
            }

            GetWebDriver().SwitchTo().Window(currentWindowHandle);
        }

        #endregion Cookies
    }
}
