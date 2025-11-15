using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;

namespace Tests.DevQAProdCom.NET.UI.BaseTestClasses
{
    internal class PerFeatureBaseTest : BaseTest
    {
        protected IUiInteractorsManager UiInteractorsManager;
        protected IUiInteractor UiInteractor; // keep in mind that in parallel test executions OneTimeSetup and OneTimeTearDown may be executed in different threads

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            UiInteractorsManager = _di.GetRequiredService<IUiInteractorsManager>();
            UiInteractor = UiInteractorsManager.GetUiInteractor();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            UiInteractorsManager.DisposeUiInteractor(UiInteractor.Name);
        }
    }
}
