using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Text
{
    public class PlaywrightUiElementBehaviorGetInputText : PlaywrightUiElementBehavior, IUiElementBehaviorGetInputText
    {
        public PlaywrightUiElementBehaviorGetInputText(IBehaviorParameters parameters) : base(parameters) { }

        public string GetInputText()
        {
            return Locator.InputValueAsync().Result.ToStringEmptyIfNull();
        }
    }
}
