using ApplicationName.QA.TestsBasis.Ui.PagesActions;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    internal class Tests_UiInteractor_Configuration : PerFeatureBaseTest
    {

        private HtmlElementsTypesAndActionsTestPageActions _pageActions;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageActions = UiInteractor.Interact<HtmlElementsTypesAndActionsTestPageActions>();
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
            _pageActions.Page.InputFieldForDownloadedFileName.SetText(expectedFileName);
            _pageActions.Page.DownloadFileButton.AddBehavior<IUiElementBehaviorDownloadFile>().DownloadFile();

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
