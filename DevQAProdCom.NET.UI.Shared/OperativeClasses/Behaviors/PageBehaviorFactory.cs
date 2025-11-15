using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public class PageBehaviorFactory : IUiPageBehaviorFactory
    {
        private readonly IBehaviorProvider _behaviorProvider;

        public PageBehaviorFactory(IBehaviorProvider behaviorProvider)
        {
            _behaviorProvider = behaviorProvider;
        }

        public T Create<T>(IExecuteJavaScript javaScriptExecutor, params KeyValuePair<string, object>[]? auxiliaryParams)
        {
            Func<IBehaviorParameters, T> func = _behaviorProvider.GetBehaviorApplierService<T>();
            IBehaviorParameters parameters = new PageBehaviorParameters(javaScriptExecutor, auxiliaryParams);

            return func(parameters);
        }
    }
}
