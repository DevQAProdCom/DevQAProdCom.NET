using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PagesActions
{
    public class TestPageTab3Actions : SingleUiPageActions<TestPageTab3>
    {
        public TestPageTab3Actions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(uiInteractor, tabName)
        {
        }
    }
}
