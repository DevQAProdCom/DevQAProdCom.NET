using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors
{
    public interface IBehaviorProvider
    {
        Func<IBehaviorParameters, T> GetBehaviorApplierService<T>();
    }
}
