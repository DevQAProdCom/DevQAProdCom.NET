using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class SeleniumUiPageBehaviorMouseScroll(IBehaviorParameters parameters) : SeleniumUiPageBehavior(parameters), IUiPageBehaviorMouseScroll
    {
        public void MouseScroll(float deltaX, float deltaY)
        {
            new Actions(WebDriver)
                    .ScrollByAmount(deltaX.ToInt32(), deltaY.ToInt32())
                    .Perform();
        }
    }
}
