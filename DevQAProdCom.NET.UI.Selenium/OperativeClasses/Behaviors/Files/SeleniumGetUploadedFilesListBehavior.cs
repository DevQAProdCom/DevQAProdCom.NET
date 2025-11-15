using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Files;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Files
{
    public class SeleniumGetUploadedFilesListBehavior : SeleniumUiElementBehavior, IGetUploadedFilesListBehavior
    {
        public SeleniumGetUploadedFilesListBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public List<string> GetUploadedFilesList()
        {
            return UiElement.ExecuteJavaScript<string[]>(new FileInfo(SharedUiConstants.Files.GetElementFilesListJavaScriptFilePath)).ToList();
        }
    }
}
