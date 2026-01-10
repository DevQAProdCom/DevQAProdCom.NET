using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.UiElements
{
    public class TopLevelShadowRootHost : UiElement
    {
        [Find(Use.IdEquals, "shadow-root-host-top-level0-button")]
        public IUiElement ButtonInsideTopLevel0ShadowRootHost;

        [Find(Use.XPath, ".//table[@id='shadow-root-host-top-level0-table2']")]
        public Table Table2;

        [ShadowRootHost(Use.IdEquals, "shadow-root-host-inside-shadow-root-host-level1-id")]
        public ShadowRootHostInsideShadowRootHost ShadowRootHostInsideShadowRootHost;

        [Find(Use.XPath, ".//table[@id='shadow-root-host-top-level0-table2']//tr//th")]
        public IUiElementsList<Cell> Cells;

        [Find(Use.IdEquals, "shadow-root-host-inside-shadow-root-host-level1-button", shadowRootHostsFindMethod: Use.IdEquals, shadowRootHostsFindCriteria: "shadow-root-host-inside-shadow-root-host-level1-id")]
        public IUiElement ButtonShadowRootHostInsideShadowRootHost;
    }
}
