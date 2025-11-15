using DevQAProdCom.NET.Global.Helpers;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.DependencyInjection;

[SetUpFixture]
internal class RunHooks
{
    [OneTimeSetUp]
    public void ConfigureServicesDependencyInjection()
    {
        CleanLogsDirectory();
        RecreateScreenshotsDirectory();
        InitializeDiContainer();

        //DiContainer.Instance.Log.SetTestRunLoggingInfo(testRunId: $"RunId: {Guid.NewGuid().ToString().Substring(0, 5)}",
        //    testRunName: "RunName: RegressionTestRun",
        //    versionUnderTest: "Version: v0.3");
    }

    private void CleanLogsDirectory()
    {
        //TODO Create LogDirectory for each separate run
        IoHelper.CleanDirectory("Logs");
    }

    private void InitializeDiContainer()
    {
        var di = DiContainer.Instance;
    }

    private void RecreateScreenshotsDirectory()
    {
        if (Directory.Exists(Const.Screenshot_Directory))
            Directory.Delete(Const.Screenshot_Directory, true);
        Directory.CreateDirectory(Const.Screenshot_Directory);
    }
}
