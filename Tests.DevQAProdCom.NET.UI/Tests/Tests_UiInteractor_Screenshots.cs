using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiInteractor_Screenshots : PerScenarioBaseTest
    {
        private const string ScreenshotDirectory = "ScreenshotDirectory";

        [SetUp]
        public void SetUp()
        {
            if (Directory.Exists(ScreenshotDirectory))
                Directory.Delete(ScreenshotDirectory, true);
            Directory.CreateDirectory(ScreenshotDirectory);
        }

        [Test]
        public void Should_UiInteractor_Make_Screenshots()
        {
            //GIVEN
            UiInteractor.Interact<TestPageTab1Service>(Const.Tab1);
            UiInteractor.Interact<TestPageTab2Service>(Const.Tab2);

            //WHEN
            UiInteractor.MakeScreenshot(directoryPath: ScreenshotDirectory, fileNamePrefix: "UiInteractor_Make_Screenshots_Test");

            //THEN
            var actualFiles = Directory.EnumerateFiles(ScreenshotDirectory);

            using (new AssertionScope())
                actualFiles.Count().Should().Be(2);
        }
    }
}
