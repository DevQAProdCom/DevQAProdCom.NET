using System.Collections.Concurrent;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.Shared.OperativeClasses;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Extensions;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Test;

namespace DevQAProdCom.NET.Logging.TestRunners.Shared.OperativeClasses
{
    public abstract class BaseTestLogger : BaseLogger, ITestLogger
    {
        #region Auxiliary Properties

        public const string TestRunLoggingLevelIdentifier = "TestRun";
        protected ConcurrentDictionary<string, IHasTestRunExecutionFlowStageInfo> TestRunExecutionFlowStagesInfo { get; } = new(); //Each thread has its own test execution flow stage
        protected ConcurrentDictionary<string, int> LogEntryOrderNumber { get; } = new();
        private readonly object _getLogEntryOrderNumberLock = new object();
        public List<string> WriteMode { get; set; }

        #endregion Auxiliary Properties

        #region General Run Info

        public string TestRunId { get; set; } = DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds();
        public string? TestRunName { get; set; }
        public string? TestRunDescription { get; set; }
        public string? VersionUnderTest { get; set; }

        public void SetTestRunLoggingInfo(string? testRunId = null, string? testRunName = null, string? testRunDescription = null, string? versionUnderTest = null)
        {
            if (!string.IsNullOrEmpty(testRunId))
                TestRunId = testRunId;

            if (!string.IsNullOrEmpty(testRunName))
                TestRunName = testRunName;

            if (!string.IsNullOrEmpty(testRunDescription))
                TestRunDescription = testRunDescription;

            if (!string.IsNullOrEmpty(versionUnderTest))
                VersionUnderTest = versionUnderTest;
        }

        #endregion General Run Info

        public BaseTestLogger(ILoggingProviderFactoriesSet loggingProviderFactoriesSet, ITestLoggerConfigParameters configParameters) : base(loggingProviderFactoriesSet)
        {
            if (configParameters != null)
            {
                SetTestRunLoggingInfo(configParameters.TestRunId, configParameters.TestRunName, configParameters.TestRunDescription, configParameters.VersionUnderTest);
                WriteMode = configParameters.WriteMode;
            }
        }

        #region Logging Provider Info

        #endregion Logging Provider Info

        #region Execution State Info

        public abstract string GetTestRunExecutionFlowStage(string flowIdentifier = "");

        public void StartTestRunExecutionFlowStage(IHasTestRunExecutionFlowStageInfo loggingInfo, string flowIdentifier = "")
        {
            TestRunExecutionFlowStagesInfo.AddOrUpdate(flowIdentifier, loggingInfo, (key, oldValue) => loggingInfo);
        }

        public void EndTestRunExecutionFlowStage(string flowIdentifier = "")
        {
            if (TestRunExecutionFlowStagesInfo.ContainsKey(flowIdentifier))
                TestRunExecutionFlowStagesInfo.TryRemove(flowIdentifier, out var value);
        }

        #endregion Execution State Info

        #region Execution Time/FeedThrough Logging

        public ILogMessage EnrichStagesOfExecutionFlowTypeWithMetadata(ILogMessage message)
        {
            var stage = GetTestRunExecutionFlowStage();

            if (stage == TestRunExecutionFlowStage.RunStart.ToString())
                return EnrichWithRunStartMetadata(message);

            if (stage == TestRunExecutionFlowStage.RunOneTimeSetUpExecution.ToString())
                return EnrichWithRunOneTimeSetUpExecutionMetadata(message);

            if (stage == TestRunExecutionFlowStage.SuiteStart.ToString())
                return EnrichWithSuiteStartMetadata(message);

            if (stage == TestRunExecutionFlowStage.SuiteOneTimeSetUpExecution.ToString())
                return EnrichWithSuiteOneTimeSetUpExecutionMetadata(message);

            if (stage == TestRunExecutionFlowStage.TestSetUpExecution.ToString())
                return EnrichWithTestSetUpExecutionMetadata(message);

            if (stage == TestRunExecutionFlowStage.TestExecution.ToString())
                return EnrichWithTestExecutionMetadata(message);

            if (stage == TestRunExecutionFlowStage.TestTearDownExecution.ToString())
                return EnrichWithTestTearDownExecutionMetadata(message);

            if (stage == TestRunExecutionFlowStage.SuiteOneTimeTearDownExecution.ToString())
                return EnrichWithSuiteOneTimeTearDownExecutionMetadata(message);

            if (stage == TestRunExecutionFlowStage.SuiteEnd.ToString())
                return EnrichWithSuiteEndMetadata(message);

            if (stage == TestRunExecutionFlowStage.RunOneTimeTearDownExecution.ToString())
                return EnrichWithRunOneTimeTearDownExecutionMetadata(message);

            if (stage == TestRunExecutionFlowStage.RunEnd.ToString())
                return EnrichWithRunEndMetadata(message);

            return message;
        }

