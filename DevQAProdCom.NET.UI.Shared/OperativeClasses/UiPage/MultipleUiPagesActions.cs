using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public class MultipleUiPagesActions : UiPageActions, IMultipleUiPagesActions
    {
        public MultipleUiPagesActions(IUiInteractorsManagersProvider uiInteractorsManagersProvider) : base(uiInteractorsManagersProvider) { }
        public MultipleUiPagesActions(IUiInteractor interactor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(interactor, tabName) { }
    }
}
