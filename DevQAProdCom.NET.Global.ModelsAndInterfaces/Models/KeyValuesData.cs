using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Models
{
    public class KeyValuesData : IKeyValuesData
    {
        public string? Key { get; set; }
        public List<string>? Values { get; set; }
    }
}
