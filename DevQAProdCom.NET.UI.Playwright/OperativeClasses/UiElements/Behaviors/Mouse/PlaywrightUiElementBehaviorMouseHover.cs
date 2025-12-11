using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class PlaywrightUiElementBehaviorMouseHover(IBehaviorParameters parameters) : PlaywrightUiElementBehavior(parameters), IUiElementBehaviorMouseHover
    {
        public void MouseHover()
        {
            Locator.HoverAsync().Wait();
        }
    }
}
