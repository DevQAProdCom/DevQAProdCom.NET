using ApplicationName.QA.TestsBasis.Ui.PagesActions;
using ApplicationName.QA.TestsBasis.Ui.UiElementsActions;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class Tests_UiInteractorsManagerProvider_DependencyInjectionIntoActions
    {
        [ThreadStatic]
        private static TestPage2Actions _testPage2Actions;

        [ThreadStatic]
        private static TableUiElementActions _tableUiElementActions;

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

        [Test]
        public void Should_Get_Dynamic_UiElementsList_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator()
        {
            //GIVEN
            _tableUiElementActions = DiContainer.Instance.GetRequiredService<TableUiElementActions>();
            var expectedCellsText = new List<string>()
            {
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!
            };

            //WHEN
            var actualCellsText = _tableUiElementActions.UiElement.Dynamic_UiElementsList_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator.Select(x => x.GetTextContent()).ToList();

            //THEN
            actualCellsText.Should().BeEquivalentTo(expectedCellsText);
        }

        [Test]
        public void Should_Service_Injected_Into_Actions_Class_Return_Result()
        {
            //WHEN
            var actualText = _testPage2Actions.ServiceForDependencyInjectionIntoActions.GetMessage();

            //THEN
            actualText.Should().Be(nameof(ApplicationName.QA.TestsBasis.Services.ServiceForDependencyInjectionIntoActions));
        }

        [TearDown]
        public void TearDown()
        {
            DiContainer.Instance.UiInteractorsManagersProvider.DisposeUiInteractorsManagerOfCurrentThread();
        }
    }
}
