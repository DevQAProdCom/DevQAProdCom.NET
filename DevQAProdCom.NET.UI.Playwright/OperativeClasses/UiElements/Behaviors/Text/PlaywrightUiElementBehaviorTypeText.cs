using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Text
{
    public class PlaywrightUiElementBehaviorTypeText : PlaywrightUiElementBehavior, IUiElementBehaviorTypeText
    {
        public PlaywrightUiElementBehaviorTypeText(IBehaviorParameters parameters) : base(parameters) { }

        public void TypeText(string text)
        {
            UiElement.FocusJs();
            Locator.ClearAsync().Wait();
            Locator.PressSequentiallyAsync(text).GetAwaiter().GetResult();
        }
    }
}
