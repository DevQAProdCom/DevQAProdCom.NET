using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class UiPageBehaviorMouseScrollVertically(IBehaviorParameters parameters) : UiPageBehavior(parameters), IUiPageBehaviorMouseScrollVertically
    {
        public void MouseScrollVertically(float deltaY)
        {
            UiPage.MouseScroll(0, deltaY);
        }
    }
}
