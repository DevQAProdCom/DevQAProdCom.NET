using DevQAProdCom.NET.Global.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorTab.Behaviors
{
    public class UiInteractorTabBehaviorParameters : BehaviorParameters
    {
        public UiInteractorTabBehaviorParameters(IUiInteractorTab uiInteractorTab, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[] auxiliaryParams) : base(auxiliaryParams)
        {
            ParamsDictionary.Add(SharedUiConstants.IUiInteractorTab, uiInteractorTab);
            ParamsDictionary.Add(SharedUiConstants.IExecuteJavaScript, javaScriptExecutor);
        }
    }
}
