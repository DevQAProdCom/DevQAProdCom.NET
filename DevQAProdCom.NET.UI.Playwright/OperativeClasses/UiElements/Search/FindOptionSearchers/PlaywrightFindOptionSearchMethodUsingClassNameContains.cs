using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingClassNameContains : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.ClassNameContains.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("css=", $"[class*='{findOption.Criteria}']");
    }
}
