using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class UiElementSearchRelativeToUiElementsListOfShadowRootHostComplexUiElementsAsClassTestPageService : SingleUiPageService<UiElementSearchRelativeToUiElementsListOfShadowRootHostComplexUiElementsAsClassTestPage>
    {
        public UiElementSearchRelativeToUiElementsListOfShadowRootHostComplexUiElementsAsClassTestPageService(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(uiInteractor, tabName)
        {

        }
    }
}
