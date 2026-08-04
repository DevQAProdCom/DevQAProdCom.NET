using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Models
{
    public class ValidationModel : IValidate
    {
        public string? Error { get; set; }
    }
}
