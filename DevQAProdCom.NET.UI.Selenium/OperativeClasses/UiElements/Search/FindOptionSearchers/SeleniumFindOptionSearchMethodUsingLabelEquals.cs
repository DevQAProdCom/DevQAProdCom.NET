using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class SeleniumFindOptionSearchMethodUsingLabelEquals : BaseSeleniumFindOptionSearchMethod
    {
        public override string Method => Use.LabelEquals.ToString();
        protected override By GetBy(IFindOption findOption) => By.XPath($".//label[text()='{findOption.Criteria}']");
    }
}
