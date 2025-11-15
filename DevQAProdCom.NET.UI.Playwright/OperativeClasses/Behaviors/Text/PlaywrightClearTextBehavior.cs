using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Text;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Text
{
    public class PlaywrightClearTextBehavior : PlaywrightUiElementBehavior, IClearTextBehavior
    {
        public PlaywrightClearTextBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public void ClearText()
        {
            Locator.ClearAsync().Wait();
        }
    }
}
