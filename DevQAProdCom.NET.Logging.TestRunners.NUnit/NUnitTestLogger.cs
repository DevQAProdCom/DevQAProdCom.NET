using System.Reflection;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.Models;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Test;
using DevQAProdCom.NET.Logging.TestRunners.Shared.OperativeClasses;
using DevQAProdCom.NET.TestRunners.NUnit.Models.Enumerations;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.Logging.TestRunners.NUnit
{
    public class NUnitTestLogger : BaseTestLogger
    {
        private readonly NUnitTestLoggerConfigParameters _configParameters;
        private readonly object _getLoggingProvidersLock = new object();

        public NUnitTestLogger(ILoggingProviderFactoriesSet loggingProviderFactoriesSet, NUnitTestLoggerConfigParameters configParameters) : base(loggingProviderFactoriesSet, configParameters)
        {
            AddEnricher(EnrichStagesOfExecutionFlowTypeWithMetadata);
            _configParameters = configParameters;
        }

        #region Logging Provider Info

        public override List<ILoggingProvider> GetLoggingProviders(string? loggingProvidersSetIdentifier = null)
        {
            lock (_getLoggingProvidersLock)
            {
                List<ILoggingProvider> loggingProviders = new List<ILoggingProvider>();

                if (_configParameters.WriteMode.Any(x => x.IsEquivalentTo(TestRunnerLoggingWriteMode.SingleLogFilePerRun.ToString())) || !_configParameters.WriteMode.Any())
                    loggingProviders = base.GetLoggingProviders(TestRunId);

                return loggingProviders;
            }
        }

        #endregion Logging Provider Info

        #region Execution State Info

        public override string GetTestRunExecutionFlowStage(string flowIdentifier = "")
        {
            TestRunExecutionFlowStagesInfo.TryGetValue(CurrentThreadId, out IHasTestRunExecutionFlowStageInfo? loggingInfo);

            if (!string.IsNullOrEmpty(loggingInfo?.TestRunExecutionFlowStage))
                return loggingInfo.TestRunExecutionFlowStage;

            Test test = TestContext.CurrentContext.Test.GetFieldValue<Test>("_test");

            if (test.TestType == TestType.SetUpFixture.ToString())
            {
                if (!IsAtLeastSingleTestExecuted())
                    return TestRunExecutionFlowStage.RunOneTimeSetUpExecution.ToString();
                return TestRunExecutionFlowStage.RunOneTimeTearDownExecution.ToString();
            }
            else if (test.TestType == TestType.TestFixture.ToString() && test.MethodName == null)
            {
                if (!IsAtLeastSingleTestExecuted())
                    return TestRunExecutionFlowStage.SuiteOneTimeSetUpExecution.ToString();
                return TestRunExecutionFlowStage.SuiteOneTimeTearDownExecution.ToString();
            }
            else if (test.TestType == TestType.TestMethod.ToString())
                return TestRunExecutionFlowStage.TestExecution.ToString();

            return TestRunExecutionFlowStage.UnIdentifiedTestRunExecutionFlowStage.ToString();
        }

        #endregion Execution State Info

        #region Execution Time/FeedThrough Logging

        protected override ILogMessage EnrichWithRunOneTimeSetUpExecutionMetadata(ILogMessage message)
        {
            TestRunHookExecutionLoggingModel loggingInfo = GetTestRunHookExecutionLoggingModel(TestRunExecutionFlowStage.RunOneTimeSetUpExecution);

            return GetTestRunHookExecutionLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        protected override ILogMessage EnrichWithSuiteOneTimeSetUpExecutionMetadata(ILogMessage message)
        {
            TestSuiteHookExecutionLoggingModel loggingInfo = GetTestSuiteHookExecutionLoggingModel(TestRunExecutionFlowStage.SuiteOneTimeSetUpExecution);

            return GetTestSuiteHookExecutionLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        protected override ILogMessage EnrichWithTestSetUpExecutionMetadata(ILogMessage message)
        {
            TestHookExecutionLoggingModel loggingInfo = GetTestHookExecutionLoggingModel(TestRunExecutionFlowStage.TestSetUpExecution);

            return GetTestHookExecutionLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        protected override ILogMessage EnrichWithTestExecutionMetadata(ILogMessage message)
        {
            TestExecutionLoggingModel loggingInfo = GetTestExecutionLoggingModel();

            return GetTestExecutionLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        protected override ILogMessage EnrichWithTestTearDownExecutionMetadata(ILogMessage message)
        {
            TestHookExecutionLoggingModel loggingInfo = GetTestHookExecutionLoggingModel(TestRunExecutionFlowStage.TestTearDownExecution);

            return GetTestHookExecutionLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        protected override ILogMessage EnrichWithSuiteOneTimeTearDownExecutionMetadata(ILogMessage message)
        {
            TestSuiteHookExecutionLoggingModel loggingInfo = GetTestSuiteHookExecutionLoggingModel(TestRunExecutionFlowStage.SuiteOneTimeTearDownExecution);

            return GetTestSuiteHookExecutionLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        protected override ILogMessage EnrichWithRunOneTimeTearDownExecutionMetadata(ILogMessage message)
        {
            TestRunHookExecutionLoggingModel loggingInfo = GetTestRunHookExecutionLoggingModel(TestRunExecutionFlowStage.RunOneTimeTearDownExecution);

            return GetTestRunHookExecutionLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        private TestRunHookExecutionLoggingModel GetTestRunHookExecutionLoggingModel(TestRunExecutionFlowStage testRunExecutionFlowStage)
        {
            return new TestRunHookExecutionLoggingModel()
            {
                TestRunExecutionFlowStage = testRunExecutionFlowStage.ToString(),
                HookClassId = TestContext.CurrentContext.Test.ID,
                HookClassName = TestContext.CurrentContext.Test.ClassName,
                LogEntryOrderNumber = GetLogEntryOrderNumber(TestRunLoggingLevelIdentifier).ToString()
            };
        }

        private TestSuiteHookExecutionLoggingModel GetTestSuiteHookExecutionLoggingModel(TestRunExecutionFlowStage testRunExecutionFlowStage)
        {
            return new TestSuiteHookExecutionLoggingModel()
            {
                TestRunExecutionFlowStage = testRunExecutionFlowStage.ToString(),
                TestSuiteId = TestContext.CurrentContext.Test.ID,
                TestSuiteName = TestContext.CurrentContext.Test.ClassName,
                LogEntryOrderNumber = GetLogEntryOrderNumber(TestContext.CurrentContext.Test.ClassName).ToString()
            };
        }

        private TestHookExecutionLoggingModel GetTestHookExecutionLoggingModel(TestRunExecutionFlowStage testRunExecutionFlowStage)
        {
            return new TestHookExecutionLoggingModel()
            {
                TestRunExecutionFlowStage = testRunExecutionFlowStage.ToString(),
                TestSuiteName = TestContext.CurrentContext.Test.ClassName,
                TestId = TestContext.CurrentContext.Test.ID,
                TestMethodName = TestContext.CurrentContext.Test.MethodName,
                LogEntryOrderNumber = GetLogEntryOrderNumber(TestContext.CurrentContext.Test.ID).ToString()
            };
        }

        private TestExecutionLoggingModel GetTestExecutionLoggingModel()
        {
            return new TestExecutionLoggingModel()
            {
                TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecution.ToString(),
                TestSuiteName = TestContext.CurrentContext.Test.ClassName,
                TestId = TestContext.CurrentContext.Test.ID,
                TestMethodName = TestContext.CurrentContext.Test.MethodName,
                LogEntryOrderNumber = GetLogEntryOrderNumber(TestContext.CurrentContext.Test.ID).ToString()
            };
        }

        #endregion Execution Time/FeedThrough Logging

        #region  Specific Time/Momentum Logging

        protected override ILogMessage EnrichWithRunStartMetadata(ILogMessage message)
        {
            TestRunBaseLoggingModel loggingInfo = GetTestRunSpecificTimeStateLoggingModel(TestRunExecutionFlowStage.RunStart);

            return GetTestRunSpecificTimeStateLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        protected override ILogMessage EnrichWithSuiteStartMetadata(ILogMessage message)
        {
            TestSuiteLoggingModel loggingInfo = GetTestSuiteSpecificTimeStateLoggingModel(TestRunExecutionFlowStage.SuiteStart);

            return GetTestSuiteSpecificTimeStateLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }
        protected override ILogMessage EnrichWithSuiteEndMetadata(ILogMessage message)
        {
            TestSuiteLoggingModel loggingInfo = GetTestSuiteSpecificTimeStateLoggingModel(TestRunExecutionFlowStage.SuiteEnd);

            return GetTestSuiteSpecificTimeStateLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        protected override ILogMessage EnrichWithRunEndMetadata(ILogMessage message)
        {
            TestRunBaseLoggingModel loggingInfo = GetTestRunSpecificTimeStateLoggingModel(TestRunExecutionFlowStage.RunEnd);

            return GetTestRunSpecificTimeStateLogMessage(loggingInfo)
                .AppendSpace()
                .Append(message);
        }

        private TestRunBaseLoggingModel GetTestRunSpecificTimeStateLoggingModel(TestRunExecutionFlowStage testRunExecutionFlowStage)
        {
            return new TestRunBaseLoggingModel()
            {
                TestRunExecutionFlowStage = testRunExecutionFlowStage.ToString(),
                LogEntryOrderNumber = GetLogEntryOrderNumber(TestRunLoggingLevelIdentifier).ToString()
            };
        }

        private TestSuiteLoggingModel GetTestSuiteSpecificTimeStateLoggingModel(TestRunExecutionFlowStage testRunExecutionFlowStage)
        {
            return new TestSuiteLoggingModel()
            {
                TestRunExecutionFlowStage = testRunExecutionFlowStage.ToString(),
                LogEntryOrderNumber = GetLogEntryOrderNumber(TestContext.CurrentContext.Test.ClassName).ToString(),
                TestSuiteId = TestContext.CurrentContext.Test.ID,
                TestSuiteName = TestContext.CurrentContext.Test.ClassName
            };
        }

        public override void LogTestRunStart(TestRunStartLoggingModel loggingInfo)
        {
            loggingInfo ??= new();

            PopulateBaseRunInfo(loggingInfo);
            loggingInfo.StartTime ??= DateTime.UtcNow;
            loggingInfo.Description ??= TestRunDescription;
            loggingInfo.TestRunExecutionFlowStage ??= TestRunExecutionFlowStage.RunStart.ToString();
            loggingInfo.LogEntryOrderNumber ??= GetLogEntryOrderNumber(TestRunLoggingLevelIdentifier).ToString();

            base.LogTestRunStart(loggingInfo);
        }

        public override void LogTestSuiteStart(TestSuiteStartLoggingModel loggingInfo)
        {
            loggingInfo ??= new();
            PopulateBaseRunInfo(loggingInfo);
            loggingInfo.TestRunExecutionFlowStage ??= TestRunExecutionFlowStage.SuiteStart.ToString();
            loggingInfo.StartTime ??= DateTime.UtcNow;
            loggingInfo.LogEntryOrderNumber ??= GetLogEntryOrderNumber(loggingInfo.TestSuiteName).ToString();

            base.LogTestSuiteStart(loggingInfo);
        }

        public override void LogTestStart(TestStartLoggingModel? loggingInfo = null)
        {
            loggingInfo ??= new();
            PopulateBaseRunInfo(loggingInfo);
            loggingInfo.TestRunExecutionFlowStage ??= TestRunExecutionFlowStage.TestStart.ToString();
            loggingInfo.LogEntryOrderNumber ??= GetLogEntryOrderNumber(TestContext.CurrentContext.Test.ID).ToString();
            loggingInfo.TestSuiteName ??= TestContext.CurrentContext.Test.ClassName;
            loggingInfo.TestId ??= TestContext.CurrentContext.Test.ID;
            loggingInfo.TestMethodName ??= TestContext.CurrentContext.Test.MethodName;
            loggingInfo.StartTime ??= DateTime.UtcNow;
            loggingInfo.TestArguments ??= GetTestArguments();

            base.LogTestStart(loggingInfo);
        }

        public override void LogTestExecutionEnd(TestExecutionEndLoggingModel loggingInfo)
        {
            loggingInfo ??= new();
            PopulateBaseRunInfo(loggingInfo);
            loggingInfo.TestRunExecutionFlowStage ??= TestRunExecutionFlowStage.TestExecutionEnd.ToString();
            loggingInfo.LogEntryOrderNumber ??= GetLogEntryOrderNumber(loggingInfo.TestId).ToString();
            loggingInfo.EndTime ??= DateTime.UtcNow;

            base.LogTestExecutionEnd(loggingInfo);
        }

        public override void LogTestSuiteEnd(TestSuiteEndLoggingModel loggingInfo)
        {
            loggingInfo ??= new();
            PopulateBaseRunInfo(loggingInfo);
            loggingInfo.TestRunExecutionFlowStage ??= TestRunExecutionFlowStage.SuiteEnd.ToString();
            loggingInfo.EndTime ??= DateTime.UtcNow;
            loggingInfo.LogEntryOrderNumber ??= GetLogEntryOrderNumber(loggingInfo.TestSuiteName).ToString();

            base.LogTestSuiteEnd(loggingInfo);
        }

        public override void LogTestRunEnd(TestRunEndLoggingModel loggingInfo)
        {
            loggingInfo ??= new();

            PopulateBaseRunInfo(loggingInfo);
            loggingInfo.EndTime ??= DateTime.UtcNow;
            loggingInfo.TestRunExecutionFlowStage ??= TestRunExecutionFlowStage.RunEnd.ToString();
            loggingInfo.LogEntryOrderNumber ??= GetLogEntryOrderNumber(TestRunLoggingLevelIdentifier).ToString();

            base.LogTestRunEnd(loggingInfo);
        }

        #endregion  Specific Time/Momentum Logging

        #region Auxiliary

        private bool IsAtLeastSingleTestExecuted()
        {
            return TestContext.CurrentContext.Result.FailCount > 0 ||
                   TestContext.CurrentContext.Result.PassCount > 0 ||
                   TestContext.CurrentContext.Result.SkipCount > 0 ||
                   TestContext.CurrentContext.Result.InconclusiveCount > 0;
        }

        protected override Dictionary<string, object> GetTestArguments()
        {
            Dictionary<string, object> arguments = new();

            MethodInfo method = TestContext.CurrentContext.Test.Type.GetMethods()
                .Single(x => x.Name == TestContext.CurrentContext.Test.MethodName);

            ParameterInfo[] parameters = method.GetParameters();
            object[] testArguments = TestContext.CurrentContext.Test.Arguments;

            for (int i = 0; i < testArguments.Length; i++)
                arguments.Add(parameters[i].Name, testArguments[i]);

            return arguments;
        }

        #endregion
    }
}
