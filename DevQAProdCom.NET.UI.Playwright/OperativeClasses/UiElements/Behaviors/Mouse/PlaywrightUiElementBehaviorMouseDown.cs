using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class PlaywrightUiElementBehaviorMouseDown(IBehaviorParameters parameters) : PlaywrightUiElementBehavior(parameters), IUiElementBehaviorMouseDown
    {
        public void MouseDown()
        {
            UiElement.MouseHover();
            Page.Mouse.DownAsync().Wait();
        }
    }
}
