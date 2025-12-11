using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class PlaywrightUiPageBehaviorMouseScroll(IBehaviorParameters parameters) : PlaywrightUiPageBehavior(parameters), IUiPageBehaviorMouseScroll
    {
        public void MouseScroll(float deltaX, float deltaY)
        {
            Page.Mouse.WheelAsync(deltaX, deltaY).Wait();
        }
    }
}
