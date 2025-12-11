using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors
{
    public class SeleniumUiElementBehavior : UiElementBehavior<SeleniumUiElement>
    {
        protected IWebDriver WebDriver => UiElement.GetIWebDriver();
        protected IWebElement WebElement => UiElement.GetWebElement();

        public SeleniumUiElementBehavior(IBehaviorParameters parameters) : base(parameters) { }
    }
}
