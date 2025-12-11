using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class UiElementSearchRelativeToBeginningOfPageTestPageService : SingleUiPageService<UiElementSearchRelativeToBeginningOfPageTestPage>
    {
        public UiElementSearchRelativeToBeginningOfPageTestPageService(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(uiInteractor, tabName)
        {

        }
    }
}
