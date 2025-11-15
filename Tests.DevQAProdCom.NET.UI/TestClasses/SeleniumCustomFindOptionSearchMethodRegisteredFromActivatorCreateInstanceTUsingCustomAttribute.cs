using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using OpenQA.Selenium;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.TestClasses
{
    public class SeleniumCustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute : BaseSeleniumFindOptionSearchMethod
    {
        public override string Method => Const.Ui.CustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute;
        protected override By GetBy(IFindOption findOption) => By.CssSelector($"[{Const.Ui.AttributeForCustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceT}='{findOption.Criteria}']");
    }
}
