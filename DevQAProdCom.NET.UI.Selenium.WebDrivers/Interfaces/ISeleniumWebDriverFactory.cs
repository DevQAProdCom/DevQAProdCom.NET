using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.WebDrivers.Interfaces
{
    public interface ISeleniumWebDriverFactory
    {
        public (IWebDriver Driver, ISeleniumUiInteractorConfiguration? Configuration) CreateWebDriver(ISeleniumUiInteractorConfiguration? configuration = null);
    }
}
