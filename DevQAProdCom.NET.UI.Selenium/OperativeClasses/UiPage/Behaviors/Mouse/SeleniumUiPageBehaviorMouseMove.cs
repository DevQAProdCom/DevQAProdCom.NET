using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class SeleniumUiPageBehaviorMouseMove(IBehaviorParameters parameters) : SeleniumUiPageBehavior(parameters), IUiPageBehaviorMouseMove
    {
        public void MouseMove(float x, float y)
        {
            new Actions(WebDriver)
            .MoveByOffset(x.ToInt32(), y.ToInt32())
            .Perform();
        }
    }
}
