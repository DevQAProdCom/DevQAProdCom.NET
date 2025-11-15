using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using Microsoft.Playwright;
using PlaywrightCookie = Microsoft.Playwright.Cookie;

namespace DevQAProdCom.NET.UI.Playwright.Mappers
{
    public class PlaywrightCookieMappers : IPlaywrightCookieMappers
    {
        public IUiInteractorCookie ToUiInteractorCookie(BrowserContextCookiesResult playwrightCookie)
        {
            IUiInteractorCookie uiInteractorCookie = new UiInteractorCookie(playwrightCookie.Name, playwrightCookie.Value, playwrightCookie.Domain, playwrightCookie.Path);
            uiInteractorCookie.Secure = playwrightCookie.Secure;
            uiInteractorCookie.HttpOnly = playwrightCookie.HttpOnly;
            uiInteractorCookie.Expires = playwrightCookie.Expires.ToDateTimeFromUnixTimeSeconds();
            uiInteractorCookie.SameSite = playwrightCookie.SameSite.ToString();

            return uiInteractorCookie;
        }

        public PlaywrightCookie ToPlaywrightCookie(IUiInteractorCookie uiInteractorCookie)
        {
            PlaywrightCookie playwrightCookie = new();
            playwrightCookie.Name = uiInteractorCookie.Name;
            playwrightCookie.Value = uiInteractorCookie.Value;
            playwrightCookie.Domain = uiInteractorCookie.Domain;
            playwrightCookie.Path = uiInteractorCookie.Path;
            playwrightCookie.Secure = uiInteractorCookie.Secure ?? false;
            playwrightCookie.HttpOnly = uiInteractorCookie.HttpOnly ?? false;
            playwrightCookie.Expires = uiInteractorCookie.Expires.ToUnixTimeSeconds();
            playwrightCookie.SameSite = uiInteractorCookie.SameSite?.ToEnum<SameSiteAttribute>();

            return playwrightCookie;
        }
    }
}
