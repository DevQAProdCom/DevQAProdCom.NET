using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class SeleniumUiElementBehaviorMouseHover(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorMouseHover
    {
        public void MouseHover()
        {
            new Actions(WebDriver)
                .MoveToElement(WebElement)
                .Perform();
        }
    }
}
