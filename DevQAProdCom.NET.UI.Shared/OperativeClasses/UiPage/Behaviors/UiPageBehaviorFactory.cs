using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors
{
    public class UiPageBehaviorFactory(IBehaviorProvider behaviorProvider) : BaseBehaviorFactory(behaviorProvider), IUiPageBehaviorFactory
    {
        public T Create<T>(IUiPage page, IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[]? auxiliaryParams)
        {
            var func = BehaviorProvider.GetBehaviorApplierService<T>();
            IBehaviorParameters parameters = new UiPageBehaviorParameters(page, javaScriptExecutor, auxiliaryParams);

            return func(parameters);
        }
    }
}
