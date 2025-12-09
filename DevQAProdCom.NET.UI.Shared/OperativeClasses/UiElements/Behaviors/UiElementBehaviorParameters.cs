using DevQAProdCom.NET.Global.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors
{
    public class UiElementBehaviorParameters : BehaviorParameters
    {
        public UiElementBehaviorParameters(IUiElement uiElement, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[] auxiliaryParams)
        {
            ParamsDictionary.Add(SharedUiConstants.IUiElement, uiElement);
            ParamsDictionary.Add(SharedUiConstants.IExecuteJavaScript, javaScriptExecutor);
        }
    }
}
