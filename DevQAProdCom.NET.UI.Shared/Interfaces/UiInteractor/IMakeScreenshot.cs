namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IMakeScreenshot
    {
        public void MakeScreenshot(string? directoryPath = null, string? fileNamePrefix = null);
    }
}
