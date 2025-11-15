using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingNameContains : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.NameContains.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("css=", $"[name*='{findOption.Criteria}']");
    }
}
