using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Files
{
    public class PlaywrightUiElementBehaviorDownloadFile : PlaywrightUiElementBehavior, IUiElementBehaviorDownloadFile
    {
        public PlaywrightUiElementBehaviorDownloadFile(IBehaviorParameters parameters) : base(parameters) { }

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

