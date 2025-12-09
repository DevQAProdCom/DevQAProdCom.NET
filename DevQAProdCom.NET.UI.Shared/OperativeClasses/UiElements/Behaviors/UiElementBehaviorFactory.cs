using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors
{
    public class UiElementBehaviorFactory(IBehaviorProvider behaviorProvider) : BaseBehaviorFactory(behaviorProvider), IUiElementBehaviorFactory
    {
        public T Create<T>(IUiElement uiElement, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[]? auxiliaryParams)
        {
            var func = BehaviorProvider.GetBehaviorApplierService<T>();
            IBehaviorParameters parameters = new UiElementBehaviorParameters(uiElement, javaScriptExecutor, auxiliaryParams);

            return func(parameters);
        }
    }
}
