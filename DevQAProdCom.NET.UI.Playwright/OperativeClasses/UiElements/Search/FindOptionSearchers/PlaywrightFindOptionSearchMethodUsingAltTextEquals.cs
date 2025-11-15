using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingAltTextEquals : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.AltTextEquals.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("css=", $"[alt='{findOption.Criteria}']");
    }
}
