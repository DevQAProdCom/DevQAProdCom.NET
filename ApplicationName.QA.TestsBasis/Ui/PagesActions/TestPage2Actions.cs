using ApplicationName.QA.TestsBasis.Services;
using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PagesActions
{
    public class TestPage2Actions : SingleUiPageActions<TestPage2>
    {
        public ServiceForDependencyInjectionIntoActions ServiceForDependencyInjectionIntoActions;

        public TestPage2Actions(IUiInteractorsManagersProvider uiInteractorsManagersProvider, ServiceForDependencyInjectionIntoActions serviceForDependencyInjectionIntoActions) : base(uiInteractorsManagersProvider)
        {
            ServiceForDependencyInjectionIntoActions = serviceForDependencyInjectionIntoActions;
        }

        public TestPage2Actions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(uiInteractor, tabName)
        {
        }
    }
}
