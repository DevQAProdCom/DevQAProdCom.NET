using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class SeleniumUiElementBehaviorClick(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorClick
    {
        public void Click()
        {
            new Actions(WebDriver)
                .Click(WebElement)
                .Perform();
        }
    }
}
