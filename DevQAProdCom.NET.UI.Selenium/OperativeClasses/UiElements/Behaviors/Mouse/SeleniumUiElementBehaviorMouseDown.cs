using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class SeleniumUiElementBehaviorMouseDown(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorMouseDown
    {
        public void MouseDown()
        {
            new Actions(WebDriver)
                .ClickAndHold(WebElement)
                .Perform();
        }
    }
}
