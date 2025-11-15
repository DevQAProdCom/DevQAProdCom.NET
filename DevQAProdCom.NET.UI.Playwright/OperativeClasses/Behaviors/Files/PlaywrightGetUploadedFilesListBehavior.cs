using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Files;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Files
{
    public class PlaywrightGetUploadedFilesListBehavior : PlaywrightUiElementBehavior, IGetUploadedFilesListBehavior
    {
        public PlaywrightGetUploadedFilesListBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public List<string> GetUploadedFilesList()
        {
            return UiElement.ExecuteJavaScript<string[]>(new FileInfo(SharedUiConstants.Files.GetElementFilesListJavaScriptFilePath)).ToList();
        }
    }
}
