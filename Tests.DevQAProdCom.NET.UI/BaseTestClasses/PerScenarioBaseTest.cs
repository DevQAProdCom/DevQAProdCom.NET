using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.BaseTestClasses
{
    public class PerScenarioBaseTest : BaseTest
    {
        protected IUiInteractorsManagersProvider UiInteractorsManagersProvider;
        protected IUiInteractor UiInteractor => UiInteractorsManagersProvider.GetUiInteractorOfCurrentThread();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            UiInteractorsManagersProvider = _di.GetRequiredService<IUiInteractorsManagersProvider>();
        }

        [TearDown]
        public void TearDown()
        {
            UiInteractor.MakeScreenshots(directoryPath: Const.Screenshot_Directory);
            UiInteractorsManagersProvider.DisposeUiInteractorsManagerOfCurrentThread();
        }
    }
}

