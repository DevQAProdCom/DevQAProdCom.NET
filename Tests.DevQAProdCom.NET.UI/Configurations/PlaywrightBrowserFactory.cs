using DevQAProdCom.NET.Global.Constants;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Browsers.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Browsers.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using Microsoft.Playwright;

namespace Tests.DevQAProdCom.NET.UI.Configurations
{
    internal class PlaywrightBrowserFactory : DefaultPlaywrightBrowserFactory
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

        public PlaywrightBrowserFactory(ILogger log) : base(log)
        {
        }

        public override BrowserTypeLaunchOptions GetChromeBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration = null) => GetDefaultCustomBrowserTypeLaunchOptions(configuration);
        public override BrowserTypeLaunchOptions GetFirefoxBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration = null) => GetDefaultCustomBrowserTypeLaunchOptions(configuration);
        public override BrowserTypeLaunchOptions GetEdgeBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration = null) => GetDefaultCustomBrowserTypeLaunchOptions(configuration);
        public override BrowserTypeLaunchOptions GetSafariBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration = null) => GetDefaultCustomBrowserTypeLaunchOptions(configuration);

        private BrowserTypeLaunchOptions GetDefaultCustomBrowserTypeLaunchOptions(IPlaywrightUiInteractorConfiguration? configuration = null)
        {
            var options = new BrowserTypeLaunchOptions()
            {
                Headless = IsRemoteRun ? true : false,
                DownloadsPath = configuration?.DownloadsDefaultDirectory,
                Args = new[] { "--disable-gpu" }
            };

            return options;
        }
    }
}
