using System.Text;
using System.Text.RegularExpressions;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.Logging.Shared.OperativeClasses
{
    public class LogMessage : ILogMessage
    {
        private readonly StringBuilder _message;
        public string Message => _message.ToString().Trim();
        public object[] Values => GetOrderedParametersValuesAccordingToPlaceholdersPositionWithinMessage(Message, ParametersValues);
        public Dictionary<string, object> ParametersValues { get; } = new();

        public LogMessage()
        {
            _message = new StringBuilder();
        }

        public LogMessage(string message)
        {
            _message = new StringBuilder(message);
        }

        public LogMessage(string message, params object[] values)
        {
            _message = new StringBuilder(message);
            ParametersValues = MatchMessagePlaceholdersWithValues(message, values);
        }

        public LogMessage(string message, params KeyValuePair<string, object>[]? parametersValues)
        {
            _message = new StringBuilder(message);

            if (parametersValues != null)
                ParametersValues = parametersValues.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public ILogMessage AppendParameter(string parameterName, object? value = null)
        {
            _message.Append($"{parameterName}");

            if (value != null)
                ParametersValues.Upsert(parameterName, value);

            return this;
        }

        public ILogMessage Append(string logRecordPart, params object[]? values)
        {
            if (!string.IsNullOrEmpty(logRecordPart))
            {
                _message.Append(logRecordPart);

                if (values?.Length > 0)
                {
                    Dictionary<string, object> parametersValues = MatchMessagePlaceholdersWithValues(logRecordPart, values);

                    foreach (var parameterValue in parametersValues)
                        ParametersValues.Upsert(parameterValue.Key, parameterValue.Value);
                }
            }

            return this;
        }

        public ILogMessage Append(string logRecordPart, params KeyValuePair<string, object>[]? parametersValues)
        {
            if (!string.IsNullOrEmpty(logRecordPart))
            {
                _message.Append(logRecordPart);

                if (parametersValues?.Length > 0)
                    foreach (var parameterValue in parametersValues)
                        ParametersValues.Upsert(parameterValue.Key, parameterValue.Value);
            }

            return this;
        }

        public ILogMessage Append(ILogMessage logRecordPart)
        {
            Append(logRecordPart.Message, logRecordPart.Values);
            return this;
        }

        public ILogMessage Append(string logRecordPart)
        {
            Append(new LogMessage(logRecordPart));
            return this;
        }

        public ILogMessage AppendSpace()
        {
            _message.Append(" ");
            return this;
        }

        public ILogMessage AppendNewLine()
        {
            _message.Append("\n");
            return this;
        }

        private Dictionary<string, object> MatchMessagePlaceholdersWithValues(string message, params object[] values)
        {
            Dictionary<string, object> matchedDict = new();

            if (!string.IsNullOrEmpty(message))
            {
                if (values.Length > 0)
                {
                    var parametersPlaceholders = GetMessageParametersPlaceholders(message);

                    if (parametersPlaceholders.Count == values.Length)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            KeyValuePair<string, object> parameterValue = new(parametersPlaceholders[i], values[i]);
                            if (!matchedDict.ContainsKey(parameterValue.Key))
                                matchedDict.Add(parameterValue.Key, parameterValue.Value);
                        }
                    }
                }
            }

            return matchedDict;
        }

        private object[] GetOrderedParametersValuesAccordingToPlaceholdersPositionWithinMessage(string message,
            Dictionary<string, object>? parametersValues)
        {
            List<object> orderedValues = new();

            if (!string.IsNullOrEmpty(message) && parametersValues != null)
            {
                var placeholders = GetMessageParametersPlaceholders(message);

                foreach (var placeholder in placeholders)
                {
                    if (parametersValues.TryGetValue(placeholder, out object? value))
                        orderedValues.Add(value);
                    else
                        orderedValues.Add($"Value for log parameter '{placeholder}' was not provided.");
                }
            }

            return orderedValues.ToArray();
        }

        private List<string> GetMessageParametersPlaceholders(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Regex regex = new Regex(@"\{[^\}]+\}");
                MatchCollection parametersPlaceholders = regex.Matches(message);

                return parametersPlaceholders.Select(x => x.Value).ToList();
            }

            return new List<string>();
        }
    }
}
