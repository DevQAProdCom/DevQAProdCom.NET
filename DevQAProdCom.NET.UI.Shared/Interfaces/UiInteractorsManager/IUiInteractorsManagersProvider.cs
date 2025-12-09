using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager
{
    public interface IUiInteractorsManagersProvider : IHaveIdentifiers
    {
        public IUiInteractor GetUiInteractor(string? uiInteractorsManagerIdentifier = null, string uiInteractorIdentifier = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeUiInteractor(string? uiInteractorsManagerIdentifier = null, string uiInteractorIdentifier = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeAllUiInteractors(string? uiInteractorsManagerIdentifier = null);
        public void DisposeUiInteractorsManager(string? uiInteractorsManagerIdentifier = null);
    }
}
