using ApplicationName.QA.TestsBasis.Ui.PageServices;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]

    internal class Tests_UiElement_MouseBehavior : PerScenarioBaseTest
    {
        [ThreadStatic] private static TestPage2Service _testPage2Service;
        [ThreadStatic] private static DragAndDropTestPageService _dragAndDropTestPageService;

        [Test]
        public void Should_MouseClick_IUiElement()
        {
            //GIVEN
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
            var expectedNameBeforeMouseClick = "Button Name Before Mouse Click/DoubleClick";
            var expectedNameAfterMouseClick = "Button Name After Mouse Click";

            //WHEN
            var actualNameBeforeMouseClick = _testPage2Service._page.MouseClickDoubleClickButton.GetTextContent();
            _testPage2Service._page.MouseClickDoubleClickButton.AddBehavior<IUiElementMouseBehavior>().MouseClick();
            var actualNameAfterMouseClick = _testPage2Service._page.MouseClickDoubleClickButton.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNameBeforeMouseClick.Should().Be(expectedNameBeforeMouseClick);
                actualNameAfterMouseClick.Should().Be(expectedNameAfterMouseClick);
            }
        }

        [Test]
        public void Should_MouseContextClick_IUiElement()
        {
            //GIVEN
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
            var expectedButtonNameBeforeMouseContextRightClick = "Button Name Before Mouse Context/Right Click";
            var expectedButtonNameAfterMouseContextRightClick = "Button Name After Mouse Context/Right Click";

            //WHEN
            var actualButtonNameBeforeMouseContextRightClick = _testPage2Service._page.MouseContextRightClickButton.GetTextContent();
            _testPage2Service._page.MouseContextRightClickButton.AddBehavior<IUiElementMouseBehavior>().MouseContextClickJs();
            var actualButtonNameAfterMouseContextRightClick = _testPage2Service._page.MouseContextRightClickButton.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualButtonNameBeforeMouseContextRightClick.Should().Be(expectedButtonNameBeforeMouseContextRightClick);
                actualButtonNameAfterMouseContextRightClick.Should().Be(expectedButtonNameAfterMouseContextRightClick);
            }
        }

        [Test]
        public void Should_DoubleClick_IUiElement()
        {
            //GIVEN
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
            var expectedButtonNameBeforeMouseDoubleClick = "Button Name Before Mouse Click/DoubleClick";
            var expectedButtonNameAfterMouseDoubleClick = "Button Name After Mouse Double Click";

            //WHEN
            var actualButtonNameBeforeMouseClick = _testPage2Service._page.MouseClickDoubleClickButton.GetTextContent();
            _testPage2Service._page.MouseClickDoubleClickButton.AddBehavior<IUiElementMouseBehavior>().MouseDoubleClick();
            var actualButtonNameAfterMouseClick = _testPage2Service._page.MouseClickDoubleClickButton.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualButtonNameBeforeMouseClick.Should().Be(expectedButtonNameBeforeMouseDoubleClick);
                actualButtonNameAfterMouseClick.Should().Be(expectedButtonNameAfterMouseDoubleClick);
            }
        }

        [Test]
        public void Should_MouseHover_IUiElement()
        {
            //GIVEN
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
            var expectedButtonNameBeforeMouseHover = "Button Name Before Mouse Hover";
            var expectedButtonNameAfterMouseHover = "Button Name After Mouse Hover";

            //WHEN
            var actualButtonNameBeforeMouseHover = _testPage2Service._page.MouseHoverButton.GetTextContent();
            _testPage2Service._page.MouseHoverButton.AddBehavior<IUiElementMouseBehavior>().MouseHover();
            var actualButtonNameAfterMouseHover = _testPage2Service._page.MouseHoverButton.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualButtonNameBeforeMouseHover.Should().Be(expectedButtonNameBeforeMouseHover);
                actualButtonNameAfterMouseHover.Should().Be(expectedButtonNameAfterMouseHover);
            }
        }

        [Test]
        public void Should_MouseDown_IUiElement()
        {
            //GIVEN
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
            var expectedButtonNameBeforeMouseDown = "Button Name Before Mouse Down";
            var expectedButtonNameAfterMouseDown = "Button Name After Mouse Down";

            //WHEN
            var actualButtonNameBeforeMouseDown = _testPage2Service._page.MouseDownButton.GetTextContent();
            _testPage2Service._page.MouseDownButton.AddBehavior<IUiElementMouseBehavior>().MouseDown();
            var actualButtonNameAfterMouseDown = _testPage2Service._page.MouseDownButton.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualButtonNameBeforeMouseDown.Should().Be(expectedButtonNameBeforeMouseDown);
                actualButtonNameAfterMouseDown.Should().Be(expectedButtonNameAfterMouseDown);
            }
        }

        [Test]
        public void Should_MouseUp_IUiElement()
        {
            //GIVEN
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
            var expectedButtonNameBeforeMouseUp = "Button Name Before Mouse Up";
            var expectedButtonNameAfterMouseUp = "Button Name After Mouse Up";

            //WHEN
            var actualButtonNameBeforeMouseUp = _testPage2Service._page.MouseUpButton.GetTextContent();
            _testPage2Service._page.MouseUpButton.AddBehavior<IUiElementMouseBehavior>().MouseDown();
            _testPage2Service._page.MouseUpButton.AddBehavior<IUiElementMouseBehavior>().MouseUp();
            var actualButtonNameAfterMouseUp = _testPage2Service._page.MouseUpButton.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualButtonNameBeforeMouseUp.Should().Be(expectedButtonNameBeforeMouseUp);
                actualButtonNameAfterMouseUp.Should().Be(expectedButtonNameAfterMouseUp);
            }
        }

        [Test]
        public void Should_DragAndDrop_IUiElement()
        {
            //GIVEN
            _dragAndDropTestPageService = UiInteractor.Interact<DragAndDropTestPageService>();
            var page = _dragAndDropTestPageService._page;
            List<string> expectedDoneListAfterDragAndDrop = new(Const.DragAndDrop.DoneListInitialState) { Const.DragAndDrop.ElementToDrag };

            //WHEN
            var elementToDrag = page.ToDoList.Single(x => x.GetTextContent() == Const.DragAndDrop.ElementToDrag);
            var elementToDropTo = page.DoneList.Single(x => x.GetTextContent() == Const.DragAndDrop.ElementToDropTo);

            elementToDrag.AddBehavior<IUiElementMouseBehavior>().DragAndDrop(elementToDropTo);
            //TODO Add waiter
            Thread.Sleep(1000);
            var actualDoneListAfterDragAndDrop = page.DoneList.Select(x => x.GetTextContent()).ToList();

            //THEN
            actualDoneListAfterDragAndDrop.Should().BeEquivalentTo(expectedDoneListAfterDragAndDrop);
        }

        [Test]
        public void Should_DragAndDropByOffset_IUiElement()
        {
            //GIVEN
            _dragAndDropTestPageService = UiInteractor.Interact<DragAndDropTestPageService>();
            var page = _dragAndDropTestPageService._page;
            List<string> expectedDoneListAfterDragAndDrop = new(Const.DragAndDrop.DoneListInitialState) { Const.DragAndDrop.ElementToDrag };

            //WHEN
            var elementToDrag = page.ToDoList.Single(x => x.GetTextContent() == Const.DragAndDrop.ElementToDrag);
            var elementToDropTo = page.DoneList.Single(x => x.GetTextContent() == Const.DragAndDrop.ElementToDropTo);

            var start = elementToDrag.GetLocation();
            var finish = elementToDropTo.GetLocation();

            var offsetX = finish.X - start.X;
            var offsetY = finish.Y - start.Y;

            elementToDrag.AddBehavior<IUiElementMouseBehavior>().DragAndDropByOffset(offsetX, offsetY);
            //TODO Add waiter
            Thread.Sleep(1000);
            var actualDoneListAfterDragAndDrop = page.DoneList.Select(x => x.GetTextContent()).ToList();

            //THEN
            actualDoneListAfterDragAndDrop.Should().BeEquivalentTo(expectedDoneListAfterDragAndDrop);
        }
    }
}
