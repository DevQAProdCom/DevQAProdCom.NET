using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components
{
    public class FrameInsideFrame : UiElement
    {
        [Find(Use.XPath, ".//table[@id='frame-top-level0-table2']")]
        public Table Table2;

        [Find(Use.IdEquals, "frame-inside-frame-level1-button")]
        public IUiElement ButtonFrameInsideFrame;

        [Find(Use.XPath, ".//table[@id='frame-top-level0-table2']//tr")]
        public IUiElementsList<Row> Rows;
    }
}
