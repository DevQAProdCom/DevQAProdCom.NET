using System.Diagnostics;
using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiPage_MouseBehavior : PerScenarioBaseTest
    {
        private const string ExpectedElementForPageMouseScrollTextContent = "Element for Page Mouse Scroll";
        private const string ExpectedElementForPageMouseScrollVerticallyTextContent = "Element for Page Mouse Scroll Vertically";
        private const string ExpectedElementForPageMouseScrollHorizontallyTextContent = "Element for Page Mouse Scroll Horizontally";
        private const int expectedScrollTimeToBeGreaterOrEqualToSec = 5;


        [Test]
        public void Should_Page_MouseScroll()
        {
            //GIVEN
            var page = UiInteractor.Interact<ScrollTestPageService>()._page;
            var element = page.ElementForPageMouseScroll;

            //WHEN
            var actualIsElementInViewportBeforeScroll = element.IsElementInViewportJs();
            page.MouseScroll(3000, 3000);

            var actualIsElementInViewportAfterScroll = element.IsElementInViewportJs();
            var actualElementTextContent = element.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualIsElementInViewportBeforeScroll.Should().BeFalse();
                actualIsElementInViewportAfterScroll.Should().BeTrue();
                actualElementTextContent.Should().Be(ExpectedElementForPageMouseScrollTextContent);
            }
        }

        [Test]
        public void Should_Page_MouseScroll_UntilCondition()
        {
            //GIVEN
            var page = UiInteractor.Interact<ScrollTestPageService>()._page;
            var element = page.ElementForPageMouseScroll;

            //WHEN
            var actualIsElementInViewportBeforeScroll = element.IsElementInViewportJs();

            var stopwatch = Stopwatch.StartNew();
            page.MouseScroll(100, 100, () => element.IsElementInViewportJs());
            stopwatch.Stop();

            var actualIsElementInViewportAfterScroll = element.IsElementInViewportJs();
            var actualElementTextContent = element.GetTextContent();
            var actualScrollElapsedTime = stopwatch.Elapsed;

            //THEN
            using (new AssertionScope())
            {
                actualIsElementInViewportBeforeScroll.Should().BeFalse();
                actualIsElementInViewportAfterScroll.Should().BeTrue();
                actualElementTextContent.Should().Be(ExpectedElementForPageMouseScrollTextContent);

                actualScrollElapsedTime.TotalSeconds.Should().BeGreaterOrEqualTo(expectedScrollTimeToBeGreaterOrEqualToSec); //Element X Position = 3000px, Y Position = 3000px. Scroll Step = 100px. PollingIntervalSec = 0.5.
            }
        }

        [Test]
        public void Should_Page_MouseScrollVertically()
        {
            //GIVEN
            var page = UiInteractor.Interact<ScrollTestPageService>()._page;
            var element = page.ElementForPageMouseScrollVertically;

            //WHEN
            var actualIsElementInViewportBeforeScroll = element.IsElementInViewportJs();
            page.MouseScrollVertically(3000);

            var actualIsElementInViewportAfterScroll = element.IsElementInViewportJs();
            var actualElementTextContent = element.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualIsElementInViewportBeforeScroll.Should().BeFalse();
                actualIsElementInViewportAfterScroll.Should().BeTrue();
                actualElementTextContent.Should().Be(ExpectedElementForPageMouseScrollVerticallyTextContent);
            }
        }

        [Test]
        public void Should_Page_MouseScrollVertically_UntilCondition()
        {
            //GIVEN
            var page = UiInteractor.Interact<ScrollTestPageService>()._page;
            var element = page.ElementForPageMouseScrollVertically;

            //WHEN
            var actualIsElementInViewportBeforeScroll = element.IsElementInViewportJs();

            var stopwatch = Stopwatch.StartNew();
            page.MouseScrollVertically(100, () => element.IsElementInViewportJs(), pollingIntervalSec: 0.5);
            stopwatch.Stop();

            var actualIsElementInViewportAfterScroll = element.IsElementInViewportJs();
            var actualElementTextContent = element.GetTextContent();
            var actualScrollElapsedTime = stopwatch.Elapsed;

            //THEN
            using (new AssertionScope())
            {
                actualIsElementInViewportBeforeScroll.Should().BeFalse();
                actualIsElementInViewportAfterScroll.Should().BeTrue();
                actualElementTextContent.Should().Be(ExpectedElementForPageMouseScrollVerticallyTextContent);

                actualScrollElapsedTime.TotalSeconds.Should().BeGreaterOrEqualTo(expectedScrollTimeToBeGreaterOrEqualToSec); //Element Y Position = 3000px. Scroll Step = 100px. PollingIntervalSec = 0.5.
            }
        }

        [Test]
        public void Should_Page_MouseScrollHorizontally()
        {
            //GIVEN
            var page = UiInteractor.Interact<ScrollTestPageService>()._page;
            var element = page.ElementForPageMouseScrollHorizontally;

            //WHEN
            var actualIsElementInViewportBeforeScroll = element.IsElementInViewportJs();
            page.MouseScrollHorizontally(3000);

            var actualIsElementInViewportAfterScroll = element.IsElementInViewportJs();
            var actualElementTextContent = element.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualIsElementInViewportBeforeScroll.Should().BeFalse();
                actualIsElementInViewportAfterScroll.Should().BeTrue();
                actualElementTextContent.Should().Be(ExpectedElementForPageMouseScrollHorizontallyTextContent);
            }
        }

        [Test]
        public void Should_Page_MouseScrollHorizontally_UntilCondition()
        {
            //GIVEN
            var page = UiInteractor.Interact<ScrollTestPageService>()._page;
            var element = page.ElementForPageMouseScrollHorizontally;

            //WHEN
            var actualIsElementInViewportBeforeScroll = element.IsElementInViewportJs();

            var stopwatch = Stopwatch.StartNew();
            page.MouseScrollHorizontally(100, () => element.IsElementInViewportJs(), pollingIntervalSec: 0.5);
            stopwatch.Stop();

            var actualIsElementInViewportAfterScroll = element.IsElementInViewportJs();
            var actualElementTextContent = element.GetTextContent();
            var actualScrollElapsedTime = stopwatch.Elapsed;

            //THEN
            using (new AssertionScope())
            {
                actualIsElementInViewportBeforeScroll.Should().BeFalse();
                actualIsElementInViewportAfterScroll.Should().BeTrue();
                actualElementTextContent.Should().Be(ExpectedElementForPageMouseScrollHorizontallyTextContent);

                actualScrollElapsedTime.TotalSeconds.Should().BeGreaterOrEqualTo(expectedScrollTimeToBeGreaterOrEqualToSec); //Element X Position = 3000px. Scroll Step = 100px. PollingIntervalSec = 0.5.
            }
        }

        [Test]
        public void Should_Page_MouseMove()
        {
            //GIVEN
            var expectedElementContentBeforeMouseMove = "Element for Mouse Move (Mouse Enter) - Content Before Mouse Move (Mouse Enter)";
            var expectedElementContentAfterMouseMove = "Element for Mouse Move (Mouse Enter) - Content After Mouse Move (Mouse Enter)";

            var page = UiInteractor.Interact<MouseActionsTestPageService>()._page;
            var element = page.ElementForMouseMoveMouseEnter;
            var actualElementLocation = element.GetLocation();

            //WHEN
            var actualElementContentBeforeMouseMove = element.GetTextContent();
            page.MouseMove(actualElementLocation.X + 1, actualElementLocation.Y + 1);
            var actualElementContentAfterMouseMove = element.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualElementContentBeforeMouseMove.Should().Be(expectedElementContentBeforeMouseMove);
                actualElementContentAfterMouseMove.Should().Be(expectedElementContentAfterMouseMove);
            }
        }
    }
}
