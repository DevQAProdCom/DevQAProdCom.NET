using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Text;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Text
{
    public class SeleniumClearTextBehavior : SeleniumUiElementBehavior, IClearTextBehavior
    {
        public SeleniumClearTextBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public void ClearText()
        {
            WebElement.Clear();
        }
    }
}
