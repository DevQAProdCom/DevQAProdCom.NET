using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Files;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace DevQAProdCom.NET.UI.UiElements.OperativeClasses
{
    public class InputFile : UiElement, IInputFile
    {
        public void UploadFiles(params string[] filePaths)
        {
            AddBehavior<IUploadFilesBehavior>().UploadFiles(filePaths);
        }

        public List<string> GetUploadedFilesList()
        {
            return AddBehavior<IGetUploadedFilesListBehavior>().GetUploadedFilesList();
        }
    }
}
