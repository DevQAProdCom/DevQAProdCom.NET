using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.OperativeClasses;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public abstract class BaseMouseBehavior : BaseBehavior, IBaseMouseBehavior
    {
        protected IExecuteJavaScript JavaScriptExecutor;

        public BaseMouseBehavior(IBehaviorParameters parameters) : base(parameters)
        {
            JavaScriptExecutor = parameters.Get<IExecuteJavaScript>(SharedUiConstants.IExecuteJavaScript);
        }

        public abstract void MouseMove(float x, float y);

        public void MouseMoveJs(float x, float y)
        {
            JavaScriptExecutor.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.MouseMoveJavaScriptFilePath),
                new KeyValuePair<string, object>(SharedUiConstants.XArgument, x), new KeyValuePair<string, object>(SharedUiConstants.YArgument, y));
        }

        public abstract void MouseScroll(float deltaX, float deltaY);

        public void MouseScroll(float deltaX, float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {

            Wait.WithTimeout(TimeSpan.FromSeconds(timeoutSec))
                .PollingEvery(TimeSpan.FromSeconds(pollingIntervalSec))
                .Until(() =>
                {
                    MouseScroll(deltaX, deltaY);
                    return untilCondition();
                });
        }

        public abstract void MouseScrollVertically(float deltaY);

        public void MouseScrollVertically(float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            Wait.WithTimeout(TimeSpan.FromSeconds(timeoutSec))
               .PollingEvery(TimeSpan.FromSeconds(pollingIntervalSec))
               .Until(() =>
               {
                   MouseScrollVertically(deltaY);
                   return untilCondition();
               });
        }

        public abstract void MouseScrollHorizontally(float deltaX);

        public void MouseScrollHorizontally(float deltaX, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            Wait.WithTimeout(TimeSpan.FromSeconds(timeoutSec))
                   .PollingEvery(TimeSpan.FromSeconds(pollingIntervalSec))
                   .Until(() =>
                   {
                       MouseScrollHorizontally(deltaX);
                       return untilCondition();
                   });
        }
    }
}
