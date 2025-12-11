using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class PlaywrightUiElementBehaviorMouseDown(IBehaviorParameters parameters) : PlaywrightUiElementBehavior(parameters), IUiElementBehaviorMouseDown
    {
        public void MouseDown()
        {
            UiElement.MouseHover();
            UiPage.MouseDown();
        }
    }
}
