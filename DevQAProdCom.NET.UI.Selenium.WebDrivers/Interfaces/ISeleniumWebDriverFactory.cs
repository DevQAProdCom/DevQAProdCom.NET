using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.WebDrivers.Interfaces
{
    public interface ISeleniumWebDriverFactory
    {
        public (IWebDriver Driver, ISeleniumWebDriverConfiguration? Configuration) CreateWebDriver(ISeleniumWebDriverConfiguration? configuration = null);
    }
}
