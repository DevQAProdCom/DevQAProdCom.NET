using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.TestClasses
{
    public class PlaywrightCustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Const.Ui.CustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute;
        protected override string GetSelector(IFindOption findOption) => string.Concat("css=", $"[{Const.Ui.AttributeForCustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceT}='{findOption.Criteria}']");
    }
}
