using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.TestClasses
{
    public class PlaywrightCustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public PlaywrightCustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute(TestClassForDiInjection someAdditionalDiInjectedClassRequiredForFindOptionSearchMethod)
        {

        }

        public override string Method => Const.Ui.CustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute;
        protected override string GetSelector(IFindOption findOption) => string.Concat("css=", $"[{Const.Ui.AttributeForCustomFindOptionSearchMethodRegisteredFromDi}='{findOption.Criteria}']");
    }
}
