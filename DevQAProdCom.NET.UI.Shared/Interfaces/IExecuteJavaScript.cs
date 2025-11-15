namespace DevQAProdCom.NET.UI.Shared.Interfaces
{
    public interface IExecuteJavaScript
    {
        void ExecuteJavaScript(string script, params KeyValuePair<string, object>[] arguments);
        void ExecuteJavaScript(FileInfo file, params KeyValuePair<string, object>[] arguments);

        T ExecuteJavaScript<T>(string script, params KeyValuePair<string, object>[] arguments);
        T ExecuteJavaScript<T>(FileInfo file, params KeyValuePair<string, object>[] arguments);
    }
}
