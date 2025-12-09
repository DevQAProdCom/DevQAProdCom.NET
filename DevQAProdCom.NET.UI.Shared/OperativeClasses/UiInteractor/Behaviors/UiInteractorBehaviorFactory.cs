using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor.Behaviors
{
    public class UiInteractorBehaviorFactory : IUiInteractorBehaviorFactory
    {
        protected readonly IBehaviorProvider _behaviorProvider;

        public UiInteractorBehaviorFactory(IBehaviorProvider behaviorProvider)
        {
            _behaviorProvider = behaviorProvider;
        }

        public T Create<T>(IUiInteractor uiInteractor, params KeyValuePair<string, object>[]? auxiliaryParams)
        {
            var func = _behaviorProvider.GetBehaviorApplierService<T>();
            IBehaviorParameters parameters = new UiInteractorBehaviorParameters(uiInteractor, auxiliaryParams);

            return func(parameters);
        }
    }
}
