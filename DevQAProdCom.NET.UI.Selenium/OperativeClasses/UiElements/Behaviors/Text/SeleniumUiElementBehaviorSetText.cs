using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Text
{
    public class SeleniumUiElementBehaviorSetText : SeleniumUiElementBehavior, IUiElementBehaviorSetText
    {
        public SeleniumUiElementBehaviorSetText(IBehaviorParameters parameters) : base(parameters) { }

        public void SetText(string text)
        {
            UiElement.FocusJs();
            WebElement.Clear();
            WebElement.SendKeys(text);
        }
    }
}
