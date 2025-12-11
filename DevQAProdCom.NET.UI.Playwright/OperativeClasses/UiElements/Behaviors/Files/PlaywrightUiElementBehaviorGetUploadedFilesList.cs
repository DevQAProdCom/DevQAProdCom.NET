using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Behaviors.Files
{
    public class PlaywrightUiElementBehaviorGetUploadedFilesList : PlaywrightUiElementBehavior, IUiElementBehaviorGetUploadedFilesList
    {
        public PlaywrightUiElementBehaviorGetUploadedFilesList(IBehaviorParameters parameters) : base(parameters) { }

        public List<string> GetUploadedFilesList()
        {
            return UiElement.ExecuteJavaScript<string[]>(new FileInfo(SharedUiConstants.Files.GetElementFilesListJavaScriptFilePath)).ToList();
        }
    }
}
