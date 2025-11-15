using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Text
{
    public class PlaywrightUiElementFulfillTextBehavior : BaseUiElementFulfillTextBehavior<PlaywrightUiElement>
    {
        protected ILocator Locator => UiElement.GetLocator();

        public PlaywrightUiElementFulfillTextBehavior(IBehaviorParameters parameters) : base(parameters)
        {

        }

        public override void SetText(string text)
        {
            UiElement.FocusJs();
            Locator.ClearAsync().Wait();
            Locator.FillAsync(text).GetAwaiter().GetResult();
        }

        public override void TypeText(string text)
        {
            UiElement.FocusJs();
            Locator.ClearAsync().Wait();
            Locator.PressSequentiallyAsync(text).GetAwaiter().GetResult();
        }
    }
}
