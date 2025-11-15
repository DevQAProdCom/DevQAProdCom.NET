namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces
{
    public interface IGetSecret
    {
        Task<T> GetSecret<T>(string secretName) where T : class;
    }
}
