using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components
{
    public class TableWithCellsWithFrames : UiElement
    {
        [Find(Use.XPath, ".//tr")]
        public IUiElementsList<RowWithCellsWithFrames> RowsWithCellsWithFrames;

        [Frame(Use.XPath, ".//iframe[@id='frame-top-level0-a-id']")]
        public TopLevelFrame TopLevelFrame;
    }
}
