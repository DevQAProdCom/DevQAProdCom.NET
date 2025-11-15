using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class SeleniumFindOptionSearchMethodUsingPlaceholderEquals : BaseSeleniumFindOptionSearchMethod
    {
        public override string Method => Use.PlaceholderEquals.ToString();
        protected override By GetBy(IFindOption findOption) => By.CssSelector($"[placeholder='{findOption.Criteria}']");
    }
}
