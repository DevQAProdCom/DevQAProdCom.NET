using NUnit.Framework;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Hooks
{
    internal class PerScenarioBaseTest : BaseTest
    {
        protected string? TestMethodName => TestContext.CurrentContext.Test.MethodName;
        protected string? TestSuiteName => TestContext.CurrentContext.Test.ClassName?.Split(".").Last();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _log.Info($"{SharedTestConstants.OneTimeSetUp}/{SharedTestConstants.PerScenarioBaseTest}/{TestSuiteName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a496a7f4);
        }

        [SetUp]
        public void SetUp()
        {
            _log.Info($"{SharedTestConstants.SetUp}/{SharedTestConstants.PerScenarioBaseTest}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a3eea371);
        }

        [TearDown]
        public void TearDown()
        {
            _log.Info($"{SharedTestConstants.TearDown}/{SharedTestConstants.PerScenarioBaseTest}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a0f4db90);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _log.Info($"{SharedTestConstants.OneTimeTearDown}/{SharedTestConstants.PerScenarioBaseTest}/{TestSuiteName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a2320c90);
        }

        protected void MockProcessing()
        {
            long cycles = 5000000;
            for (double i = 0; i < cycles; i++) ;
        }
    }

}
