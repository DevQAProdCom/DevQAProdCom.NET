using System.ComponentModel;

namespace DevQAProdCom.NET.Global.Enumerations.Files
{
    public enum FileExtension
    {
        [Description(".csv")]
        Csv,
        [Description(".ini")]
        Ini,
        [Description(".json")]
        Json,
        [Description(".md")]
        Md,
        [Description(".sln")]
        Sln,
        [Description(".xls")]
        Xls,
        [Description(".xlsx")]
        Xlsx,
        [Description(".xml")]
        Xml,
    }
}
