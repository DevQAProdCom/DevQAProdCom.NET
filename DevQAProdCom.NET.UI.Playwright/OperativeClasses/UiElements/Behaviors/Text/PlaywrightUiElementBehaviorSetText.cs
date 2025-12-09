using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Text
{
    public class PlaywrightUiElementBehaviorSetText : PlaywrightUiElementBehavior, IUiElementBehaviorSetText
    {
        public PlaywrightUiElementBehaviorSetText(IBehaviorParameters parameters) : base(parameters) { }

        public void SetText(string text)
        {
            UiElement.FocusJs();
            Locator.ClearAsync().Wait();
            Locator.FillAsync(text).GetAwaiter().GetResult();
        }
    }
}
