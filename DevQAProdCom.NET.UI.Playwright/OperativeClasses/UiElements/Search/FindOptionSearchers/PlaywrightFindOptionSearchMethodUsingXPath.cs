using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingXPath : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.XPath.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("xpath=", findOption.Criteria);
    }
}
