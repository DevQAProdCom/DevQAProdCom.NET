using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.UiElements
{
    public class RowWithCellsWithFrames : UiElement
    {
        [Find(Use.XPath, ".//th")]
        public IUiElementsList<CellWithFrame> CellsWithFrames;
    }
}
