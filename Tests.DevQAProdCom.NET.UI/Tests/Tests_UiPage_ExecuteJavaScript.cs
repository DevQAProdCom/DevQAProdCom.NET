using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.Models;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]

    internal class UiPage_ExecuteJavaScript_Tests : ExecuteJavaScript_PerScenarioBaseTest
    {
        private FileInfo _fileSyncJavaScriptVoidRelativeToPage = new($@"{Const.Folders.TestData}\SyncJavaScript_Void_RelativeToPage.js");
        private FileInfo _fileWithSyncJavaScriptToReturnNonPrimitiveValueRelativeToPage = new($@"{Const.Folders.TestData}\SyncJavaScript_ReturnNonPrimitiveValue_RelativeToPage.js");
        private FileInfo _fileWithSyncJavaScriptToReturnPrimitiveValueRelativeToPage = new($@"{Const.Folders.TestData}\SyncJavaScript_ReturnPrimitiveValue_RelativeToPage.js");

        [SetUp]
        public void SetUp()
        {
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
            _visibleButtonArgument = new KeyValuePair<string, object>("uiElementArgument", _testPage2Service._page.VisibleButton);
        }

        #region Execute JavaScript Relative to Page

        [Test]
        public void Should_Execute_Void_JavaScript_Relative_To_Page()
        {
            //GIVEN
            var actualBackgroundColorBeforeScriptExecution = _testPage2Service._page.VisibleButton.GetCssValue(BackgroundColorPropertyName);

            //WHEN
            _testPage2Service._page.ExecuteJavaScript(_fileSyncJavaScriptVoidRelativeToPage, _visibleButtonArgument);
            var actualBackgroundColorAfterScriptExecution = _testPage2Service._page.VisibleButton.GetCssValue(BackgroundColorPropertyName);

            //THEN
            using (new AssertionScope())
            {
                actualBackgroundColorBeforeScriptExecution.Should().Contain(ExpectedBackgroundColorBeforeScriptExecution);
                actualBackgroundColorAfterScriptExecution.Should().Contain(ExpectedBackgroundColorAfterScriptExecution);
            }
        }

        [Test]
        public void Should_Return_Non_Primitive_Value_Using_Execute_JavaScript_T_Relative_To_Page()
        {
            //WHEN
            var actualBoundingClientRect = _testPage2Service._page
                .ExecuteJavaScript<BoundingClientRect>(_fileWithSyncJavaScriptToReturnNonPrimitiveValueRelativeToPage, _visibleButtonArgument);

            //THEN
            using (new AssertionScope())
            {
                actualBoundingClientRect.Width.Should().Be(ExpectedBoundingClientRectWidth);
                actualBoundingClientRect.Height.Should().Be(ExpectedBoundingClientRectHeight);
            }
        }

        [Test]
        public void Should_Return_Primitive_Value_Using_Execute_JavaScript_T_Relative_To_Page()
        {
            //WHEN
            var actualBoundingClientRectWidth = _testPage2Service._page.ExecuteJavaScript<int>(_fileWithSyncJavaScriptToReturnPrimitiveValueRelativeToPage, _visibleButtonArgument);

            //THEN
            actualBoundingClientRectWidth.Should().Be(ExpectedBoundingClientRectWidth);
        }

        #endregion Execute JavaScript Relative to Page
    }
}
