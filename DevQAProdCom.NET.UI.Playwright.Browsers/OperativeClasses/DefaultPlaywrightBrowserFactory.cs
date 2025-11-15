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
        public virtual Func<BrowserTypeLaunchOptions?, (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions)>? CreateBrowserFunction { get; set; }
        public virtual Func<BrowserNewContextOptions>? GetBrowserNewContextOptionsFunction { get; set; }

        //Initialization of Playwright is better to be done once per whole run, because if create it for each test, the launch of interactor takes 3-4 times longer than even for Selenium.
        //As a result this class as a whole is better to be Singleton while using Dependency Injection.
        public virtual IPlaywright Playwright { get; set; } = Microsoft.Playwright.Playwright.CreateAsync().Result;

        //Initialization of Browser is better to be done once per whole run, because if create it for each test, the launch of interactor takes 2+ times longer than otherwise.
        //As a result this class as a whole is better to be Singleton while using Dependency Injection.
        public virtual IBrowser? PlaywrightBrowserInstance { get; set; }
        public virtual bool ShouldCreateNewBrowserInstanceEachTime { get; set; } = false;

        protected BrowserTypeLaunchOptions? _browserTypeLaunchOptions;
        public virtual BrowserTypeLaunchOptions BrowserTypeLaunchOptions
        {
            get
            {
                if (_browserTypeLaunchOptions != null)
                    return _browserTypeLaunchOptions;

                return GetOverridenOrDefaultBrowserTypeLaunchOptions();
            }
            set => _browserTypeLaunchOptions = value;
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

        public DefaultPlaywrightBrowserFactory(ILogger log, Func<BrowserTypeLaunchOptions?, (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions)>? createBrowserFunction, Func<BrowserNewContextOptions>? getBrowserNewContextOptionsFunction) : this(log)
        {
            CreateBrowserFunction = createBrowserFunction;
            GetBrowserNewContextOptionsFunction = getBrowserNewContextOptionsFunction;
        }

        public DefaultPlaywrightBrowserFactory(ILogger log, BrowserTypeLaunchOptions browserTypeLaunchOptions, BrowserNewContextOptions browserNewContextOptions) : this(log)
        {
            BrowserTypeLaunchOptions = browserTypeLaunchOptions;
            BrowserNewContextOptions = browserNewContextOptions;
        }

        #region Browser

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions) GetBrowser(BrowserTypeLaunchOptions? options = null)
        {
            if (CreateBrowserFunction != null)
                return CreateBrowserFunction(options);
            else
                return CreateBrowserWithOverridenOrDefaultBrowserTypeLaunchOptions(options);
        }

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions) CreateChromeBrowser(BrowserTypeLaunchOptions? options = null)
        {
            void SetOptions()
            {
                options ??= BrowserTypeLaunchOptions.CloneJson<BrowserTypeLaunchOptions>();
                options.Channel = "chrome";
            }

            if (ShouldCreateNewBrowserInstanceEachTime)
            {
                SetOptions();
                IBrowser playwrightBrowserInstance = Playwright.Chromium.LaunchAsync(options).Result;
                return (playwrightBrowserInstance, options);
            }
            else
            {
                if (PlaywrightBrowserInstance == null)
                {
                    SetOptions();
                    PlaywrightBrowserInstance = Playwright.Chromium.LaunchAsync(options).Result;
                    BrowserTypeLaunchOptions = options;
                }

                return (PlaywrightBrowserInstance, BrowserTypeLaunchOptions);
            }
        }

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions) CreateFirefoxBrowser(BrowserTypeLaunchOptions? options = null)
        {
            void SetOptions()
            {
                options ??= BrowserTypeLaunchOptions.CloneJson<BrowserTypeLaunchOptions>();
            }

            if (ShouldCreateNewBrowserInstanceEachTime)
            {
                SetOptions();
                IBrowser playwrightBrowserInstance = Playwright.Firefox.LaunchAsync(options).Result;
                return (playwrightBrowserInstance, options);
            }
            else
            {
                if (PlaywrightBrowserInstance == null)
                {
                    SetOptions();
                    PlaywrightBrowserInstance = Playwright.Firefox.LaunchAsync(options).Result;
                    BrowserTypeLaunchOptions = options;
                }

                return (PlaywrightBrowserInstance, BrowserTypeLaunchOptions);
            }
        }


        public virtual (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions) CreateEdgeBrowser(BrowserTypeLaunchOptions? options = null)
        {
            void SetOptions()
            {
                options ??= BrowserTypeLaunchOptions.CloneJson<BrowserTypeLaunchOptions>();
                options.Channel = "msedge";
            }

            if (ShouldCreateNewBrowserInstanceEachTime)
            {
                SetOptions();
                IBrowser playwrightBrowserInstance = Playwright.Chromium.LaunchAsync(options).Result;
                return (playwrightBrowserInstance, options);
            }
            else
            {
                if (PlaywrightBrowserInstance == null)
                {
                    SetOptions();
                    PlaywrightBrowserInstance = Playwright.Chromium.LaunchAsync(options).Result;
                    BrowserTypeLaunchOptions = options;
                }

                return (PlaywrightBrowserInstance, BrowserTypeLaunchOptions);
            }
        }

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions) CreateSafariBrowser(BrowserTypeLaunchOptions? options = null)
        {
            void SetOptions()
            {
                options ??= BrowserTypeLaunchOptions.CloneJson<BrowserTypeLaunchOptions>();
            }

            if (ShouldCreateNewBrowserInstanceEachTime)
            {
                SetOptions();
                IBrowser playwrightBrowserInstance = Playwright.Webkit.LaunchAsync(options).Result;
                return (playwrightBrowserInstance, options);
            }
            else
            {
                if (PlaywrightBrowserInstance == null)
                {
                    SetOptions();
                    PlaywrightBrowserInstance = Playwright.Webkit.LaunchAsync(options).Result;
                    BrowserTypeLaunchOptions = options;
                }

                return (PlaywrightBrowserInstance, BrowserTypeLaunchOptions);
            }
        }

        public virtual (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions) CreateBrowserWithOverridenOrDefaultBrowserTypeLaunchOptions(BrowserTypeLaunchOptions? launchOptions)
        {
            (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions) CreateBrowser()
            {

                if (Browser == BrowserName.Chrome.ToString().ToLower()) return CreateChromeBrowser(launchOptions);
                if (Browser == BrowserName.Firefox.ToString().ToLower()) return CreateFirefoxBrowser(launchOptions);
                if (Browser == BrowserName.Edge.ToString().ToLower()) return CreateEdgeBrowser(launchOptions);
                if (Browser == BrowserName.Safari.ToString().ToLower()) return CreateSafariBrowser(launchOptions);

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
                {
                    (IBrowser Browser, BrowserTypeLaunchOptions LaunchOptions) data = CreateBrowser();
                    PlaywrightBrowserInstance = data.Browser;
                    BrowserTypeLaunchOptions = data.LaunchOptions;
                }

                return (PlaywrightBrowserInstance, BrowserTypeLaunchOptions);
            }
        }

        #endregion Browser

        #region BrowserTypeLaunchOptions

        public virtual BrowserTypeLaunchOptions GetOverridenOrDefaultBrowserTypeLaunchOptions()
        {
            if (Browser == BrowserName.Chrome.ToString().ToLower()) return GetChromeBrowserTypeLaunchOptions();
            if (Browser == BrowserName.Firefox.ToString().ToLower()) return GetFirefoxBrowserTypeLaunchOptions();
            if (Browser == BrowserName.Edge.ToString().ToLower()) return GetEdgeBrowserTypeLaunchOptions();
            if (Browser == BrowserName.Safari.ToString().ToLower()) return GetSafariBrowserTypeLaunchOptions();

            throw new NotSupportedException($"Unsupported value '{Browser}' of '{SharedUiConstants.BROWSER}' environment variable is provided for retrieval of Playwright 'BrowserTypeLaunchOptions' instance.");
        }

        public virtual BrowserTypeLaunchOptions GetChromeBrowserTypeLaunchOptions() => DefaultBrowserTypeLaunchOptions;
        public virtual BrowserTypeLaunchOptions GetFirefoxBrowserTypeLaunchOptions() => DefaultBrowserTypeLaunchOptions;
        public virtual BrowserTypeLaunchOptions GetEdgeBrowserTypeLaunchOptions() => DefaultBrowserTypeLaunchOptions;
        public virtual BrowserTypeLaunchOptions GetSafariBrowserTypeLaunchOptions() => DefaultBrowserTypeLaunchOptions;
        public virtual BrowserTypeLaunchOptions DefaultBrowserTypeLaunchOptions => new BrowserTypeLaunchOptions() { Headless = false };

        #endregion BrowserTypeLaunchOptions

        #region BrowserNewContextOptions

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
        public virtual BrowserNewContextOptions DefaultBrowserNewContextOptions => new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = 1920,
                Height = 1080
            },
            AcceptDownloads = true
        };

        #endregion BrowserNewContextOptions
    }
}
