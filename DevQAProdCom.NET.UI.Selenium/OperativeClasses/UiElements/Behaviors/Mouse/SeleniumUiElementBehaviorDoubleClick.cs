using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class SeleniumUiElementBehaviorDoubleClick(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorDoubleClick
    {
        public void DoubleClick()
        {
            new Actions(WebDriver)
                .DoubleClick(WebElement)
                .Perform();
        }
    }
}
