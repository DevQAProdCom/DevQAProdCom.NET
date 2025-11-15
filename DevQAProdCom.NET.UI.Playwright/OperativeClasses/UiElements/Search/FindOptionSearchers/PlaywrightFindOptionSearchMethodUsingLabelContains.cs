using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingLabelContains : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.LabelContains.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("xpath=", $".//label[contains(text(),'{findOption.Criteria}')]");
    }
}
