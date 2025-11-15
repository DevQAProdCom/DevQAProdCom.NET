using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public class UiElementBehaviorParameters : PageBehaviorParameters
    {
        public UiElementBehaviorParameters(IUiElement uiElement, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[] auxiliaryParams) : base(javaScriptExecutor, auxiliaryParams)
        {
            ParamsDictionary.Add(SharedUiConstants.IUiElement, uiElement);
        }
    }
}
