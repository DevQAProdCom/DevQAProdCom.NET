using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Selenium.Constants;
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
        public IWebDriver Driver { get; set; }

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
            (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) data = _webDriverFactory.CreateWebDriver();
            Driver = data.Driver;
            DownloadsDefaultDirectory = data.Configuration?.DownloadsDefaultDirectory;
            NativeObjects.Add(ProjectConst.IWebDriver, Driver);
        }

        public override bool IsInteractable()
        {
            if (Driver == null)
                return false;

            try
            {
                return !string.IsNullOrEmpty(Driver.CurrentWindowHandle);
            }
            catch
            {
                return false;
            }
        }

        public override void Dispose()
        {
            if (Driver != null)
                Driver.Quit();

            if (!string.IsNullOrEmpty(DownloadsDefaultDirectory))
                Directory.Delete(DownloadsDefaultDirectory, true);
        }

        protected override IUiInteractorTab CreateTab(string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            if (!IsInteractable())
                Launch();
            else
            {
                Driver.SwitchTo().NewWindow(WindowType.Tab);
            }

            var tab = new SeleniumUiInteractorTab(_log, this, tabName, Driver.CurrentWindowHandle, _uiPageFactoryProvider, _findOptionSearchMethodsProvider, UiInteractorTabBehaviorFactory, NativeObjects);
            return tab;
        }

        protected override void CloseTab(IUiInteractorTab tab)
        {
            tab.SwitchTo();
            var isLastTab = Driver.WindowHandles.Count() == 1;
            Driver.Close();

            //this is required because simple driver close doesn't delete chromedriver.exe
            if (isLastTab)
                Dispose();
        }

        #region Cookies

        public override void SetCookie(string name, string value, string domain, string? path = SharedUiConstants.DefaultCookiePathValue)
        {
            SeleniumCookie seleniumCookie = new(name, value, domain, path, null);
            Driver.Manage().Cookies.AddCookie(seleniumCookie);
        }

        public override void SetCookie(IUiInteractorCookie cookie)
        {
            SeleniumCookie seleniumCookie = _cookieMappers.ToSeleniumCookie(cookie);
            Driver.Manage().Cookies.AddCookie(seleniumCookie);
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
            var currentWindowHandle = Driver.CurrentWindowHandle;

            List<SeleniumCookie> currentWindowHandleCookies = Driver.Manage().Cookies.AllCookies.ToList();
            seleniumCookies.AddRange(currentWindowHandleCookies);

            foreach (var windowHandle in Driver.WindowHandles)
            {
                if (windowHandle == currentWindowHandle)
                    continue;

                Driver.SwitchTo().Window(windowHandle);
                List<SeleniumCookie> windowHandleCookies = Driver.Manage().Cookies.AllCookies.ToList();
                seleniumCookies.AddRange(windowHandleCookies);
            }

            Driver.SwitchTo().Window(currentWindowHandle);
            return seleniumCookies.Select(x => _cookieMappers.ToUiInteractorCookie(x)).Distinct().ToList();
        }

        public override void ClearCookies(params string[] names)
        {
            //Such behavior is required because seleniul clears and sets and gets cookies only for current tab and current domain
            var currentWindowHandle = Driver.CurrentWindowHandle;
            foreach (var name in names)
                Driver.Manage().Cookies.DeleteCookieNamed(name);

            foreach (var windowHandle in Driver.WindowHandles)
            {
                if (windowHandle == currentWindowHandle)
                    continue;

                Driver.SwitchTo().Window(windowHandle);
                foreach (var name in names)
                    Driver.Manage().Cookies.DeleteCookieNamed(name);
            }

            Driver.SwitchTo().Window(currentWindowHandle);
        }

        public override void ClearAllCookies()
        {
            //Such behavior is required because seleniul clears and sets and gets cookies only for current tab and current domain
            var currentWindowHandle = Driver.CurrentWindowHandle;
            Driver.Manage().Cookies.DeleteAllCookies();

            foreach (var windowHandle in Driver.WindowHandles)
            {
                if (windowHandle == currentWindowHandle)
                    continue;

                Driver.SwitchTo().Window(windowHandle);
                Driver.Manage().Cookies.DeleteAllCookies();
            }

            Driver.SwitchTo().Window(currentWindowHandle);
        }

        #endregion Cookies
    }
}
