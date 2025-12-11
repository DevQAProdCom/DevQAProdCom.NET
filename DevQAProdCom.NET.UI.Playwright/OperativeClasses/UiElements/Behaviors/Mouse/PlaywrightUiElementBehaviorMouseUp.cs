using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class PlaywrightUiElementBehaviorMouseUp(IBehaviorParameters parameters) : PlaywrightUiElementBehavior(parameters), IUiElementBehaviorMouseUp
    {
        public void MouseUp()
        {
            UiElement.MouseHover();
            UiPage.MouseUp();
        }
    }
}
