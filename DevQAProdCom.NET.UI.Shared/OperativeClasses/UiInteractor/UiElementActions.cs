using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public abstract class UiElementActions<T> : BaseActions, IUiElementActions
        where T : IUiElement
    {
        public abstract T UiElement { get; }

        public UiElementActions(IUiInteractorsManagersProvider uiInteractorsManagersProvider) : base(uiInteractorsManagersProvider) { }
        public UiElementActions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(uiInteractor, tabName) { }
    }
}
