namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces
{
    public interface IBehaviorProvider
    {
        Func<IBehaviorParameters, T> GetBehaviorApplierService<T>();
    }
}
