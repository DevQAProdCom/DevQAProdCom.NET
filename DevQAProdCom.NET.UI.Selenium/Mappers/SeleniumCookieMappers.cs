using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using SeleniumCookie = OpenQA.Selenium.Cookie;

namespace DevQAProdCom.NET.UI.Selenium.Mappers
{
    public class SeleniumCookieMappers : ISeleniumCookieMappers
    {
        public IUiInteractorCookie ToUiInteractorCookie(SeleniumCookie seleniumCookie)
        {
            UiInteractorCookie uiInteractorCookie = new(seleniumCookie.Name, seleniumCookie.Value, seleniumCookie.Domain, seleniumCookie.Path);
            uiInteractorCookie.Secure = seleniumCookie.Secure;
            uiInteractorCookie.HttpOnly = seleniumCookie.IsHttpOnly;
            uiInteractorCookie.Expires = seleniumCookie.Expiry != default ? seleniumCookie.Expiry.ConvertToUtc() : default;
            uiInteractorCookie.SameSite = seleniumCookie.SameSite;

            return uiInteractorCookie;
        }

        public SeleniumCookie ToSeleniumCookie(IUiInteractorCookie uiInteractorCookie)
        {
            SeleniumCookie seleniumCookie = new(name: uiInteractorCookie.Name,
                value: uiInteractorCookie.Value,
                domain: uiInteractorCookie.Domain,
                path: uiInteractorCookie.Path,
                expiry: uiInteractorCookie.Expires,
                secure: uiInteractorCookie.Secure ?? false,
                isHttpOnly: uiInteractorCookie.HttpOnly ?? false,
                sameSite: uiInteractorCookie.SameSite);

            return seleniumCookie;
        }
    }
}
