using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class UiElementSearchRelativeTo2ShadowRootHostsComplexUiElementsAsClassTestPageService : SingleUiPageService<UiElementSearchRelativeTo2ShadowRootHostsComplexUiElementsAsClassTestPage>
    {
        public UiElementSearchRelativeTo2ShadowRootHostsComplexUiElementsAsClassTestPageService(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultTab) : base(uiInteractor, tabName)
        {

        }
    }
}