        protected abstract ILogMessage EnrichWithRunStartMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithRunOneTimeSetUpExecutionMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithSuiteStartMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithSuiteOneTimeSetUpExecutionMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithTestSetUpExecutionMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithTestExecutionMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithTestTearDownExecutionMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithSuiteOneTimeTearDownExecutionMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithSuiteEndMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithRunOneTimeTearDownExecutionMetadata(ILogMessage message);
        protected abstract ILogMessage EnrichWithRunEndMetadata(ILogMessage message);

        #endregion Execution Time/FeedThrough Logging

        #region  Specific Time/Momentum Logging

        public virtual void LogTestRunStart(TestRunStartLoggingModel loggingInfo)
        {
            ILogMessage logMessage = GetTestRunBaseLogMessage(loggingInfo)
                .WithTestRunExecutionFlowStage(loggingInfo.TestRunExecutionFlowStage)
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);

            if (!string.IsNullOrEmpty(loggingInfo.Message))
                logMessage.AppendSpace().Append(loggingInfo.Message, loggingInfo.MessageParametersValues?.ToArray());

            if (!string.IsNullOrEmpty(loggingInfo.Description))
                logMessage.AppendSpace().WithTestRunDescription(loggingInfo.Description);

            if (loggingInfo.StartTime.HasValue)
                logMessage.AppendSpace().WithStartTime(loggingInfo.StartTime.Value.ToIso8601FormatWithMicroseconds());

            if (loggingInfo.TotalAmountOfTests != null)
                logMessage.AppendSpace().WithTotalAmountOfTests(loggingInfo.TotalAmountOfTests.ToString());

            if (loggingInfo.TestsWithExecuteState != null)
                logMessage.AppendSpace().WithTestsWithExecuteState(loggingInfo.TestsWithExecuteState.ToString());

            if (loggingInfo.TestsWithSkipState != null)
                logMessage.AppendSpace().WithTestsWithSkipState(loggingInfo.TestsWithSkipState.ToString());

            if (loggingInfo.AdditionalInfo?.Count() > 0)
                logMessage.AppendSpace().WithAdditionalInfo(loggingInfo.AdditionalInfo);

            Info(GetLoggingProviders(), logMessage);
        }

        public virtual void LogTestSuiteStart(TestSuiteStartLoggingModel loggingInfo)
        {
            ILogMessage logMessage = GetTestSuiteLogMessage(loggingInfo)
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);

            if (loggingInfo.StartTime.HasValue)
                logMessage.AppendSpace().WithStartTime(loggingInfo.StartTime.Value.ToIso8601FormatWithMicroseconds());

            if (loggingInfo.TestSuiteExecutionOrder != null)
                logMessage.AppendSpace().WithTestSuiteExecutionOrder(loggingInfo.TestSuiteExecutionOrder?.ToString());

