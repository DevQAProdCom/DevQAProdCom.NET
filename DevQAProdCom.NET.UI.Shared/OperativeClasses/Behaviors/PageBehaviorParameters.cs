using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public class PageBehaviorParameters : BehaviorParameters
    {
        public PageBehaviorParameters(IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[] auxiliaryParams)
        {
            ParamsDictionary.Add(SharedUiConstants.IExecuteJavaScript, javaScriptExecutor);

            foreach (var auxiliaryParam in auxiliaryParams)
                ParamsDictionary.Add(auxiliaryParam.Key, auxiliaryParam.Value);
        }
    }
}
