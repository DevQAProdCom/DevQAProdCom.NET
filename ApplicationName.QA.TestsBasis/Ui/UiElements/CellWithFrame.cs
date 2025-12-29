using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.UiElements
{
    public class CellWithFrame : UiElement
    {
        [Frame(Use.XPath, ".//iframe[@id='frame-top-level0-a-id']")]
        public TopLevelFrame TopLevelFrame;
    }
}
