using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace DevQAProdCom.NET.UI.UiElements.OperativeClasses
{
    public class InputFile : UiElement, IInputFile
    {
        public virtual void UploadFiles(params string[] filePaths)
        {
            AddBehavior<IUiElementBehaviorUploadFiles>().UploadFiles(filePaths);
        }

        public virtual List<string> GetUploadedFilesList()
        {
            return AddBehavior<IUiElementBehaviorGetUploadedFilesList>().GetUploadedFilesList();
        }
    }
}
