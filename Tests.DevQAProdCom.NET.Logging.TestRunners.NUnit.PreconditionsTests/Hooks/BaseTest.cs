using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.DependencyInjection;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Hooks
{
    internal abstract class BaseTest
    {
        protected ITestLogger _log => DiContainer.Instance.Log;

        [SetUp]
        public void LogTestStart()
        {
            _log.LogTestStart();
        }
    }
}
