using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.UiElements
{
    public class ShadowRootHostInsideShadowRootHost : UiElement
    {
        [Find(Use.XPath, ".//table[@id='shadow-root-host-top-level0-table2']")]
        public Table Table2;

        [Find(Use.IdEquals, "shadow-root-host-inside-shadow-root-host-level1-button")]
        public IUiElement ButtonShadowRootHostInsideShadowRootHost;

        [Find(Use.XPath, ".//table[@id='shadow-root-host-top-level0-table2']//tr")]
        public IUiElementsList<Row> Rows;
    }
}
