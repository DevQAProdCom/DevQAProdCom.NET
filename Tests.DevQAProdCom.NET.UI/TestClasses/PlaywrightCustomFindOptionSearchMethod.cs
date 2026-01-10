using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.TestClasses
{
    public class PlaywrightCustomFindOptionSearchMethod(IUiInteractorsManagersProvider uiInteractorsManagersProvider) : BasePlaywrightFindOptionSearchMethodWithCustomSelector
    {
        public override string Method => Const.Ui.CustomFindOptionSearchMethod;
        protected override string GetSelector(IFindOption findOption) => string.Concat("css=", $"[{Const.Ui.AttributeForCustomFindOptionSearchMethod}='{findOption.Criteria}']");
    }
}
