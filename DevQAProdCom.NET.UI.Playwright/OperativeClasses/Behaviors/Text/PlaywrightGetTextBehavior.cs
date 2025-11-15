using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Text;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Text
{
    public class PlaywrightGetTextBehavior : PlaywrightUiElementBehavior, IGetTextBehavior
    {
        public PlaywrightGetTextBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public string GetText()
        {
            return Locator.InputValueAsync().Result.ToStringEmptyIfNull();
        }
    }
}
