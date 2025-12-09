using DevQAProdCom.NET.Global.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor.Behaviors
{
    public class UiInteractorBehaviorParameters : BehaviorParameters
    {
        public UiInteractorBehaviorParameters(IUiInteractor uiInteractor, params KeyValuePair<string, object>[] auxiliaryParams) : base(auxiliaryParams)
        {
            ParamsDictionary.Add(SharedUiConstants.IUiInteractor, uiInteractor);
        }
    }
}
