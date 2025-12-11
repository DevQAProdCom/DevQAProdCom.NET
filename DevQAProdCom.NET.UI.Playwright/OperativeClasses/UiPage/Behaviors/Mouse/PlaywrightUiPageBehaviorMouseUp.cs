using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class PlaywrightUiPageBehaviorMouseUp(IBehaviorParameters parameters) : PlaywrightUiPageBehavior(parameters), IUiPageBehaviorMouseUp
    {
        public void MouseUp()
        {
            Page.Mouse.UpAsync().Wait();
        }
    }
}
