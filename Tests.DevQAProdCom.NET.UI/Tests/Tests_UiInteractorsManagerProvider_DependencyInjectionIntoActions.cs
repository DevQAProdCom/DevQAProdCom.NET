using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class Tests_UiInteractorsManagerProvider_DependencyInjectionIntoActions
    {
        [ThreadStatic]
        private static TestPage2Actions _testPage2Actions;

        [SetUp]
        public void SetUp()
        {
            _testPage2Actions = DiContainer.Instance.GetRequiredService<TestPage2Actions>();
            _testPage2Actions.GoToPage();
        }

        [Test]
        public void Should_Find_UiElement_Using_IdEquals()
        {
            //WHEN
            var actualText = _testPage2Actions.Page.UseIdEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.IdEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_IdContains()
        {
            //WHEN
            var actualText = _testPage2Actions.Page.UseIdContains.GetTextContent();
            var actualIdAttribute = _testPage2Actions.Page.UseIdContains.GetIdAttribute();

            //THEN
            using (new AssertionScope())
            {
                actualText.Should().Be("Use.IdContains");
                actualIdAttribute.Should().Be("use-id-contains-value");
            }
        }

        [TearDown]
        public void TearDown()
        {
            DiContainer.Instance.UiInteractorsManagersProvider.DisposeUiInteractorsManagerOfCurrentThread();
        }
    }
}
