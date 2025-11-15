using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class TestPageTab2 : BaseAppUiPage
    {
        public override string RelativeUri => @"/SomePath/TestPageTab2";

        [Find(Use.IdEquals, "tab-2-button-1")]
        public IUiElement Tab2Button1;

        [Find(Use.IdEquals, "tab-2-button-check-cookie")]
        public IUiElement Tab2ButtonCheckCookie;
    }
}
