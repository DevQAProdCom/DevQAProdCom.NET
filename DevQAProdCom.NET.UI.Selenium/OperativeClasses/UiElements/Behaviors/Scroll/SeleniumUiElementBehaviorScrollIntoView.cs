using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Scroll;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Scroll
{
    public class SeleniumUiElementBehaviorScrollIntoView(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorScrollIntoView
    {
        public void ScrollIntoView()
        {
            new Actions(WebDriver)
                .ScrollToElement(WebElement)
                .Perform();
        }
    }
}
