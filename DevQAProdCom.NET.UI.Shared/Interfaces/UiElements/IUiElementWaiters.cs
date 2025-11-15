using DevQAProdCom.NET.UI.Shared.Constants;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElementWaiters
    {
        public IUiElement WaitToExist(TimeSpan? timeout = null, TimeSpan? pollingInterval = null);
        public IUiElement WaitToExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);
        public IUiElement WaitToNotExist(TimeSpan? timeout = null, TimeSpan? pollingInterval = null);
        public IUiElement WaitToNotExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);
        public IUiElement WaitToBeVisible(TimeSpan? timeout = null, TimeSpan? pollingInterval = null);
        public IUiElement WaitToBeVisible(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);
        public IUiElement WaitToNotBeVisible(TimeSpan? timeout = null, TimeSpan? pollingInterval = null);
        public IUiElement WaitToNotBeVisible(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);
    }
}
