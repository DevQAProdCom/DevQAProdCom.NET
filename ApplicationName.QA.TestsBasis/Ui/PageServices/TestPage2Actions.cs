using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class TestPage2Actions : SingleUiPageActions<TestPage2>
    {
        public TestPage2Actions(IUiInteractorsManagersProvider uiInteractorsManagersProvider) : base(uiInteractorsManagersProvider)
        {
        }

        public TestPage2Actions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(uiInteractor, tabName)
        {
        }
    }
}
