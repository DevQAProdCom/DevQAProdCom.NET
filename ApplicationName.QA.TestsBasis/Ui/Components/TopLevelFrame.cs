using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components
{
    public class TopLevelFrame : UiElement
    {
        [Find(Use.IdEquals, "frame-top-level0-button")]
        public IUiElement ButtonInsideTopLevel0Frame;

        [Find(Use.XPath, ".//table[@id='frame-top-level0-table2']")]
        public Table Table2;

        [Frame(Use.IdEquals, "frame-inside-frame-level1-id")]
        public FrameInsideFrame FrameInsideFrame;

        [Find(Use.XPath, ".//table[@id='frame-top-level0-table2']//tr//th")]
        public IUiElementsList<Cell> Cells;

        [Find(Use.IdEquals, "frame-inside-frame-level1-button", framesFindMethod: Use.IdEquals, framesFindCriteria: "frame-inside-frame-level1-id")]
        public IUiElement ButtonFrameInsideFrame;
    }
}
