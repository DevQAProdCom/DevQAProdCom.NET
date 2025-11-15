using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public class BaseUiInteractorConfiguration : IUiInteractorConfiguration
    {
        public string? DownloadsDefaultDirectory { get; set; }
        public Dictionary<string, object>? Data { get; set; }
    }
}
