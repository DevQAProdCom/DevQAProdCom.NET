namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractorBehaviorFactory
    {
        public T Create<T>(IUiInteractor uiInteractor, params KeyValuePair<string, object>[]? auxiliaryParams);
    }
}
