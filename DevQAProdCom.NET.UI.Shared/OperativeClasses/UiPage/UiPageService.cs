using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public abstract class UiPageService : IUiPageService
    {
        protected readonly internal IUiInteractorTab _uiTab;
        protected readonly internal IUiInteractor _uiInteractor;

        public UiPageService(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            tabName ??= SharedUiConstants.DefaultUiInteractorTab;
            _uiInteractor = uiInteractor;
            _uiTab = _uiInteractor.GetTab(tabName);
        }
    }
}
