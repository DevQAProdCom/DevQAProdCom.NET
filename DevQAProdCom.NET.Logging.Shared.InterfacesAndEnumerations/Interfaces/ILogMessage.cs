namespace DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces
{
    public interface ILogMessage
    {
        public string Message { get; }
        public object[]? Values { get; }
        public Dictionary<string, object>? ParametersValues { get; }

        public ILogMessage AppendParameter(string parameterName, object? value = null);
        public ILogMessage Append(string logRecordPart);
        public ILogMessage Append(string logRecordPart, params object[]? values);
        public ILogMessage Append(string logRecordPart, params KeyValuePair<string, object>[]? parametersValues);
        public ILogMessage Append(ILogMessage logRecordPart);
        public ILogMessage AppendSpace();
        public ILogMessage AppendNewLine();
    }
}
