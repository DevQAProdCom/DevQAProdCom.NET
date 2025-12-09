using DevQAProdCom.NET.UI.Shared.Constants;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElementWaiters
    {
        public IUiElement WaitToExist(TimeSpan? timeout, TimeSpan? pollingInterval = null);
        public IUiElement WaitToExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);
        public IUiElement WaitToNotExist(TimeSpan? timeout, TimeSpan? pollingInterval = null);
        public IUiElement WaitToNotExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);

        public IUiElement WaitToBeDisplayed(TimeSpan? timeout, TimeSpan? pollingInterval = null);
        public IUiElement WaitToBeDisplayed(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);
        public IUiElement WaitToBeHidden(TimeSpan? timeout, TimeSpan? pollingInterval = null);
        public IUiElement WaitToBeHidden(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);

        public IUiElement WaitToBeEnabled(TimeSpan? timeout, TimeSpan? pollingInterval = null);
        public IUiElement WaitToBeEnabled(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);
        public IUiElement WaitToBeDisabled(TimeSpan? timeout, TimeSpan? pollingInterval = null);
        public IUiElement WaitToBeDisabled(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec);
    }
}
