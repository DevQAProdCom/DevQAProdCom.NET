using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class ScrollTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/ScrollTestPage";

        [Find(Use.IdEquals, "Button for Scroll to Element")]
        public IUiElement ButtonForScrollToElement;

        [Find(Use.IdEquals, "Element for Page Mouse Scroll")]
        public IUiElement ElementForPageMouseScroll;

        [Find(Use.IdEquals, "Element for Page Mouse Scroll Vertically")]
        public IUiElement ElementForPageMouseScrollVertically;

        [Find(Use.IdEquals, "Element for Page Mouse Scroll Horizontally")]
        public IUiElement ElementForPageMouseScrollHorizontally;
    }
}
