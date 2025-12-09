using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Keyboard
{
    public class PlaywrightUiElementBehaviorSendText(IBehaviorParameters parameters) : PlaywrightUiElementBehavior(parameters), IUiElementBehaviorSendText
    {
        public void SendText(string textKeys)
        {
            UiElement.FocusJs();
            Page.Keyboard.TypeAsync(textKeys);
        }
    }
}
