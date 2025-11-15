using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public class UiElementBehaviorFactory : IUiElementBehaviorFactory
    {
        private IBehaviorProvider _behaviorProvider;

        public UiElementBehaviorFactory(IBehaviorProvider behaviorProvider)
        {
            _behaviorProvider = behaviorProvider;
        }

        public T Create<T>(IUiElement uiElement, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[]? auxiliaryParams)
        {
            Func<IBehaviorParameters, T> func = _behaviorProvider.GetBehaviorApplierService<T>();
            IBehaviorParameters parameters = new UiElementBehaviorParameters(uiElement, javaScriptExecutor, auxiliaryParams);

            return func(parameters);
        }
    }
}
