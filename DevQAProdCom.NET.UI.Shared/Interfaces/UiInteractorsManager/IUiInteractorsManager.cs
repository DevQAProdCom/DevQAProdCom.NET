using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager
{
    public interface IUiInteractorsManager : IHaveIdentifiers//: IDisposable
    {
        public IUiInteractor GetUiInteractor(string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeUiInteractor(string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeAllUiInteractors();
    }
}
