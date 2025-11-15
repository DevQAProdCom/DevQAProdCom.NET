using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiPage_UiElementsInstantiator : PerScenarioBaseTest
    {
        [ThreadStatic] private static TestPage2Service _testPage2Service;

        [SetUp]
        public void SetUp()
        {
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
        }

        [Test]
        public void Should_Get_Dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator()
        {
            //GIVEN
            var expectedCellText = Const.Table2Rows[1].Cells![1].Text!;

            //WHEN
            var actualCellText = _testPage2Service._page.Dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator.GetTextContent();

            //THEN
            actualCellText.Should().Be(expectedCellText);
        }

        [Test]
        public void Should_Get_Dynamic_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator()
        {
            //GIVEN
            var expectedCellText = Const.Table2Rows[1].Cells![1].Text!;

            //WHEN
            var actualCellText = _testPage2Service._page.Dynamic_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator.GetTextContent();

            //THEN
            actualCellText.Should().Be(expectedCellText);
        }

        [Test]
        public void Should_Get_Dynamic_UiElementsList_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator()
        {
            //GIVEN
            var expectedCellsText = new List<string>()
            {
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!
            };

            //WHEN
            var actualCellsText = _testPage2Service._page.Dynamic_UiElementsList_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator.Select(x => x.GetTextContent()).ToList();

            //THEN
            actualCellsText.Should().BeEquivalentTo(expectedCellsText);
        }

        [Test]
        public void Should_Get_Dynamic_UiElementsList_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator()
        {
            //GIVEN
            var expectedCellsText = new List<string>()
            {
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!
            };

            //WHEN
            var actualCellsText = _testPage2Service._page.Dynamic_UiElementsList_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator.Select(x => x.GetTextContent()).ToList();

            //THEN
            actualCellsText.Should().BeEquivalentTo(expectedCellsText);
        }
    }
}
