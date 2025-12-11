using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;

namespace Tests.DevQAProdCom.NET.UI.BaseTestClasses
{
    internal class PerFeatureBaseTest : BaseTest
    {
        protected IUiInteractorsManagersProvider UiInteractorsManagersProvider;
        protected IUiInteractor UiInteractor; // keep in mind that in parallel test executions OneTimeSetup and OneTimeTearDown may be executed in different threads

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            UiInteractorsManagersProvider = _di.GetRequiredService<IUiInteractorsManagersProvider>();
            UiInteractor = UiInteractorsManagersProvider.GetUiInteractor();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            UiInteractorsManagersProvider.DisposeUiInteractor(uiInteractorIdentifier: UiInteractor.Name);
        }
    }
}
