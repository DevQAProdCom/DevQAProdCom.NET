using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingDataTestIdContains : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Use.DataTestIdContains.ToString();
        protected override string GetSelector(IFindOption findOption) => string.Concat("css=", $"[data-testid*='{findOption.Criteria}']");
    }
}
