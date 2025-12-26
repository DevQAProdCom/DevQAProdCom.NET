using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager
{
    public interface IUiInteractorsManagersProvider : IHaveIdentifiers
    {
        #region UiInteractorsManagers

        public IUiInteractorsManager GetUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null);
        public void DisposeUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null);
        public void DisposeAllUiInteractorsManagers();

        #endregion UiInteractorsManagers

        #region UiInteractors

        public IUiInteractor GetUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeAllUiInteractors(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null);

        #endregion UiInteractors
    }
}
