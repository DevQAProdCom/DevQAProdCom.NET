using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class PlaywrightUiElementBehaviorDoubleClick(IBehaviorParameters parameters) : PlaywrightUiElementBehavior(parameters), IUiElementBehaviorDoubleClick
    {
        public void DoubleClick()
        {
            Locator.DblClickAsync().Wait();
        }
    }
}
