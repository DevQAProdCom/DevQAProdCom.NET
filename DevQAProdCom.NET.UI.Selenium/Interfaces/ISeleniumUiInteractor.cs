using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.Interfaces
{
    public interface ISeleniumUiInteractor : IUiInteractor
    {
        public IWebDriver GetWebDriver();
    }
}
