using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class TestPageTab2Service : SingleUiPageService<TestPageTab2>
    {
        public TestPageTab2Service(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultTab) : base(uiInteractor, tabName)
        {
        }
    }
}
