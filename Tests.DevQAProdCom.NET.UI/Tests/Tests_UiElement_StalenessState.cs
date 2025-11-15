using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Models;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]

    internal class UiElement_StalenessState_Tests : PerScenarioBaseTest
    {
        [ThreadStatic] private static TestPage2Service _testPage2Service;
        [ThreadStatic] public static List<Row> Table2Rows;
        [ThreadStatic] public static List<Row> DeletableTable2Rows;

        [SetUp]
        public void SetUp()
        {
            Table2Rows = new List<Row>()
            {
                new Row()
                {
                    Cells = new List<Cell>()
                    {
                        new Cell("Table2 | Row_1 Cell_1 Header_1"),
                        new Cell("Table2 | Row_1 Cell_2 Header_2"),
                    }
                },
                new Row()
                {
                    Cells = new List<Cell>()
                    {
                        new Cell("Table2 | Row_2 Cell_1 SubHeader_1"),
                        new Cell("Table2 | Row_2 Cell_2 SubHeader_2"),
                    }
                }
            };

            DeletableTable2Rows = new List<Row>()
            {
                new Row()
                {
                    Cells = new List<Cell>()
                    {
                        new Cell("Deletable Table | Row_1 Cell_1 Header_1"),
                        new Cell("Deletable Table | Row_1 Cell_2 Header_2"),
                    }
                },
                new Row()
                {
                    Cells = new List<Cell>()
                    {
                        new Cell("Deletable Table | Deletable Cell | Row_2 Cell_1 SubHeader_1"),
                        new Cell("Deletable Table | Row_2 Cell_2 SubHeader_2"),
                    }
                }
            };

            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
        }

        [Test]
        public void Should_Get_Table_Row_IUIElementsListOfCells_Text_Add_Delete_Add_Table_Without_StaleElementReferenceException()
        {
            //GIVEN
            _testPage2Service._page.AddTableButton.MouseClick();
            var expectedCellsText = _testPage2Service._page.DeletableTable.Row.Cells.Select(x => x.GetTextContent()).ToList();

            //WHEN
            _testPage2Service._page.DeleteTableButton.MouseClick();
            _testPage2Service._page.AddTableButton.MouseClick();

            //THEN
            var actualCellsText = _testPage2Service._page.DeletableTable.Row.Cells.Select(x => x.GetTextContent()).ToList();
            actualCellsText.Should().BeEquivalentTo(expectedCellsText);
        }
    }
}
