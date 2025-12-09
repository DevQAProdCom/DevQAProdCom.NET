using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager
{
    public interface IUiInteractorsManager : IHaveIdentifiers//: IDisposable
    {
        public IUiInteractor GetUiInteractor(string uiInteractorIdentifier = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeUiInteractor(string uiInteractorIdentifier = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeAllUiInteractors();
    }
}
