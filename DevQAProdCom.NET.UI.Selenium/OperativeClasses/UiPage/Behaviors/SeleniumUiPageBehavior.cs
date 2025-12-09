using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiPage.Behaviors
{
    public class SeleniumUiPageBehavior(IBehaviorParameters parameters) : UiPageBehavior(parameters)
    {
        private IWebDriver? _webDriver;
        protected IWebDriver? WebDriver => _webDriver ??= Parameters.GetOrDefault<IWebDriver>(SharedUiConstants.IWebDriver);
    }
}
