using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Mouse
{
    public class PlaywrightUiElementMouseBehavior : PlaywrightBaseMouseBehavior, IUiElementMouseBehavior
    {
        protected PlaywrightUiElement UiElement { get; }
        protected ILocator Locator => UiElement.GetLocator();

        public PlaywrightUiElementMouseBehavior(IBehaviorParameters parameters) : base(parameters)
        {
            UiElement = Parameters.Get<PlaywrightUiElement>(SharedUiConstants.IUiElement);
            Page = UiElement.GetPage();
        }

        public void MouseClick()
        {
            Locator.ClickAsync().Wait();
        }

        public void MouseDoubleClick()
        {
            Locator.DblClickAsync().Wait();
        }

        public void MouseDown()
        {
            MouseHover();
            Page.Mouse.DownAsync().Wait();
        }

        public void MouseDownJs()
        {
            MouseHover();
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.MouseDownJavaScriptFilePath));
        }

        public void MouseUp()
        {
            MouseHover();
            Page.Mouse.UpAsync().Wait();
        }

        public void MouseUpJs()
        {
            MouseHover();
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.MouseUpJavaScriptFilePath));
        }

        public void MouseHover()
        {
            Locator.HoverAsync().Wait();
        }

        public void MouseContextClickJs()
        {
            Locator.HoverAsync().Wait();
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.MouseContextClickJavaScriptFilePath));
        }

        /// <summary>
        /// If your page relies on the dragover event being dispatched, you need at least two mouse moves to trigger it in all browsers.
        /// To reliably issue the second mouse move, repeat your mouse.move() or locator.hover() twice.
        /// The sequence of operations would be: hover the drag element, mouse down, hover the drop element, hover the drop element second time, mouse up.
        /// </summary>
        /// <param name="uiElementToDrop"></param>
        /// <returns></returns>
        public void DragAndDrop(IUiElement uiElementToDrop)
        {
            var dropToPlaywrightUiElement = uiElementToDrop as PlaywrightUiElement;
            var dropToLocator = dropToPlaywrightUiElement.GetLocator();

            Locator.HoverAsync().Wait();
            Page.Mouse.DownAsync().Wait();
            dropToLocator.HoverAsync().Wait();
            dropToLocator.HoverAsync().Wait();
            Page.Mouse.UpAsync().Wait();
        }

        /// <summary>
        /// If your page relies on the dragover event being dispatched, you need at least two mouse moves to trigger it in all browsers.
        /// To reliably issue the second mouse move, repeat your mouse.move() or locator.hover() twice.
        /// The sequence of operations would be: hover the drag element, mouse down, hover the drop element, hover the drop element second time, mouse up.
        /// </summary>
        /// <param name="uiElementToDrop"></param>
        /// <returns></returns>
        public void DragAndDropByOffset(float offsetX, float offsetY)
        {
            var draggableElementLocation = UiElement.GetLocation();
            Page.Mouse.MoveAsync(draggableElementLocation.X, draggableElementLocation.Y).Wait();
            Page.Mouse.DownAsync().Wait();
            var droppablePositionX = draggableElementLocation.X + offsetX;
            var droppablePositionY = draggableElementLocation.Y + offsetY;
            Page.Mouse.MoveAsync(droppablePositionX, droppablePositionY).Wait();
            Page.Mouse.MoveAsync(droppablePositionX, droppablePositionY).Wait();
            Page.Mouse.UpAsync().Wait();
        }
    }
}
