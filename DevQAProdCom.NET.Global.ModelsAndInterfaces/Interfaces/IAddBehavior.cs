namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces
{
    public interface IAddBehavior
    {
        public T AddBehavior<T>(params KeyValuePair<string, object>[]? auxiliaryParams) where T : IBehavior;
    }
}
