using DevQAProdCom.NET.Global.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors
{
    public class UiPageBehaviorParameters : BehaviorParameters
    {
        public UiPageBehaviorParameters(IUiPage uiPage, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[] auxiliaryParams) : base(auxiliaryParams)
        {
            ParamsDictionary.Add(SharedUiConstants.IUiPage, uiPage);
            ParamsDictionary.Add(SharedUiConstants.IExecuteJavaScript, javaScriptExecutor);
        }
    }
}
