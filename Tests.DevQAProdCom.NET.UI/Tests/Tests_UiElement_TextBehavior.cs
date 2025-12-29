using ApplicationName.QA.TestsBasis.Ui.PagesActions;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]

    internal class Tests_UiElement_TextBehavior : PerScenarioBaseTest
    {
        [ThreadStatic] private static TestPage2Actions _testPage2Actions;
        private readonly string _expectedTextBeforeFulfillment = "Before Text";
        private readonly string _fulfilledText = " + Fulfilled Text";
        private string _expectedTextAfterAppend => $"Before Text{_fulfilledText}";

        [SetUp]
        public void SetUp()
        {
            _testPage2Actions = UiInteractor.Interact<TestPage2Actions>();
        }

        [Test]
        public void Should_InputTextBox_Handle_SetText_And_GetInputText_Behaviors()
        {
            //GIVEN
            var actualTextBeforeFulfillment = _testPage2Actions.Page.InputTextBox.GetInputText();
            var expectedTextAfterFulfillment = _fulfilledText;

            //WHEN
            _testPage2Actions.Page.InputTextBox.SetText(_fulfilledText);

            //THEN
            var actualTextAfterFulfillment = _testPage2Actions.Page.InputTextBox.GetInputText();

            using (new AssertionScope())
            {
                actualTextBeforeFulfillment.Should().BeEquivalentTo(_expectedTextBeforeFulfillment);
                actualTextAfterFulfillment.Should().BeEquivalentTo(expectedTextAfterFulfillment);
            }
        }

        [Test]
        public void Should_InputTextBox_Handle_AppendText_And_GetInputText_Behaviors()
        {
            //GIVEN
            var actualTextBeforeFulfillment = _testPage2Actions.Page.InputTextBox.GetInputText();
            var expectedTextAfterFulfillment = _expectedTextAfterAppend;

            //WHEN
            _testPage2Actions.Page.InputTextBox.AppendText(_fulfilledText);

            //THEN
            var actualTextAfterFulfillment = _testPage2Actions.Page.InputTextBox.GetInputText();

            using (new AssertionScope())
            {
                actualTextBeforeFulfillment.Should().BeEquivalentTo(_expectedTextBeforeFulfillment);
                actualTextAfterFulfillment.Should().BeEquivalentTo(expectedTextAfterFulfillment);
            }
        }

        [Test]
        public void Should_TextArea_Handle_SetText_And_GetInputText_Behaviors()
        {
            //GIVEN
            var actualTextBeforeFulfillment = _testPage2Actions.Page.TextArea.GetInputText();
            var expectedTextAfterFulfillment = _fulfilledText;

            //WHEN
            _testPage2Actions.Page.TextArea.SetText(_fulfilledText);

            //THEN
            var actualTextAfterFulfillment = _testPage2Actions.Page.TextArea.GetInputText();

            using (new AssertionScope())
            {
                actualTextBeforeFulfillment.Should().BeEquivalentTo(_expectedTextBeforeFulfillment);
                actualTextAfterFulfillment.Should().BeEquivalentTo(expectedTextAfterFulfillment);
            }
        }

        [Test]
        public void Should_TextArea_Handle_AppendText_And_GetInputText_Behaviors()
        {
            //GIVEN
            var actualTextBeforeFulfillment = _testPage2Actions.Page.TextArea.GetInputText();
            var expectedTextAfterFulfillment = _expectedTextAfterAppend;

            //WHEN
            _testPage2Actions.Page.TextArea.AppendText(_fulfilledText);

            //THEN
            var actualTextAfterFulfillment = _testPage2Actions.Page.TextArea.GetInputText();

            using (new AssertionScope())
            {
                actualTextBeforeFulfillment.Should().BeEquivalentTo(_expectedTextBeforeFulfillment);
                actualTextAfterFulfillment.Should().BeEquivalentTo(expectedTextAfterFulfillment);
            }
        }
    }
}
