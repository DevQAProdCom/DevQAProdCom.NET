namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Exceptions
{
    public class NotSetException : Exception
    {
        public NotSetException()
        {
        }

        public NotSetException(string message) : base(message)
        {
        }

        public NotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
