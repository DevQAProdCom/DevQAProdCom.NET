using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingLinkTextContains : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.LinkTextContains.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("xpath=", $".//a[contains(text(),'{findOption.Criteria}')]");
    }
}
