using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorTab
{
    public class UiInteractorTabScreenshotModel : IUiInteractorTabScreenshotModel
    {
        public string TabName { get; set; }
        public string FilePath { get; set; }
        public byte[] ScreenshotByteArray { get; set; }
    }
}
