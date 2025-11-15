using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors
{
    public class PlaywrightUiElementDownloadBehavior : PlaywrightUiElementBehavior, IUiElementDownloadBehavior
    {
        public PlaywrightUiElementDownloadBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public void DownloadFile()
        {
            var download = Page.RunAndWaitForDownloadAsync(async () =>
            {
                await Locator.ClickAsync();
            }).Result;

            var downloadDefaultDirectory = UiElement.UiPage.UiTab.UiInteractor.DownloadsDefaultDirectory;

            if (string.IsNullOrEmpty(downloadDefaultDirectory))
                throw new NotImplementedException("Cannot save downloaded file. DownloadsDefaultDirectory is not configured for the UiInteractor. Use UiInteractorFactory for configurations.");

            var originalName = download.SuggestedFilename;
            var savePath = Path.Combine(downloadDefaultDirectory, originalName);
            download.SaveAsAsync(savePath).Wait();
        }
    }
}

