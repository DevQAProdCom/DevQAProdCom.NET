using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingTextEquals : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.TextEquals.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("xpath=", $".//*[text()='{findOption.Criteria}']");
    }
}
