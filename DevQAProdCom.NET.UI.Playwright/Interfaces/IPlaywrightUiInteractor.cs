using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.Interfaces
{
    public interface IPlaywrightUiInteractor : IUiInteractor
    {
        public IBrowser Browser { get; }
        public IBrowserContext BrowserContext { get; }
    }
}
