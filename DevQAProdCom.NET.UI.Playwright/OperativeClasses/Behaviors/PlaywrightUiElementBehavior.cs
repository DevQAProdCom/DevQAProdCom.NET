using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors
{
    public class PlaywrightUiElementBehavior : UiElementBehavior<PlaywrightUiElement>
    {
        protected IBrowser Browser => UiElement.GetIBrowser();
        protected IBrowserContext BrowserContext => UiElement.GetIBrowserContext();
        protected IPage Page => UiElement.GetPage();
        protected ILocator Locator => UiElement.GetLocator();

        public PlaywrightUiElementBehavior(IBehaviorParameters parameters) : base(parameters) { }
    }
}
