using DevQAProdCom.NET.Global.Constants;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Selenium.WebDrivers.Interfaces;
using DevQAProdCom.NET.UI.Selenium.WebDrivers.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace Tests.DevQAProdCom.NET.UI.Configurations
{
    internal class SeleniumWebDriverFactory : DefaultSeleniumWebDriverFactory
    {
        private bool IsRemoteRun
        {
            get
            {
                string? remoteRunEnvVariable = Environment.GetEnvironmentVariable(GlobalConst.IS_REMOTE_RUN);

                if (!string.IsNullOrEmpty(remoteRunEnvVariable))
                    return Convert.ToBoolean(remoteRunEnvVariable);

                else
                    return false;
            }
        }
        protected override string LocalBrowser => BrowserName.Chrome.ToString();

        public SeleniumWebDriverFactory(ILogger log) : base(log) { }

        public override (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateChromeDriver(ISeleniumWebDriverConfiguration configuration)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-gpu");
            options.PageLoadStrategy = PageLoadStrategy.Normal;

            if (string.IsNullOrEmpty(configuration.DownloadsDefaultDirectory))
                configuration.DownloadsDefaultDirectory = GetDownloadsDefaultDirectory();
            options.AddUserProfilePreference("download.default_directory", configuration.DownloadsDefaultDirectory);

            //TODO this part is required to be able to identify files on remote Linux instances (AWS Device Farm)
            //Otherwise some tests returned FileNotFound exception while uploading files to <input> element with "type='file'". Simple SendKeys didn't worked.
            //IAllowsFileDetection fileDetectionDriver = (IAllowsFileDetection)driver;
            //fileDetectionDriver.FileDetector = new LocalFileDetector();

            if (IsRemoteRun)
                options.AddArgument("--headless");

            var driver = new ChromeDriver(options);
            return (driver, configuration);
        }

        public override (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateFirefoxDriver(ISeleniumWebDriverConfiguration configuration)
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("--disable-gpu");
            options.PageLoadStrategy = PageLoadStrategy.Normal;

            if (string.IsNullOrEmpty(configuration.DownloadsDefaultDirectory))
                configuration.DownloadsDefaultDirectory = GetDownloadsDefaultDirectory();
            options.SetPreference("browser.download.dir", configuration.DownloadsDefaultDirectory);

            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf, application/octet-stream");

            if (IsRemoteRun)
                options.AddArgument("--headless");

            var driver = new FirefoxDriver(options);
            return (driver, configuration);
        }

        public override (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateEdgeDriver(ISeleniumWebDriverConfiguration configuration)
        {
            //Temporary fix due to issue: https://github.com/SeleniumHQ/selenium/issues/16058#issuecomment-3078368659
            Environment.SetEnvironmentVariable("SE_DRIVER_MIRROR_URL", "https://msedgedriver.microsoft.com");

            EdgeOptions options = new EdgeOptions();
            options.AddArgument("--disable-gpu");
            options.PageLoadStrategy = PageLoadStrategy.Normal;

            if (string.IsNullOrEmpty(configuration.DownloadsDefaultDirectory))
                configuration.DownloadsDefaultDirectory = GetDownloadsDefaultDirectory();
            options.AddUserProfilePreference("download.default_directory", Path.Combine(Environment.CurrentDirectory, SharedUiConstants.DownloadsDefaultDirectory));

            if (IsRemoteRun)
                options.AddArgument("--headless");

            var driver = new EdgeDriver(options);
            return (driver, configuration);
        }

        public override (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateSafariDriver(ISeleniumWebDriverConfiguration configuration)
        {
            SafariOptions options = new SafariOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;

            var driver = new SafariDriver(options);
            return (driver, configuration);
        }
    }
}
