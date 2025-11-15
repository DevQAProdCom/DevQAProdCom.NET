using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Selenium.WebDrivers.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace DevQAProdCom.NET.UI.Selenium.WebDrivers.OperativeClasses
{
    public class DefaultSeleniumWebDriverFactory : BaseUiInteractorFactory, ISeleniumWebDriverFactory
    {
        private ILogger? _log;
        public virtual Func<ISeleniumWebDriverConfiguration?, (IWebDriver, ISeleniumWebDriverConfiguration?)>? CreateWebDriverFunction { get; set; }

        protected override string GetBaseDownloadsDefaultDirectory() => Path.Combine(Environment.CurrentDirectory, base.GetBaseDownloadsDefaultDirectory());

        public DefaultSeleniumWebDriverFactory()
        {
        }

        public DefaultSeleniumWebDriverFactory(ILogger log)
        {
            _log = log;
        }

        public DefaultSeleniumWebDriverFactory(ILogger log, Func<ISeleniumWebDriverConfiguration?, (IWebDriver, ISeleniumWebDriverConfiguration?)> createWebDriverFunction) : this(log)
        {
            CreateWebDriverFunction = createWebDriverFunction;
        }

        public (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateWebDriver(ISeleniumWebDriverConfiguration? configuration = null)
        {
            configuration ??= new SeleniumWebDriverConfiguration();
            CreateWebDriverFunction = CreateDefaultWebDriverWithDefaultConfiguration;
            return CreateWebDriverFunction(configuration);
        }

        public virtual (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateChromeDriver(ISeleniumWebDriverConfiguration configuration)
        {
            IWebDriver driver = new ChromeDriver();
            return (driver, configuration);
        }

        public virtual (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateFirefoxDriver(ISeleniumWebDriverConfiguration configuration)
        {
            IWebDriver driver = new FirefoxDriver();
            return (driver, configuration);
        }

        public virtual (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateEdgeDriver(ISeleniumWebDriverConfiguration configuration)
        {
            //This is temporary fix because of issue https://github.com/SeleniumHQ/selenium/issues/16058#issuecomment-3078368659
            Environment.SetEnvironmentVariable("SE_DRIVER_MIRROR_URL", "https://msedgedriver.microsoft.com");
            IWebDriver driver = new EdgeDriver();
            return (driver, configuration);
        }

        public virtual (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateSafariDriver(ISeleniumWebDriverConfiguration configuration)
        {
            IWebDriver driver = new SafariDriver();
            return (driver, configuration);
        }

        public virtual (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateDefaultWebDriverWithDefaultConfiguration(ISeleniumWebDriverConfiguration configuration)
        {
            if (Browser == BrowserName.Chrome.ToString().ToLower()) return CreateChromeDriver(configuration: configuration);
            if (Browser == BrowserName.Firefox.ToString().ToLower()) return CreateFirefoxDriver(configuration: configuration);
            if (Browser == BrowserName.Edge.ToString().ToLower()) return CreateEdgeDriver(configuration: configuration);
            if (Browser == BrowserName.Safari.ToString().ToLower()) return CreateSafariDriver(configuration: configuration);

            throw new NotSupportedException($"Unsupported value '{Browser}' of '{SharedUiConstants.BROWSER}' environment variable is provided for creation of Selenium 'WebDriver' instance.");
        }
    }
}
