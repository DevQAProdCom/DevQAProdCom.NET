using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Mouse
{
    public class SeleniumUiElementMouseBehavior : SeleniumBaseMouseBehavior, IUiElementMouseBehavior
    {
        protected SeleniumUiElement UiElement { get; }
        protected IWebElement WebElement => UiElement.GetWebElement();

        public SeleniumUiElementMouseBehavior(IBehaviorParameters parameters) : base(parameters)
        {
            UiElement = Parameters.Get<SeleniumUiElement>(SharedUiConstants.IUiElement);
            WebDriver = UiElement.GetIWebDriver();
        }

        public void MouseClick()
        {
            new Actions(WebDriver)
                .Click(WebElement)
                .Perform();
        }

        public void MouseDoubleClick()
        {
            new Actions(WebDriver)
                .DoubleClick(WebElement)
                .Perform();
        }

        public void MouseDown()
        {
            new Actions(WebDriver)
                .ClickAndHold(WebElement)
                .Perform();
        }

        public void MouseDownJs()
        {
            MouseHover();
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.MouseDownJavaScriptFilePath));
        }

        public void MouseUp()
        {
            new Actions(WebDriver)
                .Release(WebElement)
                .Perform();
        }

        public void MouseUpJs()
        {
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.MouseUpJavaScriptFilePath));
        }
     
        public void MouseHover()
        {
            new Actions(WebDriver)
                .MoveToElement(WebElement)
                .Perform();
        }

        public void MouseContextClickJs()
        {
            new Actions(WebDriver)
                .ContextClick(WebElement)
                .Perform();
        }

        public void DragAndDrop(IUiElement uiElementToDrop)
        {
            var dropToSeleniumUiElement = uiElementToDrop as SeleniumUiElement;
            var dragWebElement = WebElement;
            var dropToWebElement = dropToSeleniumUiElement.GetWebElement();
            new Actions(WebDriver)
                .DragAndDrop(dragWebElement, dropToWebElement)
                .Perform();
        }

        public void DragAndDropByOffset(float offsetX, float offsetY)
        {
            var draggableElementLocation = UiElement.GetLocation();
            var droppablePositionX = draggableElementLocation.X + offsetX;
            var droppablePositionY = draggableElementLocation.Y + offsetY;

            MouseMove(draggableElementLocation.X, draggableElementLocation.Y);
            MouseDown();
            MouseMoveJs(droppablePositionX, droppablePositionY);
            MouseMoveJs(droppablePositionX, droppablePositionY);
            MouseUpJs();
        }
    }
}
