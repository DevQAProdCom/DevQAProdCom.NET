using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Text
{
    public class SeleniumUiElementFulfillTextBehavior : BaseUiElementFulfillTextBehavior<SeleniumUiElement>
    {
        protected IWebElement WebElement => UiElement.GetWebElement();
        public SeleniumUiElementFulfillTextBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public override void TypeText(string text)
        {
            UiElement.FocusJs();
            WebElement.Clear();
            WebElement.SendKeys(text);
        }

        public override void SetText(string text)
        {
            UiElement.FocusJs();
            WebElement.Clear();
            WebElement.SendKeys(text);
        }
    }
}
