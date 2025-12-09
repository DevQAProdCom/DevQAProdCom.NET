using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.UI.Shared.Constants;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Traits.Mouse
{
    public interface IUiPageTraitMouseScroll
    {
        public void MouseScroll(float deltaX, float deltaY);
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
    }
}
