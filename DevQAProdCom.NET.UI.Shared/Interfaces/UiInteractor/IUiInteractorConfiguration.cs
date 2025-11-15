using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractorConfiguration: IHaveHeterogeneousKeyValueData
    {
        public string? DownloadsDefaultDirectory { get; set; }
    }
}
