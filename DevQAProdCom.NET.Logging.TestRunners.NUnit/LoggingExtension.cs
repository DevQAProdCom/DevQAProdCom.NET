using DevQAProdCom.NET.Logging.TestRunners.NUnit.DependencyInjection;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.Mappers;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Test;
using DevQAProdCom.NET.TestRunners.NUnit.Models.Enumerations;
using DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Root;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using NUnit.Engine;
using NUnit.Engine.Extensibility;
using NUnit.Framework;

namespace DevQAProdCom.NET.Logging.TestRunners.NUnit
{
    [Extension]
    public class LoggingExtension : ITestEventListener
    {
        private ITestLogger? _testLogger;

        private ITestLogger TestLogger
        {
            get
            {
                if (_testLogger == null)
                    _testLogger = DiContainer.Instance.GetRequiredService<ITestLogger>();

                return _testLogger;
            }
        }

        private string _testRunStartReport = string.Empty;
        private bool _isTestRunStartInfoLogged = false;
        private bool _isTestRunSetUpFixtureFinished = false; //Is used to identify that OneTimeSetup methods of Run Level SetUpFixture inside Test Project has finished its execution and that configuration of DI services, including ITestLogger, done inside it finished.
        private static string CurrentThreadId => Thread.CurrentThread.ManagedThreadId.ToString();

        public void OnTestEvent(string report)
        {
            if (report.StartsWith("<start-run"))
                _testRunStartReport = report;

            else if (report.StartsWith("<start-suite"))
            {
                //Preserve order: first LogTestSuiteStart, then LogTestRunStart. Such workaround have to be done because all DI services, including ITestLogger, are configured in OneTimeSetup methods of Run Level SetUpFixture inside Test Project, but after "<start-run" event happens.
                //That is why, in case any issue happens during "<start-run" logging, there would be no way to log error, cause DI can't return TestLogger instance for doing that. So there is a need to first identify whether IsTestRunSetUpFixtureFinished and all DI services, including TestLogger, are configured and then only proceed with any further logging.
                //It is done inside LogTestSuiteStart while setting "IsTestRunSetUpFixtureFinished = true", cause, as soon as execution of first TestSuite started,
                //it means that OneTimeSetup methods of Run Level SetUpFixture have already been executed and that DI services have already been configured.
                LogTestSuiteStart(report);
                LogTestRunStart();
            }

            else if (report.StartsWith("<test-case"))
                LogTestExecutionEnd(report);

            else if (report.StartsWith("<test-suite"))
                LogTestSuiteEnd(report);

            else if (report.StartsWith("<test-run"))
                LogTestRunEnd(report);
        }

        private void LogTestRunStart()
        {
            if (!_isTestRunStartInfoLogged && _isTestRunSetUpFixtureFinished)
            {
                try
                {
                    TestLogger.StartTestRunExecutionFlowStage(new TestRunBaseLoggingModel() { TestRunExecutionFlowStage = TestRunExecutionFlowStage.RunStart.ToString() }, CurrentThreadId);
                    TestRunStart testRunStart = _testRunStartReport.FromXml<TestRunStart>();
                    TestRunStartLoggingModel loggingInfo = testRunStart.ToTestRunStartLoggingModel();
                    TestLogger.LogTestRunStart(loggingInfo);
                }
                catch (Exception ex)
                {
                    TestLogger.Error("Unable to log 'Test Run Start' info. Error '{Error}'. Stack Trace: '{StackTrace}'.", ex.Message, ex.StackTrace);
                }

                TestLogger.EndTestRunExecutionFlowStage(CurrentThreadId);
                _isTestRunStartInfoLogged = true;
            }
        }

