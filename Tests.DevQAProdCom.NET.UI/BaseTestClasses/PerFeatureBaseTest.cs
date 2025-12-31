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
            UiInteractor = UiInteractorsManagersProvider.GetUiInteractor(uiInteractorsManagerName: TestContext.CurrentContext.Test.ClassName);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            UiInteractorsManagersProvider.DisposeUiInteractorsManagers(uiInteractorsManagerName: TestContext.CurrentContext.Test.ClassName);
        }
    }
}





//using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
//using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;

//namespace Tests.DevQAProdCom.NET.UI.BaseTestClasses
//{
//    internal class PerFeatureBaseTest : BaseTest
//    {
//        protected IUiInteractorsManagersProvider UiInteractorsManagersProvider;
//        protected IUiInteractor UiInteractor; // keep in mind that in parallel test executions OneTimeSetup and OneTimeTearDown may be executed in different threads
//        protected int ThreadId; // save thread to dispose interactor after feature execution ends keeping in mind that in parallel test executions OneTimeSetup and OneTimeTearDown may be executed in different threads

//        [OneTimeSetUp]
//        public void OneTimeSetup()
//        {
//            UiInteractorsManagersProvider = _di.GetRequiredService<IUiInteractorsManagersProvider>();
//            UiInteractorsManagersProvider.GetUiInteractor(uiInteractorsManagerName: TestContext.CurrentContext.Test.ClassName);
//        }

//        [OneTimeTearDown]
//        public void OneTimeTearDown()
//        {
//            UiInteractorsManagersProvider.DisposeUiInteractor(uiInteractorsManagerName: TestContext.CurrentContext.Test.ClassName);
//        }
//    }
//}
