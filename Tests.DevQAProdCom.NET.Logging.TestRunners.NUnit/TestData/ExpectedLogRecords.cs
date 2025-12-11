using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using NUnit.Framework.Interfaces;
using Serilog.Events;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Models;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Models;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.TestData
{
    public class ExpectedLogRecords
    {
        public static List<LogModel> PreconditionsTestsSuite1
        {
            get
            {
                return new List<LogModel>()
                {
                    #region [SuiteStart]

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] Start Time: '{StartTime}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteStart.ToString(),
                    },

                    #endregion [SuiteStart]

                    #region [OneTimeSetUp]

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a496a7f4}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestSuiteId}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] OneTimeSetUp/PerScenarioBaseTest/PreconditionsTestsSuite1 [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteOneTimeSetUpExecution.ToString(),
                    },
                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a3c0d3be}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestSuiteId}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] OneTimeSetUp/PreconditionsTestsSuite1 [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteOneTimeSetUpExecution.ToString(),
                    },

                    #endregion [OneTimeSetUp]

                    #region Suite1_Test_Info

                    #region [TestStart] Suite1_Test_Info

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Start Time: '{StartTime}'. TestArguments: 'No Arguments {@TestArguments}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestInfo}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestStart.ToString(),
                        LogEntryOrderNumber = "0"
                    },

                    #endregion [TestStart] Suite1_Test_Info

                    #region [SetUp] Suite1_Test_Info

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a3eea371}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] SetUp/PerScenarioBaseTest/PreconditionsTestsSuite1/Suite1_Test_Info [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestInfo}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "1"
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.b1f3c1f5}",
                        MessageTemplate =
                            "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] SetUp/PreconditionsTestsSuite1/Suite1_Test_Info [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestInfo}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "2"
                    },

                    #endregion [SetUp] Suite1_Test_Info

                    #region [TestExecution] Suite1_Test_Info

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.f2a06559}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TestExecution/PreconditionsTestsSuite1/Suite1_Test_Info [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestInfo}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "3"
                    },

                    #endregion [Execution] Suite1_Test_Info

                    #region [TearDown] Suite1_Test_Info

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.afb88fb7}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TearDown/PreconditionsTestsSuite1/Suite1_Test_Info [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestInfo}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "4"
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a0f4db90}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TearDown/PerScenarioBaseTest/PreconditionsTestsSuite1/Suite1_Test_Info [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestInfo}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "5"
                    },

                    #endregion TearDown Suite1_Test_Info

                    #region [TestExecutionEnd] Suite1_Test_Info

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Result: '{Result}'. End Time: '{EndTime}'. Duration: '{Duration}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestInfo}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecutionEnd.ToString(),
                        Result = TestStatus.Passed.ToString(),
                        LogEntryOrderNumber = "6"
                    },

                    #endregion [TestExecutionEnd] Suite1_Test_Info

                    #endregion Suite1_Test_Info

                    #region Suite1_TestCase_Debug

                    #region [TestStart] Suite1_TestCase_Debug

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Start Time: '{StartTime}'. TestArguments: '{@TestArguments}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseDebug}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestStart.ToString(),
                        LogEntryOrderNumber = "0",
                        TestArguments = new Dictionary<string, object>()
                        {
                            { SharedTestConstants.IntParameterName, 1 },
                            { SharedTestConstants.StringParameterName, "str1" },
                            { SharedTestConstants.DoubleParameterName, 0.001 }
                        }
                    },

                    #endregion [TestStart] Suite1_TestCase_Debug

                    #region [SetUp] Suite1_TestCase_Debug

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a3eea371}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] SetUp/PerScenarioBaseTest/PreconditionsTestsSuite1/Suite1_TestCase_Debug [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseDebug}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "1"
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.b1f3c1f5}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] SetUp/PreconditionsTestsSuite1/Suite1_TestCase_Debug [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseDebug}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "2"
                    },

                    #endregion [SetUp] Suite1_TestCase_Debug

                    #region [TestExecution] Suite1_TestCase_Debug

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.aa4524b4}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TestExecution/PreconditionsTestsSuite1/Suite1_TestCase_Debug [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseDebug}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "3",
                        Level = LogEventLevel.Debug.ToString()
                    },

                    #endregion [Execution] Suite1_TestCase_Debug

                    #region [TearDown] Suite1_TestCase_Debug

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.afb88fb7}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TearDown/PreconditionsTestsSuite1/Suite1_TestCase_Debug [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseDebug}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "4"
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a0f4db90}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TearDown/PerScenarioBaseTest/PreconditionsTestsSuite1/Suite1_TestCase_Debug [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseDebug}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "5"
                    },

                    #endregion Suite1_TestCase_Debug

                    #region [TestExecutionEnd] Suite1_TestCase_Debug

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Result: '{Result}'. End Time: '{EndTime}'. Duration: '{Duration}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseDebug}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecutionEnd.ToString(),
                        Result = TestStatus.Passed.ToString(),
                        LogEntryOrderNumber = "6"
                    },

                    #endregion [TestExecutionEnd] Suite1_TestCase_Debug

                    #endregion Suite1_TestCase_Debug

                    #region Suite1_TestCaseSource_Verbose

                    #region [TestStart] Suite1_TestCaseSource_Verbose

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Start Time: '{StartTime}'. TestArguments: '{@TestArguments}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseSourceVerbose}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestStart.ToString(),
                        LogEntryOrderNumber = "0",
                        TestArguments = new Dictionary<string, object>()
                        {
                            { SharedTestConstants.IntParameterName, 1 },
                            {
                                SharedTestConstants.Parameters,
                                new Parameters() { DoubleParameterName = 0.001, StringParameterName = "str1" }
                            },
                        }
                    },

                    #endregion [TestStart] Suite1_TestCaseSource_Verbose

                    #region [SetUp] Suite1_TestCaseSource_Verbose

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a3eea371}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] SetUp/PerScenarioBaseTest/PreconditionsTestsSuite1/Suite1_TestCaseSource_Verbose [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseSourceVerbose}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "1"
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.b1f3c1f5}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] SetUp/PreconditionsTestsSuite1/Suite1_TestCaseSource_Verbose [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseSourceVerbose}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "2"
                    },

                    #endregion [SetUp] Suite1_TestCaseSource_Verbose

                    #region [TestExecution] Suite1_TestCaseSource_Verbose

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a3a9c958}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TestExecution/PreconditionsTestsSuite1/Suite1_TestCaseSource_Verbose [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseSourceVerbose}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "3",
                        Level = LogEventLevel.Verbose.ToString()
                    },

                    #endregion [Execution] Suite1_TestCaseSource_Verbose

                    #region [TearDown] Suite1_TestCaseSource_Verbose

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.afb88fb7}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TearDown/PreconditionsTestsSuite1/Suite1_TestCaseSource_Verbose [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseSourceVerbose}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "4"
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a0f4db90}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TearDown/PerScenarioBaseTest/PreconditionsTestsSuite1/Suite1_TestCaseSource_Verbose [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseSourceVerbose}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "5"
                    },

                    #endregion Suite1_TestCaseSource_Verbose

                    #region [TestExecutionEnd] Suite1_TestCaseSource_Verbose

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Result: '{Result}'. End Time: '{EndTime}'. Duration: '{Duration}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite1TestCaseSourceVerbose}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecutionEnd.ToString(),
                        Result = TestStatus.Passed.ToString(),
                        LogEntryOrderNumber = "6"
                    },

                    #endregion [TestExecutionEnd] Suite1_TestCaseSource_Verbose

                    #endregion Suite1_TestCaseSource_Verbose

                    #region [OneTimeTearDown]

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a12a608c}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestSuiteId}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] OneTimeTearDown/PreconditionsTestsSuite1 [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteOneTimeTearDownExecution.ToString(),
                        LogEntryOrderNumber = "3"
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a2320c90}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestSuiteId}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] OneTimeTearDown/PerScenarioBaseTest/PreconditionsTestsSuite1 [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteOneTimeTearDownExecution.ToString(),
                        LogEntryOrderNumber = "4"
                    },

                    #endregion [OneTimeTearDown]

                    #region [SuiteEnd]

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] End Time: '{EndTime}'. Duration: '{Duration}'. Total: '{Total}'. Passed: '{Passed}'. Failed: '{Failed}'. Skipped: '{Skipped}'. Other Results: '{@OtherResults}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite1FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteEnd.ToString(),
                        Total = "3",
                        Passed = "3",
                        Failed = "0",
                        Skipped = "0",
                        OtherResults = new Dictionary<string, string>()
                        {
                            { "Inconclusive", "0" },
                            { "Warnings", "0" }
                        },
                        LogEntryOrderNumber = "5"
                    },

                    #endregion [SuiteEnd]
                };
            }
        }

        public static List<LogModel> PreconditionsTestsSuite2
        {
            get
            {
                return new List<LogModel>()
                {
                    #region [SuiteStart]

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] Start Time: '{StartTime}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteStart.ToString(),
                        LogEntryOrderNumber = "0"
                    },

                    #endregion [SuiteStart]

                    #region [OneTimeSetUp]

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a496a7f4}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestSuiteId}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] OneTimeSetUp/PerScenarioBaseTest/PreconditionsTestsSuite2 [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteOneTimeSetUpExecution.ToString(),
                    },
                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a00a2624}",
                        MessageTemplate =
                            "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestSuiteId}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] OneTimeSetUp/PreconditionsTestsSuite2 [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteOneTimeSetUpExecution.ToString(),
                    },

                    #endregion [OneTimeSetUp]

                    #region Suite2_Test_Error_Warning_Fatal_AssertFail

                    #region [TestStart] Suite2_Test_Error_Warning_Fatal_AssertFail

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Start Time: '{StartTime}'. TestArguments: 'No Arguments {@TestArguments}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestErrorWarningFatalAssertFail}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestStart.ToString(),
                        LogEntryOrderNumber = "0"
                    },

                    #endregion [TestStart] Suite2_Test_Error_Warning_Fatal_AssertFail

                    #region [SetUp] Suite2_Test_Error_Warning_Fatal_AssertFail

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a3eea371}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] SetUp/PerScenarioBaseTest/PreconditionsTestsSuite2/Suite2_Test_Error_Warning_Fatal_AssertFail [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestErrorWarningFatalAssertFail}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "1"
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.aacfe03b}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] SetUp/PreconditionsTestsSuite2/Suite2_Test_Error_Warning_Fatal_AssertFail [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestErrorWarningFatalAssertFail}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "2"
                    },

                    #endregion [SetUp] Suite2_Test_Error_Warning_Fatal_AssertFail

                    #region [TestExecution] Suite2_Test_Error_Warning_Fatal_AssertFail

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a34e3e88}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TestExecution/PreconditionsTestsSuite2/Suite2_Test_Error_Warning_Fatal_AssertFail [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestErrorWarningFatalAssertFail}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "3",
                        Level = LogEventLevel.Warning.ToString()
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a4359598}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TestExecution/PreconditionsTestsSuite2/Suite2_Test_Error_Warning_Fatal_AssertFail [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestErrorWarningFatalAssertFail}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "4",
                        Level = LogEventLevel.Error.ToString()
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.cb31b274}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TestExecution/PreconditionsTestsSuite2/Suite2_Test_Error_Warning_Fatal_AssertFail [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestErrorWarningFatalAssertFail}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "5",
                        Level = LogEventLevel.Fatal.ToString()
                    },

                    #endregion [Execution] Suite2_Test_Error_Warning_Fatal_AssertFail

                    #region [TearDown] Suite2_Test_Error_Warning_Fatal_AssertFail

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a36b092c}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TearDown/PreconditionsTestsSuite2/Suite2_Test_Error_Warning_Fatal_AssertFail [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestErrorWarningFatalAssertFail}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "6"
                    },

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a0f4db90}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] TearDown/PerScenarioBaseTest/PreconditionsTestsSuite2/Suite2_Test_Error_Warning_Fatal_AssertFail [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestErrorWarningFatalAssertFail}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                        LogEntryOrderNumber = "7"
                    },

                    #endregion Suite2_Test_Error_Warning_Fatal_AssertFail

                    #region [TestExecutionEnd] Suite2_Test_Error_Warning_Fatal_AssertFail

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Result: '{Result}'. End Time: '{EndTime}'. Duration: '{Duration}'. Failure Messages: '{@FailureMessages}'. Stack Trace: '{@StackTrace}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestErrorWarningFatalAssertFail}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecutionEnd.ToString(),
                        Result = TestStatus.Failed.ToString(),
                        LogEntryOrderNumber = "8",
                        FailureMessages = new List<string>() { "Warning, Error, Fatal" },
                        StackTrace = $"{SharedTestConstants.ProjectNameWithPreconditionTests}.PreconditionsTestsSuite2.Suite2_Test_Error_Warning_Fatal_AssertFail"
                    },

                    #endregion [TestExecutionEnd] Suite2_Test_Error_Warning_Fatal_AssertFail

                    #endregion Suite2_Test_Error_Warning_Fatal_AssertFail

                    #region Suite2_Test_Skipped

                    #region [TestExecutionEnd] Suite2_Test_Skipped

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Result: '{Result}'. End Time: '{EndTime}'. Duration: '{Duration}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite2TestSkipped}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecutionEnd.ToString(),
                        Result = TestStatus.Skipped.ToString(),
                        LogEntryOrderNumber = "0",
                    },

                    #endregion [TestExecutionEnd] Suite2_Test_Skipped

                    #endregion Suite2_Test_Skipped

                    #region [OneTimeTearDown]

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.f008a311}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestSuiteId}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] OneTimeTearDown/PreconditionsTestsSuite2 [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteOneTimeTearDownExecution.ToString(),
                        LogEntryOrderNumber = "3"
                    },
                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a2320c90}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestSuiteId}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] OneTimeTearDown/PerScenarioBaseTest/PreconditionsTestsSuite2 [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteOneTimeTearDownExecution.ToString(),
                        LogEntryOrderNumber = "4"
                    },

                    #endregion [OneTimeTearDown]

                    #region [SuiteEnd]

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] End Time: '{EndTime}'. Duration: '{Duration}'. Total: '{Total}'. Passed: '{Passed}'. Failed: '{Failed}'. Skipped: '{Skipped}'. Other Results: '{@OtherResults}'. Failure Messages: '{@FailureMessages}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite2FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteEnd.ToString(),
                        Total = "2",
                        Passed = "0",
                        Failed = "1",
                        Skipped = "1",
                        OtherResults = new Dictionary<string, string>()
                        {
                            { "Inconclusive", "0" },
                            { "Warnings", "0" }
                        },
                        FailureMessages = new List<string>() { "One or more child tests had errors" },
                        LogEntryOrderNumber = "5"
                    },

                    #endregion [SuiteEnd]
                };
            }
        }

        public static List<LogModel> PreconditionsTestsSuite3
        {
            get
            {
                return new List<LogModel>()
                {
                    #region [SuiteStart]

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] Start Time: '{StartTime}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite3FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteStart.ToString(),
                        LogEntryOrderNumber = "0"
                    },

                    #endregion [SuiteStart]

                    #region Suite3_Test_Skipped

                    #region [TestExecutionEnd] Suite3_Test_Skipped

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][{TestId}][{TestMethodName}][#{LogEntryOrderNumber}] Result: '{Result}'. End Time: '{EndTime}'. Duration: '{Duration}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite3FullNameWithPreconditionTests}",
                        TestMethodName = $"{SharedTestConstants.Suite3TestSkipped}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecutionEnd.ToString(),
                        Result = TestStatus.Skipped.ToString(),
                        LogEntryOrderNumber = "0",
                    },

                    #endregion [TestExecutionEnd] Suite3_Test_Skipped

                    #endregion Suite3_Test_Skipped

                    #region [SuiteEnd]

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestSuiteName}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] End Time: '{EndTime}'. Duration: '{Duration}'. Total: '{Total}'. Passed: '{Passed}'. Failed: '{Failed}'. Skipped: '{Skipped}'. Other Results: '{@OtherResults}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestSuiteName = $"{SharedTestConstants.Suite3FullNameWithPreconditionTests}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteEnd.ToString(),
                        Total = "1",
                        Passed = "0",
                        Failed = "0",
                        Skipped = "1",
                        OtherResults = new Dictionary<string, string>()
                        {
                            { "Inconclusive", "0" },
                            { "Warnings", "0" }
                        },
                        LogEntryOrderNumber = "1"
                    },

                    #endregion [SuiteEnd]
                };
            }
        }

        public static List<LogModel> PreconditionsTestsRunStatesAndHooks
        {
            get
            {
                return new List<LogModel>()
                {
                    #region [RunStart]

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] TestRunDescription: '{TestRunDescription}'. Start Time: '{StartTime}'. Additional Info: '{@AdditionalInfo}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        TestRunDescription = $"{SharedTestConstants.ManuallySetTestRunDescription}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.RunStart.ToString(),
                    },

                    #endregion [RunStart]

                    #region [RunOneTimeSetUpExecution]

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.a18dee14}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestRunExecutionFlowStage}][{HookClassId}][{HookClassName}][#{LogEntryOrderNumber}] SetUpFixture/OneTimeSetUp/StartRun [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        HookClassName = $"{SharedTestConstants.RunHooks}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.RunOneTimeSetUpExecution.ToString(),
                    },

                    #endregion [RunOneTimeSetUpExecution]

                    #region [RunOneTimeTearDownExecution]

                    new LogModel()
                    {
                        LogRecordId = $"{SharedTestConstants.LogRecordId.ab95bec6}",
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestRunExecutionFlowStage}][{HookClassId}][{HookClassName}][#{LogEntryOrderNumber}] SetUpFixture/OneTimeTearDown/EndRun [{LogRecordId}]",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        HookClassName = $"{SharedTestConstants.RunHooks}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.RunOneTimeTearDownExecution.ToString(),
                    },

                    #endregion [RunOneTimeTearDownExecution]

                    #region [RunEnd]

                    new LogModel()
                    {
                        MessageTemplate = "[{TestRunId}][{TestRunName}][{VersionUnderTest}][{TestRunExecutionFlowStage}][#{LogEntryOrderNumber}] End Time: '{EndTime}'. Duration: '{Duration}'. Total: '{Total}'. Passed: '{Passed}'. Failed: '{Failed}'. Skipped: '{Skipped}'. Other Results: '{@OtherResults}'.",
                        TestRunId = $"{SharedTestConstants.ManuallySetTestRunId}",
                        TestRunName = $"{SharedTestConstants.ManuallySetTestRunName}",
                        VersionUnderTest = $"{SharedTestConstants.ManuallySetVersionUnderTest}",
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.RunEnd.ToString(),
                        Total = "6",
                        Passed = "3",
                        Failed = "1",
                        Skipped = "2",
                        OtherResults = new Dictionary<string, string>()
                        {
                            { "Inconclusive", "0" },
                            { "Warnings", "0" }
                        },
                    }

                    #endregion [RunEnd]
                };
            }
        }
    }
}
