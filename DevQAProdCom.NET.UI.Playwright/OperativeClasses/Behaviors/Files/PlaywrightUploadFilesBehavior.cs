using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Files;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Files
{
    public class PlaywrightUploadFilesBehavior : PlaywrightUiElementBehavior, IUploadFilesBehavior
    {
        public PlaywrightUploadFilesBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public void UploadFiles(params string[] filePaths)
        {
            Locator.SetInputFilesAsync(filePaths).GetAwaiter().GetResult();
        }
    }
}
