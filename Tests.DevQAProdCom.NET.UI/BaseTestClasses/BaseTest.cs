using DevQAProdCom.NET.DependencyInjection.Shared.Interfaces;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;
using Tests.DevQAProdCom.NET.UI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.UI.BaseTestClasses
{
    public abstract class BaseTest
    {
        protected IDiContainer _di = DiContainer.Instance;
        protected ITestLogger _log => DiContainer.Instance.Log;

        [SetUp]
        public void SetUp()
        {
            _log.LogTestStart();
        }
    }
}
