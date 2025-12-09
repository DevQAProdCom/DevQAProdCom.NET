namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface IUiPageBehaviorFactory
    {
        public T Create<T>(IUiPage uiPage, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[]? auxiliaryParams);
    }
}
