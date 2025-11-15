using NUnit.Framework;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Hooks;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Models;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests
{
    internal class PreconditionsTestsSuite1 : PerScenarioBaseTest
    {
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(1, new Parameters() { DoubleParameterName = 0.001, StringParameterName = "str1" });
            }
        }

        [Test]
        public void Suite1_Test_Info()
        {
           _log.Info($"{SharedTestConstants.TestExecution}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.f2a06559);
            MockProcessing();
        }

        [TestCase(1, "str1", 0.001)]
        public void Suite1_TestCase_Debug(int intParameterName, string stringParameterName, double doubleParameterName)
        {
           _log.Debug($"{SharedTestConstants.TestExecution}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.aa4524b4);
            MockProcessing();
        }

        [TestCaseSource(nameof(TestCases))]
        public void Suite1_TestCaseSource_Verbose(int intParameterName, Parameters parameters)
        {
           _log.Verbose($"{SharedTestConstants.TestExecution}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a3a9c958);
            MockProcessing();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
           _log.Info($"{SharedTestConstants.OneTimeSetUp}/{TestSuiteName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a3c0d3be);
            MockProcessing();
        }

        [SetUp]
        public void SetUp()
        {
           _log.Info($"{SharedTestConstants.SetUp}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.b1f3c1f5);
            MockProcessing();
        }

        [TearDown]
        public void TearDown()
        {
           _log.Info($"{SharedTestConstants.TearDown}/{TestSuiteName}/{TestMethodName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.afb88fb7);
            MockProcessing();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
           _log.Info($"{SharedTestConstants.OneTimeTearDown}/{TestSuiteName} [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a12a608c);
            MockProcessing();
        }
    }

}
