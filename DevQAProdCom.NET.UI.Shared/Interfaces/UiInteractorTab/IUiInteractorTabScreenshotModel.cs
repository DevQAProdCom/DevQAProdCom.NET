namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab
{
    public interface IUiInteractorTabScreenshotModel
    {
        string TabName { get; set; }
        string FilePath { get; set; }
        byte[] ScreenshotByteArray { get; set; }
    }
}
