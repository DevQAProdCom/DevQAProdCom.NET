namespace DevQAProdCom.NET.Global.Helpers
{
    public class FluentWait
    {
        private TimeSpan _timeout = TimeSpan.FromSeconds(30);
        private TimeSpan _pollingInterval = TimeSpan.FromSeconds(1);
        private Func<bool>? _condition;
        private List<Type> _ignoredExceptionTypes = new();
        private string? _externalErrorMessage = "Timeout Exception";
        private bool _throwTimeoutException = true;
        private bool _ignoreAllExceptions = false;

        public FluentWait()
        {
        }

        public FluentWait(TimeSpan timeout)
        {
            _timeout = timeout;
        }

        public FluentWait(TimeSpan timeout, TimeSpan pollingInterval)
        {
            _timeout = timeout;
            _pollingInterval = pollingInterval;
        }

        public FluentWait(TimeSpan timeout, TimeSpan pollingInterval, List<Type> ignoredExceptionTypes)
        {
            _timeout = timeout;
            _pollingInterval = pollingInterval;
            _ignoredExceptionTypes = ignoredExceptionTypes;
        }

        public FluentWait WithTimeout(TimeSpan timeout)
        {
            this._timeout = timeout;
            return this;
        }

        public FluentWait PollingEvery(TimeSpan pollingInterval)
        {
            this._pollingInterval = pollingInterval;
            return this;
        }

        public FluentWait WithErrorMessage(string errorMessage)
        {
            this._externalErrorMessage = errorMessage;
            return this;
        }

        public void Until(Func<bool> condition)
        {
            this._condition = condition;
            Start();
        }

        public FluentWait IgnoreException<TException>() where TException : Exception
        {
            _ignoreAllExceptions = true;
            return this;
        }

        public FluentWait IgnoreAllExceptions()
        {
            _ignoredExceptionTypes.Add(typeof(Exception));
            return this;
        }

        public FluentWait DoNotThrowTimeoutException()
        {
            _throwTimeoutException = false;
            return this;
        }

        private void Start()
        {
            DateTime endTime = DateTime.UtcNow.Add(_timeout);
            string? lastCatchedErrorMessage = null;

            while (DateTime.UtcNow < endTime)
            {
                try
                {
                    if (_condition!())
                        return;
                }
                catch (Exception ex)
                {
                    lastCatchedErrorMessage = ex.Message;

                    if (!_ignoreAllExceptions)
                    {
                        if (_ignoredExceptionTypes.Contains(ex.GetType()))
                            continue;
                        else
                            throw;
                    }
                }

                Thread.Sleep(_pollingInterval);
            }

            if (_throwTimeoutException)
            {
                string errorMessageStartsWith = $"Condition not met within the specified timeout '{_timeout}'. \nError message: ";
                if (string.IsNullOrEmpty(lastCatchedErrorMessage))
                    throw new TimeoutException($"{errorMessageStartsWith}{_externalErrorMessage}");
                else
                    throw new TimeoutException($"{errorMessageStartsWith}\n\t1. {_externalErrorMessage} \n\t2. {lastCatchedErrorMessage}");
            }
        }
    }
}
