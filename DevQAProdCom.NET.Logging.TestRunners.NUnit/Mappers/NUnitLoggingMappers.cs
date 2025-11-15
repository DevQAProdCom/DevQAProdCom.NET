using DevQAProdCom.NET.Logging.TestRunners.NUnit.Constants;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Test;
using DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Root;

namespace DevQAProdCom.NET.Logging.TestRunners.NUnit.Mappers
{
    public static class NUnitLoggingMappers
    {
        public static TestRunStartLoggingModel ToTestRunStartLoggingModel(this TestRunStart info)
        {
            return new TestRunStartLoggingModel()
            {
                StartTime = info.StartTime,
                TestRunExecutionFlowStage = TestRunExecutionFlowStage.RunStart.ToString(),
                AdditionalInfo = new() { { Const.CommandLine, info.CommandLine } }
            };
        }

        public static TestSuiteStartLoggingModel ToTestSuiteStartLoggingModel(this TestSuiteStart info)
        {
            return new TestSuiteStartLoggingModel()
            {
                TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteStart.ToString(),
                TestSuiteName = info.FullName,
            };
        }

        public static TestExecutionEndLoggingModel ToTestExecutionEndLoggingModel(this TestCaseEnd info)
        {
            return new TestExecutionEndLoggingModel()
            {
                TestRunExecutionFlowStage = TestRunExecutionFlowStage.TestExecutionEnd.ToString(),
                TestSuiteName = info.ClassName,
                TestId = info.Id,
                TestMethodName = info.MethodName,
                Result = info.Result,
                EndTime = info.EndTime,
                FailureMessages = info.Failure?.Message != null ? new List<string>() { info.Failure.Message } : null,
                StackTrace = info.Failure?.StackTrace != null ? new List<string>() { info.Failure.StackTrace } : null,
                Duration = TimeSpan.FromSeconds(info.Duration),
                //Attributes = attributes
                Categories = info.Properties.Where(x => x.Name == Const.Category).Select(x => x.Value).ToList()
            };
        }

        public static TestSuiteEndLoggingModel ToTestSuiteEndLoggingModel(this TestSuiteEnd info)
        {
            return new TestSuiteEndLoggingModel()
            {
                TestRunExecutionFlowStage = TestRunExecutionFlowStage.SuiteEnd.ToString(),
                TestSuiteName = info.FullName,
                EndTime = info.EndTime,
                Duration = TimeSpan.FromSeconds(info.Duration),
                Total = info.Total,
                Passed = info.Passed,
                Failed = info.Failed,
                Skipped = info.Skipped,
                OtherResults = new Dictionary<string, string>()
                {
                    { Const.Inconclusive, $"{info.Inconclusive}" },
                    { Const.Warnings, $"{info.Warnings}" }
                },
                FailureMessages = info.Failure?.Message != null
                    ? new List<string>() { info.Failure.Message }
                    : null,
                StackTrace = info.Failure?.StackTrace != null
                    ? new List<string>() { info.Failure.StackTrace }
                    : null,
                Categories = info.Properties.Where(x => x.Name == Const.Category).Select(x => x.Value).ToList()
            };
        }

        public static TestRunEndLoggingModel ToTestRunEndLoggingModel(this TestRunEnd info)
        {
            return new TestRunEndLoggingModel()
            {
                TestRunExecutionFlowStage = TestRunExecutionFlowStage.RunEnd.ToString(),
                EndTime = info.EndTime,
                Duration = TimeSpan.FromSeconds(info.Duration),
                Total = info.Total,
                Passed = info.Passed,
                Failed = info.Failed,
                Skipped = info.Skipped,
                OtherResults = new Dictionary<string, string>() { { Const.Inconclusive, $"{info.Inconclusive}" }, { Const.Warnings, $"{info.Warnings}" } }
            };
        }
    }
}
