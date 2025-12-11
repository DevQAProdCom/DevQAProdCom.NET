using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class PlaywrightUiPageBehaviorMouseDown(IBehaviorParameters parameters) : PlaywrightUiPageBehavior(parameters), IUiPageBehaviorMouseDown
    {
        public void MouseDown()
        {
            Page.Mouse.DownAsync().Wait();
        }
    }
}
