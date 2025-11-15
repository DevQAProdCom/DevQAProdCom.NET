using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using Microsoft.Playwright;
using PlaywrightCookie = Microsoft.Playwright.Cookie;

namespace DevQAProdCom.NET.UI.Playwright.Interfaces
{
    public interface IPlaywrightCookieMappers
    {
        public IUiInteractorCookie ToUiInteractorCookie(BrowserContextCookiesResult playwrightCookie);
        public PlaywrightCookie ToPlaywrightCookie(IUiInteractorCookie uiInteractorCookie);
    }
}
