using ApplicationName.QA.TestsBasis.Ui.PagesActions;
using ApplicationName.QA.TestsBasis.Ui.UiElementsActions;
using FluentAssertions;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class Tests_UiInteractor_Actions : PerScenarioBaseTest
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
        public void Should_Get_Dynamic_UiElementsList_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator()
        {
            //GIVEN
            var tableUiElementActions = UiInteractor.GetUiElementActions<TableUiElementActions>();
            var expectedCellsText = new List<string>()
            {
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!
            };

            //WHEN
            var actualCellsText = tableUiElementActions.UiElement.Dynamic_UiElementsList_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator.Select(x => x.GetTextContent()).ToList();

            //THEN
            actualCellsText.Should().BeEquivalentTo(expectedCellsText);
        }
    }
}
