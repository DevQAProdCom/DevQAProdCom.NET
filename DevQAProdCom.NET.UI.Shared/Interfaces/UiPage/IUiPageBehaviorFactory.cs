namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface IUiPageBehaviorFactory
    {
        public T Create<T>(IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[]? auxiliaryParams);
    }
}
