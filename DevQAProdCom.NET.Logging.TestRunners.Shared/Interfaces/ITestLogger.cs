using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Test;

namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces
{
    public interface ITestLogger : ILogger, ITestLoggerConfigParameters
    {
        public void SetTestRunLoggingInfo(string? testRunId = null, string? testRunName = null, string? testRunDescription = null, string? versionUnderTest = null);

        #region Execution State Info

        public string GetTestRunExecutionFlowStage(string flowIdentifier = "");
        public void StartTestRunExecutionFlowStage(IHasTestRunExecutionFlowStageInfo loggingInfo, string flowIdentifier = "");
        public void EndTestRunExecutionFlowStage(string flowIdentifier = "");

        #endregion Execution State Info

        #region Execution Time/FeedThrough Logging

        public ILogMessage EnrichStagesOfExecutionFlowTypeWithMetadata(ILogMessage message);

        #endregion  Execution Time/FeedThrough Logging

        #region  Specific Time/Momentum Logging

        void LogTestRunStart(TestRunStartLoggingModel loggingInfo);
        void LogTestSuiteStart(TestSuiteStartLoggingModel loggingInfo);
        void LogTestStart(TestStartLoggingModel? loggingInfo = null);
        void LogTestExecutionEnd(TestExecutionEndLoggingModel loggingInfo);
        void LogTestSuiteEnd(TestSuiteEndLoggingModel loggingInfo);
        void LogTestRunEnd(TestRunEndLoggingModel loggingInfo);

        #endregion Specific Time/Momentum Logging
    }
}
