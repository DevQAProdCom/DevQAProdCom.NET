using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorTab.Behaviors
{
    public class UiInteractorTabBehaviorFactory(IBehaviorProvider behaviorProvider) : BaseBehaviorFactory(behaviorProvider), IUiInteractorTabBehaviorFactory
    {
        public T Create<T>(IUiInteractorTab uiInteractorTab, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[]? auxiliaryParams)
        {
            var func = BehaviorProvider.GetBehaviorApplierService<T>();
            IBehaviorParameters parameters = new UiInteractorTabBehaviorParameters(uiInteractorTab, javaScriptExecutor, auxiliaryParams);

            return func(parameters);
        }
    }
}
