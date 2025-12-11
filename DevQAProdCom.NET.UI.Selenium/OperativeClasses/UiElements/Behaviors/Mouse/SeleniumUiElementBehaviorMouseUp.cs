using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class SeleniumUiElementBehaviorMouseUp(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorMouseUp
    {
        public void MouseUp()
        {
            new Actions(WebDriver)
                .Release(WebElement)
                .Perform();
        }
    }
}
