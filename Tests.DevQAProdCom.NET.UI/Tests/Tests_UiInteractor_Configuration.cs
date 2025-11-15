using ApplicationName.QA.TestsBasis.Ui.PageServices;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    internal class Tests_UiInteractor_Configuration : PerFeatureBaseTest
    {

        private HtmlElementsTypesAndActionsTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<HtmlElementsTypesAndActionsTestPageService>();
        }

        [Test]
        public void Should_Check_Configuration_DownloadDefaultDirectory()
        {
            //GIVEN
            var downloadsDefaultDirectory = UiInteractor.DownloadsDefaultDirectory!;
            downloadsDefaultDirectory.Should().NotBeNullOrEmpty();
            var expectedFileName = $"random-file_{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}.txt";
            var actualDirectoryFilesBeforeDownload = IoHelper.GetFilesInDirectory(downloadsDefaultDirectory).Select(x => x.Name).ToList();

            //WHEN
            _pageService._page.InputFieldForDownloadedFileName.SetText(expectedFileName);
            _pageService._page.DownloadFileButton.AddBehavior<IUiElementDownloadBehavior>().DownloadFile();
            Thread.Sleep(500);
            var actualDirectoryFilesAfterDownload = IoHelper.GetFilesInDirectory(downloadsDefaultDirectory).Select(x => x.Name).ToList();

            //THEN
            using (new AssertionScope())
            {
                actualDirectoryFilesBeforeDownload.Should().NotContain(expectedFileName);
                actualDirectoryFilesAfterDownload.Should().Contain(expectedFileName);
            }
        }
    }
}
