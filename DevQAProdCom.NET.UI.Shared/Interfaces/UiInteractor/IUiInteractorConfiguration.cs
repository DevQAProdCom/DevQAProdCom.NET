using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractorConfiguration : IHaveHeterogeneousKeyValueData
    {
        public DateTime? Created { get; set; }
        public TimeSpan? TimeToLive { get; set; }
        public DateTime? ExpirationTime { get; }
        public string? DownloadsDefaultDirectory { get; set; }
    }
}
