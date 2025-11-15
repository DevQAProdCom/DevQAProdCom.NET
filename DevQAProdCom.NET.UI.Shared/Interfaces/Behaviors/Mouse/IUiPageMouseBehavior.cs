using DevQAProdCom.NET.UI.Shared.Constants;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse
{
    public interface IUiPageMouseBehavior
    {
        void MouseMove(float x, float y);

        void MouseScroll(float deltaX, float deltaY);
        void MouseScroll(float deltaX, float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec);

        void MouseScrollVertically(float deltaY);
        void MouseScrollVertically(float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec);

        void MouseScrollHorizontally(float deltaX);
        void MouseScrollHorizontally(float deltaX, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec);
    }
}
