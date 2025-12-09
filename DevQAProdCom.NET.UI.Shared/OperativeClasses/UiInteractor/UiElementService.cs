using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public class UiElementService<T> where T : IUiElement
    {
        protected readonly internal IUiInteractorTab _uiTab;
        protected readonly internal IUiInteractor _uiInteractor;

        public UiElementService(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            tabName ??= SharedUiConstants.DefaultUiInteractorTab;
            _uiInteractor = uiInteractor;
            _uiTab = _uiInteractor.GetTab(tabName);
        }
    }
}
