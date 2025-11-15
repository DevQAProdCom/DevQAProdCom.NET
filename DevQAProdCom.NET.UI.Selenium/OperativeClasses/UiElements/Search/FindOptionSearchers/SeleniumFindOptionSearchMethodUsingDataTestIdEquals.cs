using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class SeleniumFindOptionSearchMethodUsingDataTestIdEquals : BaseSeleniumFindOptionSearchMethod
    {
        public override string Method => Use.DataTestIdEquals.ToString();
        protected override By GetBy(IFindOption findOption) => By.CssSelector($"[data-testid='{findOption.Criteria}']");
    }
}
