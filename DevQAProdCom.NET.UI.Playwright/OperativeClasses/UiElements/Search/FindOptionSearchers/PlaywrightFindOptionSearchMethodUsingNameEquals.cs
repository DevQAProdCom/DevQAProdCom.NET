using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingNameEquals : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.NameEquals.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("css=", $"[name='{findOption.Criteria}']");
    }
}
