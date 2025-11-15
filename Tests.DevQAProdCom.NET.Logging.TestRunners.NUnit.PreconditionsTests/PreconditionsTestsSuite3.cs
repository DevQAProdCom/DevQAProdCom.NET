using NUnit.Framework;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Hooks;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests
{
    [Ignore("Ignored Suite")]
    internal class PreconditionsTestsSuite3 : PerScenarioBaseTest
    {
        [Test]
        public void Suite3_Test_Skipped()
        {
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _log.Info($"{SharedTestConstants.OneTimeSetUp}/{TestSuiteName}");
            MockProcessing();
        }

        [SetUp]
        public void SetUp()
        {
            _log.Info($"{SharedTestConstants.SetUp}/{TestSuiteName}/{TestContext.CurrentContext.Test.MethodName}");
            MockProcessing();
        }

        [TearDown]
        public void TearDown()
        {
            _log.Info($"{SharedTestConstants.TearDown}/{TestSuiteName}/{TestContext.CurrentContext.Test.MethodName}");
            MockProcessing();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _log.Info($"{SharedTestConstants.OneTimeTearDown}/{TestSuiteName}");
            MockProcessing();
        }
    }
}
