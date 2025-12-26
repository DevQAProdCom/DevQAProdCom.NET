using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.Browsers.Interfaces
{
    public interface IPlaywrightBrowserFactory
    {
        public (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions, IPlaywrightUiInteractorConfiguration? Configuration) GetBrowser(BrowserTypeLaunchOptions? launchOptions = null, 
            IPlaywrightUiInteractorConfiguration? configuration = null);
        public BrowserNewContextOptions BrowserNewContextOptions { get; set; }
    }
}
