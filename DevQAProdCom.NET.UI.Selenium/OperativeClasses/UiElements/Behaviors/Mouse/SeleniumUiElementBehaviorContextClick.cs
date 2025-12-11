using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class SeleniumUiElementBehaviorContextClick(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorContextClick
    {
        public void ContextClick()
        {
            new Actions(WebDriver)
                .ContextClick(WebElement)
                .Perform();
        }
    }
}
