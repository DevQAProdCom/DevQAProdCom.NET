using NUnit.Framework;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Hooks;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests
{
    internal class PreconditionsTestsSuite2 : PerScenarioBaseTest
    {
        [Test]
        public void Suite2_Test_Error_Warning_Fatal_AssertFail()
        {
           _log.Warning($"{SharedTestConstants.TestExecution}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a34e3e88);
           _log.Error($"{SharedTestConstants.TestExecution}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a4359598);
           _log.Fatal($"{SharedTestConstants.TestExecution}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.cb31b274);

            Assert.Fail("Warning, Error, Fatal");
            MockProcessing();
        }

        [Test]
        [Ignore("Suite2_Test_Skipped")]
        public void Suite2_Test_Skipped()
        {
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
           _log.Info($"{SharedTestConstants.OneTimeSetUp}/{TestSuiteName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a00a2624);
            MockProcessing();
        }

        [SetUp]
        public void SetUp()
        {
           _log.Info($"{SharedTestConstants.SetUp}/{TestSuiteName}/{TestContext.CurrentContext.Test.MethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.aacfe03b);
            MockProcessing();
        }

        [TearDown]
        public void TearDown()
        {
           _log.Info($"{SharedTestConstants.TearDown}/{TestSuiteName}/{TestContext.CurrentContext.Test.MethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a36b092c);
            MockProcessing();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
           _log.Info($"{SharedTestConstants.OneTimeTearDown}/{TestSuiteName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.f008a311);
            MockProcessing();
        }
    }
}
