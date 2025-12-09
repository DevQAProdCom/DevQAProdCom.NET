using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor.Behaviors
{
    public class UiInteractorBehavior(IBehaviorParameters parameters) : BaseBehavior(parameters)
    {
        private IUiInteractor? _uiInteractor;
        protected IUiInteractor UiInteractor => _uiInteractor ??= Parameters.Get<IUiInteractor>(SharedUiConstants.IUiInteractor);
    }
}
