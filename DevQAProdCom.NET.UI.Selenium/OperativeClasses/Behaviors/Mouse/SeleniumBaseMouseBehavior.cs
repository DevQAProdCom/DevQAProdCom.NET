using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Mouse
{
    public class SeleniumBaseMouseBehavior : BaseMouseBehavior
    {
        protected virtual IWebDriver WebDriver { get; set; }

        public SeleniumBaseMouseBehavior(IBehaviorParameters parameters) : base(parameters)
        {
            WebDriver = parameters.GetOrDefault<IWebDriver>(SharedUiConstants.IWebDriver);
        }

        public override void MouseMove(float x, float y)
        {
            new Actions(WebDriver)
            .MoveByOffset(x.ToInt32(), y.ToInt32())
            .Perform();
        }

        public override void MouseScroll(float deltaX, float deltaY)
        {
            new Actions(WebDriver)
                    .ScrollByAmount(deltaX.ToInt32(), deltaY.ToInt32())
                    .Perform();
        }

        public override void MouseScrollVertically(float deltaY)
        {
            new Actions(WebDriver)
                .ScrollByAmount(0, deltaY.ToInt32())
                .Perform();
        }

        public override void MouseScrollHorizontally(float deltaX)
        {
            new Actions(WebDriver)
                .ScrollByAmount(deltaX.ToInt32(), 0)
                .Perform();
        }
    }
}
