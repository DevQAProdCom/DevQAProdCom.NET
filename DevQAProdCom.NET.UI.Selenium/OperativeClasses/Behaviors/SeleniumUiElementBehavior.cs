using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors
{
    public class SeleniumUiElementBehavior : UiElementBehavior<SeleniumUiElement>
    {
        protected IWebDriver WebDriver => UiElement.GetIWebDriver();
        protected IWebElement WebElement => UiElement.GetWebElement();

        public SeleniumUiElementBehavior(IBehaviorParameters parameters) : base(parameters) { }
    }
}
