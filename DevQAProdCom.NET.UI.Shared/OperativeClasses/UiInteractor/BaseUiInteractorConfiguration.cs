using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public class BaseUiInteractorConfiguration : IUiInteractorConfiguration
    {
        public string? DownloadsDefaultDirectory { get; set; }
        public DateTime? Created { get; set; }
        public TimeSpan? TimeToLive { get; set; } = TimeSpan.FromHours(2);
        public DateTime? ExpirationTime => Created.HasValue && TimeToLive.HasValue ? Created.Value.Add(TimeToLive.Value) : null;
        public Dictionary<string, object>? Data { get; set; }
    }
}
