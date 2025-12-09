using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Text
{
    public class PlaywrightUiElementBehaviorClearText : PlaywrightUiElementBehavior, IUiElementBehaviorClearText
    {
        public PlaywrightUiElementBehaviorClearText(IBehaviorParameters parameters) : base(parameters) { }

        public void ClearText()
        {
            Locator.ClearAsync().Wait();
        }
    }
}
