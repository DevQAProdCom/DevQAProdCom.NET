namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab
{
    public interface IUiInteractorTabMakeScreenshot
    {
        public IUiInteractorTabScreenshotModel MakeScreenshot(string? directoryPath = null, string? fileNamePrefix = null);
    }
}
