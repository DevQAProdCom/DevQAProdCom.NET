using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class MouseActionsTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/TestPage_MouseActions";

        [Find(Use.IdEquals, "element-for-mouse-move-mouse-enter")]
        public ILabel ElementForMouseMoveMouseEnter;
    }
}
