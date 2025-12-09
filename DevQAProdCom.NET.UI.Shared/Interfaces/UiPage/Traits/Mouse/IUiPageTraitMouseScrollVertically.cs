using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.UI.Shared.Constants;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Traits.Mouse
{
    public interface IUiPageTraitMouseScrollVertically
    {
        public void MouseScrollVertically(float deltaY);
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
    }
}
