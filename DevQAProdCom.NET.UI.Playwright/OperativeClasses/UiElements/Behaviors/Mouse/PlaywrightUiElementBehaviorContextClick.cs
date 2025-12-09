using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class PlaywrightUiElementBehaviorContextClick(IBehaviorParameters parameters) : PlaywrightUiElementBehavior(parameters), IUiElementBehaviorContextClick
    {
        public void ContextClick()
        {
            UiElement.ContextClickJs();
        }
    }
}

