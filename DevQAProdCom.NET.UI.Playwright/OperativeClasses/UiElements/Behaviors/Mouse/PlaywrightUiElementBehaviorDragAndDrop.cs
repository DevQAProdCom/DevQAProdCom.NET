using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class PlaywrightUiElementBehaviorDragAndDrop(IBehaviorParameters parameters) : PlaywrightUiElementBehavior(parameters), IUiElementBehaviorDragAndDrop
    {
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

            UiElement.MouseHover();
            UiElement.MouseDown();
            dropToLocator.HoverAsync().Wait();
            dropToLocator.HoverAsync().Wait();
            UiElement.MouseUp();
        }
    }
}
