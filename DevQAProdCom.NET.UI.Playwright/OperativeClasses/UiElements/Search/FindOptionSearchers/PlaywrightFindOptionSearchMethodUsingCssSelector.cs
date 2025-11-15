using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingCssSelector : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.CssSelector.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("css=", findOption.Criteria);
    }
}
