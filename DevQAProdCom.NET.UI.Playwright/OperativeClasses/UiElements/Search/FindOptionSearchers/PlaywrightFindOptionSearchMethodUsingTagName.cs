using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingTagName : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.TagName.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("xpath=", $".//{findOption.Criteria}");
    }
}
