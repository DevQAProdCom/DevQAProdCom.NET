using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiPage.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class PlaywrightUiPageBehaviorMouseMove(IBehaviorParameters parameters) : PlaywrightUiPageBehavior(parameters), IUiPageBehaviorMouseMove
    {
        public void MouseMove(float x, float y)
        {
            Page.Mouse.MoveAsync(x, y).Wait();
        }
    }
}
