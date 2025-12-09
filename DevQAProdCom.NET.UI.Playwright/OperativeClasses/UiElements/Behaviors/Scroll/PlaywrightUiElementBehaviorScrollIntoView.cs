using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Scroll;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Scroll
{
    public class PlaywrightUiElementBehaviorScrollIntoView(IBehaviorParameters parameters) : PlaywrightUiElementBehavior(parameters), IUiElementBehaviorScrollIntoView
    {
        public void ScrollIntoView()
        {
            Locator.ScrollIntoViewIfNeededAsync().Wait();
        }
    }
}
