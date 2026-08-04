namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces
{
    public interface IValidate
    {
        public bool IsSuccessful => string.IsNullOrEmpty(Error);
        public string? Error { get; }
    }
}