        private void LogTestSuiteStart(string report)
        {
            try
            {
                if (report.Contains($"type=\"{TestType.TestFixture}\""))
                {
                    _isTestRunSetUpFixtureFinished = true;

                    TestLogger.StartTestRunExecutionFlowStage(new TestSuiteLoggingModel()
                    {
                        TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteStart.ToString(),
                        TestSuiteId = TestContext.CurrentContext.Test.ID,
                        TestSuiteName = TestContext.CurrentContext.Test.ClassName!,
                    }, CurrentThreadId);

                    TestSuiteStart testSuiteStart = report.FromXml<TestSuiteStart>();
                    TestSuiteStartLoggingModel loggingInfo = testSuiteStart.ToTestSuiteStartLoggingModel();
                    TestLogger.LogTestSuiteStart(loggingInfo);
                }
            }
            catch (Exception ex)
            {
                TestLogger.Error("Unable to log 'Test Suite Start' info. Error '{Error}'. Stack Trace: '{StackTrace}'.", ex.Message, ex.StackTrace!);
            }

            if (report.Contains($"type=\"{TestType.TestFixture}\"")) // it is needed to avoid TestLogger GetRequired service issue
                TestLogger.EndTestRunExecutionFlowStage(CurrentThreadId);
        }

        private void LogTestExecutionEnd(string report)
        {
            try
            {
                TestCaseEnd testCaseEnd = report.FromXml<TestCaseEnd>();

                TestLogger.StartTestRunExecutionFlowStage(new TestExecutionLoggingModel()
                {
                    TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecutionEnd.ToString(),
                    TestSuiteName = testCaseEnd.ClassName,
                    TestId = testCaseEnd.Id,
                    TestMethodName = testCaseEnd.MethodName,
                }, CurrentThreadId);

                TestExecutionEndLoggingModel loggingInfo = testCaseEnd.ToTestExecutionEndLoggingModel();
                TestLogger.LogTestExecutionEnd(loggingInfo);
            }
            catch (Exception ex)
            {
                TestLogger.Error("Unable to log 'Test Execution End' info. Error '{Error}'. Stack Trace: '{StackTrace}'.", ex.Message, ex.StackTrace!);
            }

            TestLogger.EndTestRunExecutionFlowStage(CurrentThreadId);
        }

        private void LogTestSuiteEnd(string report)
        {
            try
            {
                TestLogger.StartTestRunExecutionFlowStage(new TestSuiteLoggingModel()
                {
                    TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteEnd.ToString(),
                    TestSuiteId = TestContext.CurrentContext.Test.ID,
                    TestSuiteName = TestContext.CurrentContext.Test.ClassName!,

                }, CurrentThreadId);

                TestSuiteEnd testSuiteEnd = report.FromXml<TestSuiteEnd>();

                if (testSuiteEnd.Type == TestType.TestFixture.ToString())
                {
                    TestSuiteEndLoggingModel loggingInfo = testSuiteEnd.ToTestSuiteEndLoggingModel();
                    TestLogger.LogTestSuiteEnd(loggingInfo);
                }
            }
            catch (Exception ex)
            {
                TestLogger.Error("Unable to log 'Test Suite End' info. Error '{Error}'. Stack Trace: '{StackTrace}'.", ex.Message, ex.StackTrace!);
            }

            TestLogger.EndTestRunExecutionFlowStage(CurrentThreadId);
        }

        private void LogTestRunEnd(string report)
        {
            try
            {
                TestLogger.StartTestRunExecutionFlowStage(new TestRunBaseLoggingModel() { TestRunExecutionFlowStage = TestRunExecutionFlowStage.RunEnd.ToString() }, CurrentThreadId);
                TestRunEnd testRunEnd = report.FromXml<TestRunEnd>();
                TestRunEndLoggingModel loggingInfo = testRunEnd.ToTestRunEndLoggingModel();
                TestLogger.LogTestRunEnd(loggingInfo);
            }
            catch (Exception ex)
            {
                TestLogger.Error("Unable to log 'Test Run End' info. Error '{Error}'. Stack Trace: '{StackTrace}'.", ex.Message, ex.StackTrace!);
            }

            TestLogger.EndTestRunExecutionFlowStage(CurrentThreadId);

            //Manual disposal is required in order log data not to be lost, because some sinks of Logging Providers, like using Seq localhost or remote server,
            //may not retrieve data otherwise without manual invocation Close and Flush methods used inside Disposal.
            TestLogger.Dispose();
        }
    }
}
