using DevQAProdCom.NET.Global.Helpers;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.DependencyInjection;

[SetUpFixture]
internal class RunHooks
{
    [OneTimeSetUp]
    public void StartRun()
    {
        IoHelper.CleanDirectory("Logs");
        DiContainer.Instance.Log.SetTestRunLoggingInfo(testRunId: SharedTestConstants.ManuallySetTestRunId, testRunDescription: SharedTestConstants.ManuallySetTestRunDescription,
            testRunName: SharedTestConstants.ManuallySetTestRunName, versionUnderTest: SharedTestConstants.ManuallySetVersionUnderTest);
        DiContainer.Instance.Log.Info($"SetUpFixture/OneTimeSetUp/StartRun [{{LogRecordId}}]", SharedTestConstants.LogRecordId.a18dee14);
    }

    [OneTimeTearDown]
    public void EndRun()
    {
        DiContainer.Instance.Log.Info($"SetUpFixture/OneTimeTearDown/EndRun [{{LogRecordId}}]", SharedTestConstants.LogRecordId.ab95bec6);
    }
}
