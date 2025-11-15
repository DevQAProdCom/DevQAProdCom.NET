namespace DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse
{
    internal interface IBaseMouseBehavior : IMouseScrollBehavior
    {
        void MouseMove(float x, float y);
        void MouseMoveJs(float x, float y);
    }
}
