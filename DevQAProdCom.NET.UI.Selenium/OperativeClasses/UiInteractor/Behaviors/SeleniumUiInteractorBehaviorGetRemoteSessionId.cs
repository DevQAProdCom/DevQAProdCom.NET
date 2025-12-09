using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor.Behaviors;
using OpenQA.Selenium.Remote;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractor.Behaviors
{
    public class SeleniumUiInteractorBehaviorGetRemoteSessionId(IBehaviorParameters parameters) : SeleniumUiInteractorBehavior(parameters), IUiInteractorBehaviorGetRemoteSessionId
    {
        public string? GetRemoteSessionId()
        {
            if (WebDriver.GetType() == typeof(RemoteWebDriver))
                return ((RemoteWebDriver)WebDriver).SessionId.ToString();

            return null;
        }
    }
}
