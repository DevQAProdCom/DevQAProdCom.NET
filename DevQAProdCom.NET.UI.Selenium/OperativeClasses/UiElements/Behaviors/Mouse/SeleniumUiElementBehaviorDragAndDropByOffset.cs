using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class SeleniumUiElementBehaviorDragAndDropByOffset(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorDragAndDropByOffset
    {
        /// <summary>
        /// If your page relies on the dragover event being dispatched, you need at least two mouse moves to trigger it in all browsers.
        /// To reliably issue the second mouse move, repeat your mouse.move() or hover() twice.
        /// The sequence of operations would be: hover the drag element, mouse down, hover the drop element, hover the drop element second time, mouse up.
        /// <returns></returns>
        public void DragAndDropByOffset(float offsetX, float offsetY)
        {
            var draggableElementLocation = UiElement.GetLocation();
            var droppablePositionX = draggableElementLocation.X + offsetX;
            var droppablePositionY = draggableElementLocation.Y + offsetY;

            UiPage.MouseMove(draggableElementLocation.X, draggableElementLocation.Y);
            UiElement.MouseDown();
            UiPage.MouseMoveJs(droppablePositionX, droppablePositionY);
            UiPage.MouseMoveJs(droppablePositionX, droppablePositionY);
            UiElement.MouseUpJs();
        }
    }
}
