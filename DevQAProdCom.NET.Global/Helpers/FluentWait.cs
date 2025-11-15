namespace DevQAProdCom.NET.Global.Helpers
{
    public class FluentWait
    {
        private TimeSpan _timeout = TimeSpan.FromSeconds(30);
        private TimeSpan _pollingInterval = TimeSpan.FromSeconds(1);
        private Func<bool>? _condition;
        private List<Type>? _ignoredExceptionTypes;
        private string? _externalErrorMessage = "Timeout Exception";

        public FluentWait()
        {
            _ignoredExceptionTypes = new();
        }

        public FluentWait(TimeSpan timeout) : this()
        {
            _timeout = timeout;
        }

        public FluentWait(TimeSpan timeout, TimeSpan pollingInterval) : this()
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

        public FluentWait Ignore<TException>() where TException : Exception
        {
            _ignoredExceptionTypes ??= new();
            _ignoredExceptionTypes.Add(typeof(TException));

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
                catch (Exception ex) when (_ignoredExceptionTypes!.Contains((ex.GetType())))
                {
                    // Ignore the exception from the list of exceptions that should be not taken into account
                    lastCatchedErrorMessage = ex.Message;
                }

                Thread.Sleep(_pollingInterval);
            }

            throw new TimeoutException($"Condition not met within the specified timeout '{_timeout}'. \nError message: \n\t1. {_externalErrorMessage} \n\t2. {lastCatchedErrorMessage}");
        }
    }
}
