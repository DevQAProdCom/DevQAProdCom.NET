namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations
{
    public enum TestRunExecutionFlowStage
    {
        UnIdentifiedTestRunExecutionFlowStage,

        RunStart,

        RunOneTimeSetUpSetStart,
        RunOneTimeSetUpStart,
        RunOneTimeSetUpExecution,
        RunOneTimeSetUpEnd,
        RunOneTimeSetUpSetEnd,

        SuiteStart,

        SuiteOneTimeSetUpSetStart,
        SuiteOneTimeSetUpStart,
        SuiteOneTimeSetUpExecution,
        SuiteOneTimeSetUpEnd,
        SuiteOneTimeSetUpSetEnd,

        TestStart,

        TestSetUpSetStart,
        TestSetUpStart,
        TestSetUpExecution,
        TestSetUpEnd,
        TestSetUpSetEnd,

        TestExecutionStart,
        TestExecution,
        TestExecutionEnd,

        TestTearDownSetStart,
        TestTearDownStart,
        TestTearDownExecution,
        TestTearDownEnd,
        TestTearDownSetEnd,

        TestEnd,

        SuiteOneTimeTearDownSetStart,
        SuiteOneTimeTearDownStart,
        SuiteOneTimeTearDownExecution,
        SuiteOneTimeTearDownEnd,
        SuiteOneTimeTearDownSetEnd,

        SuiteEnd,

        RunOneTimeTearDownSetStart,
        RunOneTimeTearDownStart,
        RunOneTimeTearDownExecution,
        RunOneTimeTearDownEnd,
        RunOneTimeTearDownSetEnd,

        RunEnd,
    }
}
