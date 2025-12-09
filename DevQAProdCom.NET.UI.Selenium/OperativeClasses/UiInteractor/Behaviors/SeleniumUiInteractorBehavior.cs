using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor.Behaviors;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractor.Behaviors
{
    public class SeleniumUiInteractorBehavior(IBehaviorParameters parameters) : UiInteractorBehavior(parameters)
    {
        private IWebDriver? _webDriver;
        protected IWebDriver? WebDriver => _webDriver ??= Parameters.GetOrDefault<IWebDriver>(SharedUiConstants.IWebDriver);
    }
}
