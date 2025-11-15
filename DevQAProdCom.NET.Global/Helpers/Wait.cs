namespace DevQAProdCom.NET.Global.Helpers
{
    public static class Wait
    {
        public static FluentWait Create() => new FluentWait();
        public static FluentWait Create(TimeSpan timeout, TimeSpan pollingInterval) => new FluentWait(timeout, pollingInterval);
        public static FluentWait Create(TimeSpan timeout, TimeSpan pollingInterval, List<Type>? ignoredExceptionTypes) => new FluentWait(timeout, pollingInterval, ignoredExceptionTypes);

        public static FluentWait WithTimeout(TimeSpan timeout)
        {
            return new FluentWait().WithTimeout(timeout);
        }
    }
}
