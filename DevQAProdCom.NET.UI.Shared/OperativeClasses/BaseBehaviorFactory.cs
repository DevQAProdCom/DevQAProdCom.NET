using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses
{
    public class BaseBehaviorFactory
    {
        protected readonly IBehaviorProvider BehaviorProvider;

        public BaseBehaviorFactory(IBehaviorProvider behaviorProvider)
        {
            BehaviorProvider = behaviorProvider;
        }
    }
}
