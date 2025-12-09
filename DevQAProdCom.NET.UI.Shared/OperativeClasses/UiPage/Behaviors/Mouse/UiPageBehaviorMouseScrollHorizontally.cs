using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class UiPageBehaviorMouseScrollHorizontally(IBehaviorParameters parameters) : UiPageBehavior(parameters), IUiPageBehaviorMouseScrollHorizontally
    {
        public void MouseScrollHorizontally(float deltaX)
        {
            UiPage.MouseScroll(deltaX, 0);
        }
    }
}
