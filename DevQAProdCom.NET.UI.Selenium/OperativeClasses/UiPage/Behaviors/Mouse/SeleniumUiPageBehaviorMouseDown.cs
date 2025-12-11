using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class SeleniumUiPageBehaviorMouseDown(IBehaviorParameters parameters) : SeleniumUiPageBehavior(parameters), IUiPageBehaviorMouseDown
    {
        public void MouseDown()
        {
            new Actions(WebDriver)
                .Release()
                .Perform();
        }
    }
}
