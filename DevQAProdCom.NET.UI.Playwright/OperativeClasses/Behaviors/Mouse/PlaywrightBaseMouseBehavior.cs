using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Mouse
{
    public class PlaywrightBaseMouseBehavior : BaseMouseBehavior
    {
        protected virtual IPage Page { get; set; }

        public PlaywrightBaseMouseBehavior(IBehaviorParameters parameters) : base(parameters)
        {
            Page = parameters.GetOrDefault<IPage>(SharedUiConstants.IUiPage);
        }

        public override void MouseMove(float x, float y)
        {
            Page.Mouse.MoveAsync(x, y).Wait();
        }

        public override void MouseScroll(float deltaX, float deltaY)
        {
            Page.Mouse.WheelAsync(deltaX, deltaY).Wait();
        }

        public override void MouseScrollVertically(float deltaY)
        {
            Page.Mouse.WheelAsync(0, deltaY).Wait();
        }

        public override void MouseScrollHorizontally(float deltaX)
        {
            Page.Mouse.WheelAsync(deltaX, 0).Wait();
        }
    }
}
