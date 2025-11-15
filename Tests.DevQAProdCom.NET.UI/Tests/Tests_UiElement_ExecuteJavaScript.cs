using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.Models;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiElement_ExecuteJavaScript : ExecuteJavaScript_PerScenarioBaseTest
    {
        private FileInfo _fileWithSyncJavaScriptVoidRelativeToUiElement = new($@"{Const.Folders.TestData}\SyncJavaScript_Void_RelativeToUiElement.js");
        private FileInfo _fileWithSyncJavaScriptToReturnNonPrimitiveValueRelativeToUiElement = new($@"{Const.Folders.TestData}\SyncJavaScript_ReturnNonPrimitiveValue_RelativeToUiElement.js");
        private FileInfo _fileWithSyncJavaScriptToReturnPrimitiveValueRelativeToUiElement = new($@"{Const.Folders.TestData}\SyncJavaScript_ReturnPrimitiveValue_RelativeToUiElement.js");

        [SetUp]
        public void SetUp()
        {
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
            _visibleButtonArgument = new KeyValuePair<string, object>("uiElementArgument", _testPage2Service._page.VisibleButton);
        }

        #region Execute JavaScript Relative to UiElement

        [Test]
        public void Should_Execute_Void_JavaScript_Relative_To_UiElement()
        {
            //GIVEN
            var actualBackgroundColorBeforeScriptExecution = _testPage2Service._page.VisibleButton.GetCssValue(BackgroundColorPropertyName);

            //WHEN
            _testPage2Service._page.VisibleButton.ExecuteJavaScript(_fileWithSyncJavaScriptVoidRelativeToUiElement);
            var actualBackgroundColorAfterScriptExecution = _testPage2Service._page.VisibleButton.GetCssValue(BackgroundColorPropertyName);

            //THEN
            using (new AssertionScope())
            {
                actualBackgroundColorBeforeScriptExecution.Should().Contain(ExpectedBackgroundColorBeforeScriptExecution);
                actualBackgroundColorAfterScriptExecution.Should().Contain(ExpectedBackgroundColorAfterScriptExecution);
            }
        }

        [Test]
        public void Should_Return_Non_Primitive_Value_Using_Execute_JavaScript_T_Relative_To_UiElement()
        {
            //WHEN
            var actualBoundingClientRect = _testPage2Service._page.VisibleButton.ExecuteJavaScript<BoundingClientRect>(_fileWithSyncJavaScriptToReturnNonPrimitiveValueRelativeToUiElement);

            //THEN
            using (new AssertionScope())
            {
                actualBoundingClientRect.Width.Should().Be(ExpectedBoundingClientRectWidth);
                actualBoundingClientRect.Height.Should().Be(ExpectedBoundingClientRectHeight);
            }
        }

        [Test]
        public void Should_Return_Primitive_Value_Using_Execute_JavaScript_T_Relative_To_UiElement()
        {
            //WHEN
            var actualBoundingClientRectWidth = _testPage2Service._page.VisibleButton.ExecuteJavaScript<int>(_fileWithSyncJavaScriptToReturnPrimitiveValueRelativeToUiElement);

            //THEN
            actualBoundingClientRectWidth.Should().Be(ExpectedBoundingClientRectWidth);
        }

        #endregion Execute JavaScript Relative to UiElement

        #region Execute JavaScript Relative to Parent UiElement

        [Test]
        public void Should_Execute_Void_JavaScript_Relative_To_UiElement_As_Parent_Container()
        {
            //GIVEN
            var actualBackgroundColorBeforeScriptExecution = _testPage2Service._page.Form.GetCssValue(BackgroundColorPropertyName);

            //WHEN
            _testPage2Service._page.Form.ExecuteJavaScript(_fileWithSyncJavaScriptVoidRelativeToUiElement);
            var actualBackgroundColorAfterScriptExecution = _testPage2Service._page.Form.GetCssValue(BackgroundColorPropertyName);

            //THEN
            using (new AssertionScope())
            {
                actualBackgroundColorBeforeScriptExecution.Should().Contain(ExpectedBackgroundColorBeforeScriptExecution);
                actualBackgroundColorAfterScriptExecution.Should().Contain(ExpectedBackgroundColorAfterScriptExecution);
            }
        }

        [Test]
        public void Should_Return_Non_Primitive_Value_Using_Execute_JavaScript_T_Relative_To_UiElement_As_Parent_Container()
        {
            //WHEN
            var actualBoundingClientRect = _testPage2Service._page.Form.ExecuteJavaScript<BoundingClientRect>(_fileWithSyncJavaScriptToReturnNonPrimitiveValueRelativeToUiElement);

            //THEN
            using (new AssertionScope())
            {
                actualBoundingClientRect.Width.Should().Be(ExpectedBoundingClientRectWidth);
                actualBoundingClientRect.Height.Should().Be(ExpectedBoundingClientRectHeight);
            }
        }

        [Test]
        public void Should_Return_Primitive_Value_Using_Execute_JavaScript_T_Relative_To_UiElement_As_Parent_Container()
        {
            //WHEN
            var actualBoundingClientRectWidth = _testPage2Service._page.Form.ExecuteJavaScript<int>(_fileWithSyncJavaScriptToReturnPrimitiveValueRelativeToUiElement);

            //THEN
            actualBoundingClientRectWidth.Should().Be(ExpectedBoundingClientRectWidth);
        }

        #endregion Execute JavaScript Relative to Parent UiElement
    }
}
