using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToShadowRootHostComplexUiElementAsClass
{
    public class ShadowRootHost_ComplexUiElementAsClass_bb3d : UiElement
    {
        [ShadowRootHost(Use.IdEquals, "page->shadowRootHostComplexUiElementAsClass(4e79)->shadowRootHostComplexUiElementAsClass(bb3d)->shadowRootHostSimpleUiElementAsInterface(bb3d)")]
        public IUiElement ShadowRootHostSimpleUiElementAsInterface_bb3d;
    }
}
