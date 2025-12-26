using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Browsers.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.Browsers.OperativeClasses
{
    public class DefaultPlaywrightBrowserFactory : BaseUiInteractorFactory, IPlaywrightBrowserFactory
    {
        private ILogger? _log;
        public virtual Func<BrowserTypeLaunchOptions?, IPlaywrightUiInteractorConfiguration?, (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration)>? CreateBrowserFunction { get; set; }
        public virtual Func<BrowserNewContextOptions>? GetBrowserNewContextOptionsFunction { get; set; }

        //Initialization of Playwright is better to be done once per whole run, because if create it for each test, the launch of interactor takes 3-4 times longer than even for Selenium.
        //As a result this class as a whole is better to be Singleton while using Dependency Injection.
        public virtual IPlaywright Playwright { get; set; } = Microsoft.Playwright.Playwright.CreateAsync().Result;

        //Initialization of Browser is better to be done once per whole run, because if create it for each test, the launch of interactor takes 2+ times longer than otherwise.
        //As a result this class as a whole is better to be Singleton while using Dependency Injection.
        public virtual IBrowser? PlaywrightBrowserInstance { get; set; }
        public virtual bool ShouldCreateNewBrowserInstanceEachTime { get; set; } = false;
        public IPlaywrightUiInteractorConfiguration? PlaywrightBrowserConfiguration { get; private set; }
        protected virtual IPlaywrightUiInteractorConfiguration GetDefaultPlaywrightBrowserConfiguration() => new PlaywrightUiInteractorConfiguration();

        protected BrowserTypeLaunchOptions? _browserTypeLaunchOptions;
        public virtual BrowserTypeLaunchOptions? GetBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration)
        {
            if (_browserTypeLaunchOptions != null)
                return _browserTypeLaunchOptions;

            return GetOverridenOrDefaultBrowserTypeLaunchOptions(configuration);
        }

        protected BrowserNewContextOptions? _browserNewContextOptions;
        public virtual BrowserNewContextOptions BrowserNewContextOptions
        {
            get
            {
                if (GetBrowserNewContextOptionsFunction != null)
                    return GetBrowserNewContextOptionsFunction();
                else if (_browserNewContextOptions != null)
                    return _browserNewContextOptions;
                else
                    return GetOverridenOrDefaultBrowserNewContextOptions();
            }

            set => _browserNewContextOptions = value;
        }

        public DefaultPlaywrightBrowserFactory()
        {
        }

        public DefaultPlaywrightBrowserFactory(ILogger log)
        {
            _log = log;
        }

        public DefaultPlaywrightBrowserFactory(ILogger log, Func<BrowserTypeLaunchOptions?, IPlaywrightUiInteractorConfiguration?, (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration)>? createBrowserFunction, Func<BrowserNewContextOptions>? getBrowserNewContextOptionsFunction) : this(log)
        {
            CreateBrowserFunction = createBrowserFunction;
            GetBrowserNewContextOptionsFunction = getBrowserNewContextOptionsFunction;
        }

        public DefaultPlaywrightBrowserFactory(ILogger log, BrowserTypeLaunchOptions browserTypeLaunchOptions, BrowserNewContextOptions browserNewContextOptions) : this(log)
        {
            _browserTypeLaunchOptions = browserTypeLaunchOptions;
            BrowserNewContextOptions = browserNewContextOptions;
        }

        #region Browser

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) GetBrowser(BrowserTypeLaunchOptions? options = null, IPlaywrightUiInteractorConfiguration? configuration = null)
        {
            configuration ??= GetDefaultPlaywrightBrowserConfiguration();

            if (CreateBrowserFunction != null)
                return CreateBrowserFunction(options, configuration);
            else
                return CreateBrowserWithOverridenOrDefaultBrowserTypeLaunchOptions(options, configuration);
        }

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) CreateChromeBrowser(BrowserTypeLaunchOptions? options = null, IPlaywrightUiInteractorConfiguration? configuration = null)
        {
            (BrowserTypeLaunchOptions? LauchOptions, IPlaywrightUiInteractorConfiguration? Configuration) SetupConfigurationData(BrowserTypeLaunchOptions? options, IPlaywrightUiInteractorConfiguration? configuration)
            {
                configuration ??= GetDefaultPlaywrightBrowserConfiguration();
                options ??= GetBrowserTypeLaunchOptions(configuration)?.CloneJson<BrowserTypeLaunchOptions>();
                if (options != null)
                    options.Channel = "chrome";
                return (options, configuration);
            }

            Func<BrowserTypeLaunchOptions?, IBrowser> launchBrowser = (options) => Playwright.Chromium.LaunchAsync(options).Result;
            return CreateBrowser(SetupConfigurationData, launchBrowser, options, configuration);
        }

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) CreateFirefoxBrowser(BrowserTypeLaunchOptions? options = null, IPlaywrightUiInteractorConfiguration? configuration = null)
        {
            (BrowserTypeLaunchOptions? LauchOptions, IPlaywrightUiInteractorConfiguration? Configuration) SetupConfigurationData(BrowserTypeLaunchOptions? options, IPlaywrightUiInteractorConfiguration? configuration)
            {
                configuration ??= GetDefaultPlaywrightBrowserConfiguration();
                options ??= GetBrowserTypeLaunchOptions(configuration)?.CloneJson<BrowserTypeLaunchOptions>();
                return (options, configuration);
            }

            Func<BrowserTypeLaunchOptions?, IBrowser> launchBrowser = (options) => Playwright.Firefox.LaunchAsync(options).Result;
            return CreateBrowser(SetupConfigurationData, launchBrowser, options, configuration);
        }


        public virtual (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) CreateEdgeBrowser(BrowserTypeLaunchOptions? options = null, IPlaywrightUiInteractorConfiguration? configuration = null)
        {
            (BrowserTypeLaunchOptions? LauchOptions, IPlaywrightUiInteractorConfiguration? Configuration) SetupConfigurationData(BrowserTypeLaunchOptions? options, IPlaywrightUiInteractorConfiguration? configuration)
            {
                configuration ??= GetDefaultPlaywrightBrowserConfiguration();
                options ??= GetBrowserTypeLaunchOptions(configuration)?.CloneJson<BrowserTypeLaunchOptions>();
                if (options != null)
                    options.Channel = "msedge";
                return (options, configuration);
            }

            Func<BrowserTypeLaunchOptions?, IBrowser> launchBrowser = (options) => Playwright.Chromium.LaunchAsync(options).Result;
            return CreateBrowser(SetupConfigurationData, launchBrowser, options, configuration);
        }

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) CreateSafariBrowser(BrowserTypeLaunchOptions? options = null, IPlaywrightUiInteractorConfiguration? configuration = null)
        {
            (BrowserTypeLaunchOptions? LauchOptions, IPlaywrightUiInteractorConfiguration? Configuration) SetupConfigurationData(BrowserTypeLaunchOptions? options, IPlaywrightUiInteractorConfiguration? configuration)
            {
                configuration ??= GetDefaultPlaywrightBrowserConfiguration();
                options ??= GetBrowserTypeLaunchOptions(configuration)?.CloneJson<BrowserTypeLaunchOptions>();
                return (options, configuration);
            }

            Func<BrowserTypeLaunchOptions?, IBrowser> launchBrowser = (options) => Playwright.Webkit.LaunchAsync(options).Result;
            return CreateBrowser(SetupConfigurationData, launchBrowser, options, configuration);
        }

        private (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) CreateBrowser(Func<BrowserTypeLaunchOptions?, IPlaywrightUiInteractorConfiguration?, (BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration)> setupConfigurationData, Func<BrowserTypeLaunchOptions?, IBrowser> launchBrowser, BrowserTypeLaunchOptions? options = null, IPlaywrightUiInteractorConfiguration? configuration = null)
        {
            if (ShouldCreateNewBrowserInstanceEachTime)
            {
                (BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) configurationData = setupConfigurationData.Invoke(options, configuration);
                IBrowser playwrightBrowserInstance = launchBrowser.Invoke(configurationData.LaunchOptions);
                return (playwrightBrowserInstance, configurationData.LaunchOptions, configurationData.Configuration);
            }
            else
            {
                if (PlaywrightBrowserInstance == null)
                {
                    (BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) configurationData = setupConfigurationData.Invoke(options, configuration);
                    PlaywrightBrowserInstance = launchBrowser.Invoke(configurationData.LaunchOptions);
                    _browserTypeLaunchOptions = configurationData.LaunchOptions;
                    PlaywrightBrowserConfiguration = configurationData.Configuration;
                }

                return (PlaywrightBrowserInstance, _browserTypeLaunchOptions, PlaywrightBrowserConfiguration);
            }
        }

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) CreateBrowserWithOverridenOrDefaultBrowserTypeLaunchOptions(
            BrowserTypeLaunchOptions? launchOptions,
            IPlaywrightUiInteractorConfiguration? configuration = null)
        {
            (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) CreateBrowser()
            {

                if (Browser == BrowserName.Chrome.ToString().ToLower()) return CreateChromeBrowser(launchOptions, configuration);
                if (Browser == BrowserName.Firefox.ToString().ToLower()) return CreateFirefoxBrowser(launchOptions, configuration);
                if (Browser == BrowserName.Edge.ToString().ToLower()) return CreateEdgeBrowser(launchOptions, configuration);
                if (Browser == BrowserName.Safari.ToString().ToLower()) return CreateSafariBrowser(launchOptions, configuration);

                throw new NotSupportedException($"Unsupported value '{Browser}' of '{SharedUiConstants.BROWSER}' environment variable is provided for creation of Playwright 'Browser' instance.");
            }

            //This line is needed to ensure not to create multiple browser instances, which is done for performance optimization.
            //IBrowser.NewContextAsync() ensures isolation between tests.
            if (ShouldCreateNewBrowserInstanceEachTime) //TODO ?? rename ShouldCreateNewBrowserInstanceEachTime   ShouldCreateSingleBrowserInstancePerRun
            {
                return CreateBrowser();
            }
            else //ShouldCreateSingleBrowserInstancePerRun
            {
                if (PlaywrightBrowserInstance == null)
                    CreateBrowser();

                return (PlaywrightBrowserInstance!, _browserTypeLaunchOptions, PlaywrightBrowserConfiguration);
            }
        }

        #endregion Browser

        #region BrowserTypeLaunchOptions

        public virtual BrowserTypeLaunchOptions? DefaultBrowserTypeLaunchOptions => new BrowserTypeLaunchOptions() { Headless = false };
        public virtual BrowserTypeLaunchOptions? GetOverridenOrDefaultBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration)
        {
            if (Browser == BrowserName.Chrome.ToString().ToLower()) return GetChromeBrowserTypeLaunchOptions(configuration);
            if (Browser == BrowserName.Firefox.ToString().ToLower()) return GetFirefoxBrowserTypeLaunchOptions(configuration);
            if (Browser == BrowserName.Edge.ToString().ToLower()) return GetEdgeBrowserTypeLaunchOptions(configuration);
            if (Browser == BrowserName.Safari.ToString().ToLower()) return GetSafariBrowserTypeLaunchOptions(configuration);

            throw new NotSupportedException($"Unsupported value '{Browser}' of '{SharedUiConstants.BROWSER}' environment variable is provided for retrieval of Playwright 'BrowserTypeLaunchOptions' instance.");
        }

        public virtual BrowserTypeLaunchOptions? GetChromeBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration = null) => DefaultBrowserTypeLaunchOptions;
        public virtual BrowserTypeLaunchOptions? GetFirefoxBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration = null) => DefaultBrowserTypeLaunchOptions;
        public virtual BrowserTypeLaunchOptions? GetEdgeBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration = null) => DefaultBrowserTypeLaunchOptions;
        public virtual BrowserTypeLaunchOptions? GetSafariBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration = null) => DefaultBrowserTypeLaunchOptions;

        #endregion BrowserTypeLaunchOptions

        #region BrowserNewContextOptions

        public virtual BrowserNewContextOptions DefaultBrowserNewContextOptions => new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = 1920,
                Height = 1080
            },
            AcceptDownloads = true
        };

        public virtual BrowserNewContextOptions GetOverridenOrDefaultBrowserNewContextOptions()
        {
            if (Browser == BrowserName.Chrome.ToString().ToLower()) return GetChromeBrowserNewContextOptions();
            if (Browser == BrowserName.Firefox.ToString().ToLower()) return GetFirefoxBrowserNewContextOptions();
            if (Browser == BrowserName.Edge.ToString().ToLower()) return GetEdgeBrowserNewContextOptions();
            if (Browser == BrowserName.Safari.ToString().ToLower()) return GetSafariBrowserNewContextOptions();

            throw new NotSupportedException($"Unsupported value '{Browser}' of '{SharedUiConstants.BROWSER}' environment variable is provided for retrieval of Playwright 'BrowserNewContextOptions' instance.");
        }

        public virtual BrowserNewContextOptions GetChromeBrowserNewContextOptions() => DefaultBrowserNewContextOptions;
        public virtual BrowserNewContextOptions GetFirefoxBrowserNewContextOptions() => DefaultBrowserNewContextOptions;
        public virtual BrowserNewContextOptions GetEdgeBrowserNewContextOptions() => DefaultBrowserNewContextOptions;
        public virtual BrowserNewContextOptions GetSafariBrowserNewContextOptions() => DefaultBrowserNewContextOptions;

        #endregion BrowserNewContextOptions
    }
}
