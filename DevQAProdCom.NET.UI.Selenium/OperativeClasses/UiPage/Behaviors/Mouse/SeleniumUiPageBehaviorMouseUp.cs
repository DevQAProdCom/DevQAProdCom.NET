using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class SeleniumUiPageBehaviorMouseUp(IBehaviorParameters parameters) : SeleniumUiPageBehavior(parameters), IUiPageBehaviorMouseUp
    {
        public void MouseUp()
        {
            new Actions(WebDriver)
                .Release()
                .Perform();
        }
    }
}
