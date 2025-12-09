using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class SeleniumUiElementBehaviorDragAndDrop(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorDragAndDrop
    {
        public void DragAndDrop(IUiElement uiElementToDrop)
        {
            var dropToSeleniumUiElement = uiElementToDrop as SeleniumUiElement;
            var dragWebElement = WebElement;
            var dropToWebElement = dropToSeleniumUiElement.GetWebElement();
            new Actions(WebDriver)
                .DragAndDrop(dragWebElement, dropToWebElement)
                .Perform();
        }
    }
}
