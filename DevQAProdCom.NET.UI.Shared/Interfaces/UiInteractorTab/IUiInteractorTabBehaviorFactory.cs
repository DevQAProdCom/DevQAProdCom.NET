using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab
{
    public interface IUiInteractorTabBehaviorFactory
    {
        public T Create<T>(IUiInteractorTab uiInteractorTab, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[]? auxiliaryParams);
    }
}
