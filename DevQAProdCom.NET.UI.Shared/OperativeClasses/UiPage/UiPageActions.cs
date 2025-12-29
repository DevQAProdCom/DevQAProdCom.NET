using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public abstract class UiPageActions : BaseActions, IUiPageActions
    {
        public UiPageActions(IUiInteractorsManagersProvider uiInteractorsManagersProvider) : base(uiInteractorsManagersProvider) { }
        public UiPageActions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(uiInteractor, tabName) { }
    }
}
