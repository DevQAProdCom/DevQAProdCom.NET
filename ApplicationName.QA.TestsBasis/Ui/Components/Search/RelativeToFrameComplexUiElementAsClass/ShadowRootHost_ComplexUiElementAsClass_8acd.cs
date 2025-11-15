using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToFrameComplexUiElementAsClass
{
    public class ShadowRootHost_ComplexUiElementAsClass_8acd : UiElement
    {
        [ShadowRootHost(Use.IdEquals, "page->frameComplexUiElementAsClass(8acd)->shadowRootHostComplexUiElementAsClass(8acd)->shadowRootHostSimpleUiElementAsInterface(8acd)")]
        public IUiElement ShadowRootHostSimpleUiElementAsInterface_8acd;
    }
}
