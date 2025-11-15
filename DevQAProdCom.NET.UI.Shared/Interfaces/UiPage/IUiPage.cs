using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface IUiPage : IUiPageInfo, IInstantiateUiElements, IExecuteJavaScript, IUiPageMouseBehavior, IHasNativeObjects
    {
        public IUiInteractorTab UiTab { get; }
        public T AddBehavior<T>(params KeyValuePair<string, object>[]? auxiliaryParams) where T : IBehavior;

        public Uri GetPageUrl(params KeyValuePair<string, string>[] placeholderValues);
        public void GoTo(params KeyValuePair<string, string>[] placeholderValues);
        public void WaitForLoad();
    }
}
