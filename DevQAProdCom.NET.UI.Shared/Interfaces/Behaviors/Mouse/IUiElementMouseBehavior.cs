using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse
{
    public interface IUiElementMouseBehavior : IBehavior
    {
        void MouseClick();
        void MouseDoubleClick();
        void MouseContextClickJs();
        void MouseDown();
        void MouseDownJs();
        void MouseUp();
        void MouseUpJs();
        void MouseHover();
        void MouseMove(float x, float y);
        void MouseMoveJs(float x, float y);

        void DragAndDrop(IUiElement uiElementToDrop);
        void DragAndDropByOffset(float offsetX, float offsetY);

        void MouseScroll(float deltaX, float deltaY);
        void MouseScrollVertically(float deltaY);
        void MouseScrollHorizontally(float deltaX);
    }
}
