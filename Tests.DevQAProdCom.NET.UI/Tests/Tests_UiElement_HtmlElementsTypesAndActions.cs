using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_HtmlElementsTypesAndActions : PerFeatureBaseTest
    {
        private HtmlElementsTypesAndActionsTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<HtmlElementsTypesAndActionsTestPageService>();
        }

        [Test]
        public void Should_InputFile_UiElement_Upload_Files_And_Get_Files_List()
        {
            //GIVEN
            List<string> expectedUploadedFiles = new() { "SyncJavaScript_ReturnNonPrimitiveValue_RelativeToPage.js", "SyncJavaScript_ReturnNonPrimitiveValue_RelativeToUiElement.js" };
            var file1 = Path.Combine(Environment.CurrentDirectory, "TestData", expectedUploadedFiles.ElementAt(0));
            var file2 = Path.Combine(Environment.CurrentDirectory, "TestData", expectedUploadedFiles.ElementAt(1));

            //WHEN
            _pageService._page.UploadFileInputFieldOfFileType.UploadFiles(file1, file2);
            var actualUploadedFiles = _pageService._page.UploadFileInputFieldOfFileType.GetUploadedFilesList();

            //THEN
            actualUploadedFiles.Should().BeEquivalentTo(expectedUploadedFiles);
        }
    }
}
