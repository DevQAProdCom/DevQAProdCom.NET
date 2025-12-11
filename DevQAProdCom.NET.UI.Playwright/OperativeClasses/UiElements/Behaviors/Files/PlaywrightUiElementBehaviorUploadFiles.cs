using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Files
{
    public class PlaywrightUiElementBehaviorUploadFiles : PlaywrightUiElementBehavior, IUiElementBehaviorUploadFiles
    {
        public PlaywrightUiElementBehaviorUploadFiles(IBehaviorParameters parameters) : base(parameters) { }

        public void UploadFiles(params string[] filePaths)
        {
            Locator.SetInputFilesAsync(filePaths).GetAwaiter().GetResult();
        }
    }
}
