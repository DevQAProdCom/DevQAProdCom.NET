using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class HtmlElementsTypesAndActionsTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/HtmlElementsTypesAndActionsTestPage";

        [Find(Use.IdEquals, "uploadFileInputFieldOfFileType")]
        public IInputFile UploadFileInputFieldOfFileType;

        [Find(Use.IdEquals, "inputFieldForDownloadedFileName")]
        public IInputText InputFieldForDownloadedFileName;

        [Find(Use.IdEquals, "downloadFileButton")]
        public IButton DownloadFileButton;
    }
}
