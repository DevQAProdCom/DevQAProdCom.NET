using ApplicationName.QA.TestsBasis.Ui.Components;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class FramesTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/FramesTestPage";

        [Find(Use.IdEquals, "button-not-in-frame")]
        public IUiElement ButtonNotInFrame;

        [Find(Use.IdEquals, "frame-top-level0-button", framesFindMethod: Use.IdEquals, framesFindCriteria: "frame-top-level0-a-id")]
        public IUiElement ButtonInsideTopLevel0Frame;

        [Find(Use.XPath, ".//table[@id='frame-top-level0-table2']//tr//th", framesFindMethod: Use.IdEquals, framesFindCriteria: "frame-top-level0-a-id")]
        public IUiElementsList<Cell> CellsInsideTopLevel0Frame;

        [Find(Use.XPath, ".//table[@id='frame-top-level0-table2']", framesFindMethod: Use.IdEquals, framesFindCriteria: "frame-top-level0-a-id")]
        public Table Table2InsideTopLevel0Frame;

        [Frame(Use.IdEquals, "frame-top-level0-a-id")]
        public TopLevelFrame TopLevelFrame;

        [Find(Use.IdEquals, "table-with-cells-with-frames")]
        public TableWithCellsWithFrames TableWithCellsWithFrames;

        [Frame(Use.NameEquals, "frame-top-level0-name")]
        public IUiElementsList<TopLevelFrame> TopLevelFramesList;
    }
}
