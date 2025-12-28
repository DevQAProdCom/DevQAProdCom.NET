using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class TestPageTab2Actions : SingleUiPageActions<TestPageTab2>
    {
        public TestPageTab2Actions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(uiInteractor, tabName)
        {
        }
    }
}
