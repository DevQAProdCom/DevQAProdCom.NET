using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]

    internal class Tests_UiElement_UsageCombinations : PerScenarioBaseTest
    {
        [ThreadStatic] private static TestPage2Service _testPage2Service;

        [SetUp]
        public void SetUp()
        {
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
        }

        [Test]
        public void CUR_Should_Get_Table2_Row1_Cell_Text()
        {
            //GIVEN
            var expectedCellText = Const.Table2Rows[1].Cells![1].Text!;

            //WHEN
            var actualCellText = _testPage2Service._page.Table2.Rows.ElementAt(1).Cell.GetTextContent();

            //THEN
            actualCellText.Should().Be(expectedCellText);
        }

        [Test]
        public void Should_Get_Table2_Rows1_Cells1_Text()
        {
            //GIVEN
            var expectedCellText = Const.Table2Rows[1].Cells![1].Text!;

            //WHEN
            var actualCellText = _testPage2Service._page.Table2.Rows.ElementAt(1).Cells.ElementAt(1).GetTextContent();

            //THEN
            actualCellText.Should().Be(expectedCellText);
        }

        [Test]
        public void Should_Get_Table2_Rows_Select_Cells_Text()
        {
            //GIVEN
            var expectedCellsText = new List<string>()
            {
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualCellsText = _testPage2Service._page.Table2.Rows.Select(x => x.Cell.GetTextContent()).ToList();

            //THEN
            actualCellsText.Should().BeEquivalentTo(expectedCellsText);
        }

        [Test]
        public void Should_Get_Table2_Row1_Select_CellsText()
        {
            //GIVEN
            var expectedCellsText = new List<string>()
            {
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualCellsText = _testPage2Service._page.Table2.Rows.ElementAt(1).Cells.Select(x => x.GetTextContent()).ToList();

            //THEN
            actualCellsText.Should().BeEquivalentTo(expectedCellsText);
        }

        [Test]
        public void Should_Get_Table2_Rows_Select_Cells_Select_Text()
        {
            //GIVEN
            var expectedCellsText = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualCellsText = _testPage2Service._page.Table2.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();

            //THEN
            actualCellsText.Should().BeEquivalentTo(expectedCellsText);
        }

        [Test]
        public void Should_Get_Row1_Select_CellsText()
        {
            //GIVEN
            var expectedCellsText = new List<string>()
            {
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualCellsText = _testPage2Service._page.Rows.ElementAt(1).Cells.Select(x => x.GetTextContent()).ToList();

            //THEN
            actualCellsText.Should().BeEquivalentTo(expectedCellsText);
        }

        [Test]
        public void Should_Get_Table3_Rows1_Cells1_UlList_Select_Text()
        {
            //GIVEN
            var expectedListText = new List<string>()
            {
                "List | Text 1",
                "List | Text 2",
            };

            //WHEN
            var actualListText = _testPage2Service._page.Rows3!.ElementAt(1).Cells!.ElementAt(1).UlList!.Select(x => x.GetTextContent()).ToList();

            //THEN
            actualListText.Should().BeEquivalentTo(expectedListText);
        }

        [Test]
        public void Should_Get_TestListItemByIndex_Div_Button_Text()
        {
            //WHEN
            var actualButton0TextContent = _testPage2Service._page.TestListItems1[0].DivA.ButtonA.GetTextContent();
            var actualButton1TextContent = _testPage2Service._page.TestListItems1[1].DivA.ButtonA.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualButton0TextContent.Should().Be(Const.TestListItems1DivButtonsTextContent[0]);
                actualButton1TextContent.Should().Be(Const.TestListItems1DivButtonsTextContent[1]);
            }
        }

        [Test]
        public void Should_Get_TestListItemsSelect_Div_Button_Text()
        {
            //WHEN
            var actualText = _testPage2Service._page.TestListItems1.Select(x => x.DivA.ButtonA.GetTextContent()).ToList();

            //THEN
            actualText.Should().BeEquivalentTo(Const.TestListItems1DivButtonsTextContent);
        }

        [Test]
        public void Should_Return_Empty_List_Without_Throwing_Exceptions_When_No_Elements_Found_In_The_DOM()
        {
            //WHEN
            var listCount = _testPage2Service._page.NotExistingInTheDomList.Count();

            //THEN
            listCount.Should().Be(0);
        }
    }
}
