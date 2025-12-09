using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using OpenQA.Selenium;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors
{
    public class SeleniumUiElementBehavior : UiElementBehavior<SeleniumUiElement>
    {
        protected IWebDriver WebDriver => UiElement.GetIWebDriver();
        protected IWebElement WebElement => UiElement.GetWebElement();

        public SeleniumUiElementBehavior(IBehaviorParameters parameters) : base(parameters) { }
    }
}
