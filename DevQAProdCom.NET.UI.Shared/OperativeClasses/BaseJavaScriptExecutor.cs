using DevQAProdCom.NET.UI.Shared.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses
{
    public abstract class BaseJavaScriptExecutor : IExecuteJavaScript
    {
        public abstract void ExecuteJavaScript(string script, params KeyValuePair<string, object>[] arguments);
        public void ExecuteJavaScript(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            string script = File.ReadAllText(file.FullName);
            ExecuteJavaScript(script, arguments);
        }

        public abstract T ExecuteJavaScript<T>(string script, params KeyValuePair<string, object>[] arguments);
        public T ExecuteJavaScript<T>(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            string script = File.ReadAllText(file.FullName);
            return ExecuteJavaScript<T>(script, arguments);
        }
    }
}
