using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Text
{
    public class SeleniumUiElementBehaviorClearText : SeleniumUiElementBehavior, IUiElementBehaviorClearText
    {
        public SeleniumUiElementBehaviorClearText(IBehaviorParameters parameters) : base(parameters) { }

        public void ClearText()
        {
            WebElement.Clear();
        }
    }
}
