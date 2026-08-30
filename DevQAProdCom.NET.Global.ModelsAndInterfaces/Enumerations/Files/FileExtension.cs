using System.ComponentModel;

namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations.Files
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
        [Description(".txt")]
        Txt,
        [Description(".xls")]
        Xls,
        [Description(".xlsx")]
        Xlsx,
        [Description(".xml")]
        Xml,
    }
}
