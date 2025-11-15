namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElementBehaviorFactory
    {
        T Create<T>(IUiElement uiElement, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[]? auxiliaryParams);
    }
}
