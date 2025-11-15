using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class SeleniumFindOptionSearchMethodUsingTextContains : BaseSeleniumFindOptionSearchMethod
    {
        public override string Method => Use.TextContains.ToString();
        protected override By GetBy(IFindOption findOption) => By.XPath($".//*[contains(text(),'{findOption.Criteria}')]");
    }
}
