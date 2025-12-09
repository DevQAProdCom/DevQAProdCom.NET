using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.BaseTestClasses
{
    public class PerScenarioBaseTest : BaseTest
    {
        protected IUiInteractorsManagersProvider UiInteractorsManagerAsyncLocalInstance;
        protected IUiInteractor UiInteractor => UiInteractorsManagerAsyncLocalInstance.GetUiInteractor();

        [SetUp]
        public void Setup()
        {
            UiInteractorsManagerAsyncLocalInstance = _di.GetRequiredService<IUiInteractorsManagersProvider>();
        }

        [TearDown]
        public void TearDown()
        {
            UiInteractor.MakeScreenshots(directoryPath: Const.Screenshot_Directory);
            //UiInteractorsManager.DisposeUiInteractorsManager();
            UiInteractorsManagerAsyncLocalInstance.DisposeUiInteractor(UiInteractor.Name);
        }
    }
}