            Info(GetLoggingProviders(), logMessage);
        }

        public virtual void LogTestStart(TestStartLoggingModel? loggingInfo = null)
        {
            loggingInfo ??= new();
            ILogMessage logMessage = GetTestExecutionLogMessage(loggingInfo);

            if (!string.IsNullOrEmpty(loggingInfo.Message))
                logMessage.AppendSpace().Append(loggingInfo.Message, loggingInfo.MessageParametersValues?.ToArray());

            if (loggingInfo.StartTime.HasValue)
                logMessage.AppendSpace().WithStartTime(loggingInfo.StartTime.Value.ToIso8601FormatWithMicroseconds());

            logMessage.AppendSpace().WithTestArguments(loggingInfo.TestArguments);

            Info(GetLoggingProviders(), logMessage);
        }

        public virtual void LogTestExecutionEnd(TestExecutionEndLoggingModel loggingInfo)
        {
            ILogMessage logMessage = GetTestExecutionLogMessage(loggingInfo);

            if (!string.IsNullOrEmpty(loggingInfo.Result))
                logMessage.AppendSpace().WithResult(loggingInfo.Result);

            if (loggingInfo.EndTime.HasValue)
                logMessage.AppendSpace().WithEndTime(loggingInfo.EndTime.Value.ToIso8601FormatWithMicroseconds());

            if (loggingInfo.Duration != null)
                logMessage.AppendSpace().WithDuration(loggingInfo.Duration?.ToString());

            if (loggingInfo.FailureMessages?.Count > 0)
                logMessage.AppendSpace().WithFailureMessages(loggingInfo.FailureMessages.ToArray());

            if (loggingInfo.StackTrace?.Count > 0)
                logMessage.AppendSpace().WithStackTrace(loggingInfo.StackTrace.ToArray());

            if (loggingInfo.Categories?.Count > 0)
                logMessage.AppendSpace().WithCategories(loggingInfo.Categories.ToArray());

            Info(GetLoggingProviders(), logMessage);
        }

        public virtual void LogTestSuiteEnd(TestSuiteEndLoggingModel loggingInfo)
        {
            ILogMessage logMessage = GetTestSuiteLogMessage(loggingInfo)
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);

            if (loggingInfo.EndTime.HasValue)
                logMessage.AppendSpace().WithEndTime(loggingInfo.EndTime.Value.ToIso8601FormatWithMicroseconds());

            if (loggingInfo.Duration != null)
                logMessage.AppendSpace().WithDuration(loggingInfo.Duration?.ToString());

            if (loggingInfo.Total != null)
                logMessage.AppendSpace().WithTotal(loggingInfo.Total?.ToString());

            if (loggingInfo.Passed != null)
                logMessage.AppendSpace().WithPassed(loggingInfo.Passed?.ToString());

            if (loggingInfo.Failed != null)
                logMessage.AppendSpace().WithFailed(loggingInfo.Failed?.ToString());

            if (loggingInfo.Skipped != null)
                logMessage.AppendSpace().WithSkipped(loggingInfo.Skipped?.ToString());

            if (loggingInfo.OtherResults?.Count > 0)
                logMessage.AppendSpace().WithOtherResults(loggingInfo.OtherResults);

            if (loggingInfo.FailureMessages?.Count > 0)
                logMessage.AppendSpace().WithFailureMessages(loggingInfo.FailureMessages.ToArray());

            if (loggingInfo.StackTrace?.Count > 0)
                logMessage.AppendSpace().WithStackTrace(loggingInfo.StackTrace.ToArray());

            if (loggingInfo.Categories?.Count > 0)
                logMessage.AppendSpace().WithCategories(loggingInfo.Categories.ToArray());

            Info(GetLoggingProviders(), logMessage);
        }

        public virtual void LogTestRunEnd(TestRunEndLoggingModel loggingInfo)
        {
            ILogMessage logMessage = GetTestRunBaseLogMessage(loggingInfo)
                .WithTestRunExecutionFlowStage(loggingInfo.TestRunExecutionFlowStage)
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);

            if (loggingInfo.EndTime.HasValue)
                logMessage.AppendSpace().WithEndTime(loggingInfo.EndTime.Value.ToIso8601FormatWithMicroseconds());

            if (loggingInfo.Duration != null)
                logMessage.AppendSpace().WithDuration(loggingInfo.Duration?.ToString());

            if (loggingInfo.Total != null)
                logMessage.AppendSpace().WithTotal(loggingInfo.Total?.ToString());

            if (loggingInfo.Passed != null)
                logMessage.AppendSpace().WithPassed(loggingInfo.Passed?.ToString());

            if (loggingInfo.Failed != null)
                logMessage.AppendSpace().WithFailed(loggingInfo.Failed?.ToString());

            if (loggingInfo.Skipped != null)
                logMessage.AppendSpace().WithSkipped(loggingInfo.Skipped?.ToString());

            if (loggingInfo.OtherResults?.Count > 0)
                logMessage.AppendSpace().WithOtherResults(loggingInfo.OtherResults);

            Info(GetLoggingProviders(), logMessage);
        }

        #endregion  Specific Time/Momentum Logging /Specific State

        #region Auxiliary Methods

        protected ILogMessage GetTestRunBaseLogMessage(TestRunBaseLoggingModel loggingInfo)
        {
            loggingInfo.TestRunId ??= TestRunId;
            loggingInfo.TestRunName ??= TestRunName;
            loggingInfo.VersionUnderTest ??= VersionUnderTest;

            return new LogMessage()
                .WithTestRunId(loggingInfo.TestRunId)
                .WithTestRunName(loggingInfo.TestRunName)
                .WithVersionUnderTest(loggingInfo.VersionUnderTest);
        }

        protected ILogMessage GetTestRunSpecificTimeStateLogMessage(TestRunBaseLoggingModel loggingInfo)
        {
            return GetTestRunBaseLogMessage(loggingInfo)
                .WithTestRunExecutionFlowStage(loggingInfo.TestRunExecutionFlowStage)
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);
        }

        protected ILogMessage GetTestRunHookExecutionLogMessage(TestRunHookExecutionLoggingModel loggingInfo)
        {
            return GetTestRunBaseLogMessage(loggingInfo)
                .WithTestRunExecutionFlowStage(loggingInfo.TestRunExecutionFlowStage)
                .WithHookClassId(loggingInfo.HookClassId)
                .WithHookClassName(loggingInfo.HookClassName)
                .WithHookId(loggingInfo.HookId)
                .WithHookMethodName(loggingInfo.HookMethodName)
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);
        }

        protected ILogMessage GetTestSuiteSpecificTimeStateLogMessage(TestSuiteLoggingModel loggingInfo)
        {
            return GetTestSuiteLogMessage(loggingInfo)
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);
        }

        protected ILogMessage GetTestSuiteHookExecutionLogMessage(TestSuiteHookExecutionLoggingModel loggingInfo)
        {
            return GetTestSuiteLogMessage(loggingInfo)
                .WithHookClassId(loggingInfo.HookClassId)
                .WithHookClassName(loggingInfo.HookClassName)
                .WithHookId(loggingInfo.HookId)
                .WithHookMethodName(loggingInfo.HookMethodName)
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);
        }

        protected ILogMessage GetTestSuiteLogMessage(TestSuiteLoggingModel loggingInfo)
        {
            return GetTestRunBaseLogMessage(loggingInfo)
                .WithTestSuiteName(loggingInfo.TestSuiteName)
                .WithTestSuiteId(loggingInfo.TestSuiteId)
                .WithTestRunExecutionFlowStage(loggingInfo.TestRunExecutionFlowStage);
        }

        protected ILogMessage GetTestHookExecutionLogMessage(TestHookExecutionLoggingModel loggingInfo)
        {
            return GetTestSuiteLogMessage(loggingInfo)
                .WithTestId(loggingInfo.TestId)
                .WithTestMethodName(loggingInfo.TestMethodName)
                .WithHookClassId(loggingInfo.HookClassId)
                .WithHookClassName(loggingInfo.HookClassName)
                .WithHookId(loggingInfo.HookId)
                .WithHookMethodName(loggingInfo.HookMethodName)
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);
        }

        protected ILogMessage GetTestExecutionLogMessage(TestExecutionLoggingModel loggingInfo)
        {
            return GetTestRunBaseLogMessage(loggingInfo)
                .WithTestSuiteName(loggingInfo.TestSuiteName)
                .WithTestSuiteId(loggingInfo.TestSuiteId)
                .WithTestRunExecutionFlowStage(loggingInfo.TestRunExecutionFlowStage)
                .WithTestId(loggingInfo.TestId)
                .WithTestMethodName(loggingInfo.TestMethodName)
                .WithTestRetryAttempt(loggingInfo.TestRetryAttempt?.ToString())
                .WithTestRepeatAttempt(loggingInfo.TestRepeatAttempt.ToString())
                .WithLogEntryOrderNumber(loggingInfo.LogEntryOrderNumber);
        }

        protected int GetLogEntryOrderNumber(string identifier)
        {
            lock (_getLogEntryOrderNumberLock)
            {
                if (!string.IsNullOrEmpty(identifier))
                    if (!LogEntryOrderNumber.TryGetValue(identifier, out int lastLogEntryOrderNumber))
                        LogEntryOrderNumber.TryAdd(identifier, 0);
                    else
                    {
                        int newLogEntryOrderNumber = lastLogEntryOrderNumber + 1;
                        LogEntryOrderNumber.Upsert(identifier, newLogEntryOrderNumber);
                        return newLogEntryOrderNumber;
                    }

                return 0;
            }
        }

        protected abstract Dictionary<string, object> GetTestArguments();

        protected void PopulateBaseRunInfo(TestRunBaseLoggingModel loggingInfo)
        {
            loggingInfo.TestRunId ??= TestRunId;
            loggingInfo.TestRunName ??= TestRunName;
            loggingInfo.VersionUnderTest ??= VersionUnderTest;
        }

        #endregion Auxiliary Methods
    }
}
