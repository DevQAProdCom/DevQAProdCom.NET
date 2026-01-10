using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using OpenQA.Selenium;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.TestClasses
{
    public class SeleniumCustomFindOptionSearchMethod(IUiInteractorsManagersProvider uiInteractorsManagersProvider) : BaseSeleniumFindOptionSearchMethod
    {
        public override string Method => Const.Ui.CustomFindOptionSearchMethod;
        protected override By GetBy(IFindOption findOption) => By.CssSelector($"[{Const.Ui.AttributeForCustomFindOptionSearchMethod}='{findOption.Criteria}']");
    }
}
