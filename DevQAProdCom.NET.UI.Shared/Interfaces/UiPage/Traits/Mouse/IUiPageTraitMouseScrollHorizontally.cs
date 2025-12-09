using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.UI.Shared.Constants;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Traits.Mouse
{
    public interface IUiPageTraitMouseScrollHorizontally
    {
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
