using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractorMakeScreenshots
    {
        public List<IUiInteractorTabScreenshotModel> MakeScreenshots(string? directoryPath = null, string? fileNamePrefix = null);
    }
}
