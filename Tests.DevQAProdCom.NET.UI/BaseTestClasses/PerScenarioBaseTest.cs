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

        [SetUp]
        public void Setup()
        {
            UiInteractorsManagersProvider = _di.GetRequiredService<IUiInteractorsManagersProvider>();
        }

        [TearDown]
        public void TearDown()
        {
            UiInteractor.MakeScreenshots(directoryPath: Const.Screenshot_Directory);
            //UiInteractorsManager.DisposeUiInteractorsManager();
            UiInteractorsManagersProvider.DisposeUiInteractor(uiInteractorsManagerScope: UiInteractorsManagerScope.Test);
        }
    }
}

