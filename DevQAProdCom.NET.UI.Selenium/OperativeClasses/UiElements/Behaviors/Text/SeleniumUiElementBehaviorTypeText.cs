using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Text
{
    public class SeleniumUiElementBehaviorTypeText : SeleniumUiElementBehavior, IUiElementBehaviorTypeText
    {
        public SeleniumUiElementBehaviorTypeText(IBehaviorParameters parameters) : base(parameters) { }

        public void TypeText(string text)
        {
            UiElement.FocusJs();
            WebElement.Clear();
            WebElement.SendKeys(text);
        }
    }
}
