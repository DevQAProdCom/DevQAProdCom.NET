using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Files
{
    public class SeleniumUiElementBehaviorGetUploadedFilesList : SeleniumUiElementBehavior, IUiElementBehaviorGetUploadedFilesList
    {
        public SeleniumUiElementBehaviorGetUploadedFilesList(IBehaviorParameters parameters) : base(parameters) { }

        public List<string> GetUploadedFilesList()
        {
            return UiElement.ExecuteJavaScript<string[]>(new FileInfo(SharedUiConstants.Files.GetElementFilesListJavaScriptFilePath)).ToList();
        }
    }
}
