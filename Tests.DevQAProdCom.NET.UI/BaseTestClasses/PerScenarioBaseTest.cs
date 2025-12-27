using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.BaseTestClasses
{
    public class PerScenarioBaseTest : BaseTest
    {
        protected IUiInteractorsManagersProvider UiInteractorsManagersProvider;
        protected IUiInteractor UiInteractor => UiInteractorsManagersProvider.GetUiInteractor(uiInteractorsManagerScope: UiInteractorsManagerScope.Test);

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            UiInteractorsManagersProvider = _di.GetRequiredService<IUiInteractorsManagersProvider>();
        }

        [TearDown]
        public void TearDown()
        {
            UiInteractor.MakeScreenshots(directoryPath: Const.Screenshot_Directory);
            UiInteractorsManagersProvider.DisposeUiInteractorsManager(uiInteractorsManagerScope: UiInteractorsManagerScope.Test);
        }
    }
}

