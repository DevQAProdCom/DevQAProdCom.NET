using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.Browsers.Interfaces
{
    public interface IPlaywrightBrowserFactory
    {
        public (IBrowser Browser, BrowserTypeLaunchOptions? LaunchOptions) GetBrowser(BrowserTypeLaunchOptions? launchOptions = null);
        public BrowserNewContextOptions BrowserNewContextOptions { get; set; }
    }
}
