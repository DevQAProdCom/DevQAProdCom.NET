using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class TestPageTab1Service : SingleUiPageService<TestPageTab1>
    {
        public TestPageTab1Service(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultTab) : base(uiInteractor, tabName)
        {
        }
    }
}
