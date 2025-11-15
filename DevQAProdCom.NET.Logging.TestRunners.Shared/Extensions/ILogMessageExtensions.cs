using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Extensions
{
    public static class ILogMessageExtensions
    {
        public static ILogMessage WithLogEntryOrderNumber(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[#{LogEntryOrderNumber}]", value);

            return message;
        }

        public static ILogMessage WithTestRunExecutionFlowStage(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{TestRunExecutionFlowStage}]", value);

            return message;
        }

        public static ILogMessage WithTestRunId(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{TestRunId}]", value);

            return message;
        }

        public static ILogMessage WithTestRunName(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{TestRunName}]", value);

            return message;
        }

        public static ILogMessage WithVersionUnderTest(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{VersionUnderTest}]", value);

            return message;
        }

        public static ILogMessage WithTestRunDescription(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("TestRunDescription: '{TestRunDescription}'.", value);

            return message;
        }

        public static ILogMessage WithTotalAmountOfTests(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Total Amount of Tests: '{TotalAmountOfTests}'.", value);

            return message;
        }

        public static ILogMessage WithTestsWithExecuteState(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Tests with Execute State: '{TestsWithExecuteState}'.", value);

            return message;
        }

        public static ILogMessage WithTestsWithSkipState(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Tests with Skip State: '{TestsWithSkipState}'.", value);

            return message;
        }

        public static ILogMessage WithClassId(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{ClassId}]", value);

            return message;
        }

        public static ILogMessage WithClassName(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{ClassName}]", value);

            return message;
        }

        public static ILogMessage WithHookClassId(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{HookClassId}]", value);

            return message;
        }

        public static ILogMessage WithHookClassName(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{HookClassName}]", value);

            return message;
        }

        public static ILogMessage WithHookId(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{HookId}]", value);

            return message;
        }

        public static ILogMessage WithHookMethodName(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{HookMethodName}]", value);

            return message;
        }

        public static ILogMessage WithHookExecutionOrder(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{HookExecutionOrder}]", value);

            return message;
        }

        public static ILogMessage WithTestSuiteId(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{TestSuiteId}]", value);

            return message;
        }

        public static ILogMessage WithTestSuiteName(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{TestSuiteName}]", value);

            return message;
        }

        public static ILogMessage WithTestSuiteExecutionOrder(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{TestSuiteExecutionOrder}]", value);

            return message;
        }

        public static ILogMessage WithTestId(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{TestId}]", value);

            return message;
        }

        public static ILogMessage WithTestMethodName(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{TestMethodName}]", value);

            return message;
        }

        public static ILogMessage WithTestExecutionOrder(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[{TestExecutionOrder}]", value);

            return message;
        }

        public static ILogMessage WithTestRetryAttempt(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[RetryAttempt: {TestRetryAttempt}]", value);

            return message;
        }

        public static ILogMessage WithTestRepeatAttempt(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("[RepeatAttempt: {TestRepeatAttempt}]", value);

            return message;
        }

        public static ILogMessage WithStartTime(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Start Time: '{StartTime}'.", value);

            return message;
        }

        public static ILogMessage WithTestArguments(this ILogMessage message, Dictionary<string, object>? arguments = null)
        {
            if (arguments?.Count > 0)
                message.Append("TestArguments: '{@TestArguments}'.", arguments);
            else
                message.Append("TestArguments: 'No Arguments {@TestArguments}'.", arguments); //Such implementation is done so that TestArguments parameter to be write to logs for query purposes even if it is empty.

            return message;
        }

        public static ILogMessage WithResult(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Result: '{Result}'.", value);

            return message;
        }

        public static ILogMessage WithEndTime(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("End Time: '{EndTime}'.", value);

            return message;
        }

        public static ILogMessage WithDuration(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Duration: '{Duration}'.", value);

            return message;
        }

        public static ILogMessage WithFailureMessages(this ILogMessage message, params string[] values)
        {
            if (values?.Length > 0)
                message.Append("Failure Messages: '{@FailureMessages}'.", values.ToList());

            return message;
        }

        public static ILogMessage WithStackTrace(this ILogMessage message, params string[] values)
        {
            if (values?.Length > 0)
                message.Append("Stack Trace: '{@StackTrace}'.", values);

            return message;
        }

        public static ILogMessage WithCategories(this ILogMessage message, params string[] values)
        {
            if (values?.Length > 0)
                message.Append("Categories: '{@Categories}'.", values);

            return message;
        }

        public static ILogMessage WithTotal(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Total: '{Total}'.", value);

            return message;
        }

        public static ILogMessage WithPassed(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Passed: '{Passed}'.", value);

            return message;
        }

        public static ILogMessage WithFailed(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Failed: '{Failed}'.", value);

            return message;
        }

        public static ILogMessage WithSkipped(this ILogMessage message, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                message.Append("Skipped: '{Skipped}'.", value);

            return message;
        }

        public static ILogMessage WithOtherResults(this ILogMessage message, Dictionary<string, string> values)
        {
            if (values?.Count > 0)
                message.Append("Other Results: '{@OtherResults}'.", values);

            return message;
        }

        public static ILogMessage WithAdditionalInfo(this ILogMessage message, Dictionary<string, object>? arguments = null)
        {
            if (arguments?.Count > 0)
                message.Append("Additional Info: '{@AdditionalInfo}'.", arguments);

            return message;
        }
    }
}
