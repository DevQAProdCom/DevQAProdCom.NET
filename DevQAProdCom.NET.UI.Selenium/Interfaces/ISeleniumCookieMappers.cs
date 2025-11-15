using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using SeleniumCookie = OpenQA.Selenium.Cookie;

namespace DevQAProdCom.NET.UI.Selenium.Interfaces
{
    public interface ISeleniumCookieMappers
    {
        public IUiInteractorCookie ToUiInteractorCookie(SeleniumCookie seleniumCookie);
        public SeleniumCookie ToSeleniumCookie(IUiInteractorCookie uiInteractorCookie);
    }
}
