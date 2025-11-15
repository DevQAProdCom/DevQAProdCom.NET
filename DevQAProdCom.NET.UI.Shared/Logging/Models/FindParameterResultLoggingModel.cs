using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Logging.Models
{
    public class FindParameterResultLoggingModel
    {
        public uint NestingLevel { get; set; }
        public FindState FindState { get; set; }
        public string Name { get; set; }
        public int? UiIndex { get; set; }
        public bool IsElementOfList { get; set; }
        public UiElementsFindInfoLoggingModel FindInfo { get; set; }
        public int? TotalAmountOfElementsFound { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? StackTrace { get; set; }
    }
}
