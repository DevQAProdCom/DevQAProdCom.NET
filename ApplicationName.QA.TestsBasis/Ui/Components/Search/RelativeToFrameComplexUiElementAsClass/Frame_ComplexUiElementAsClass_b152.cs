using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToFrameComplexUiElementAsClass
{
    public class Frame_ComplexUiElementAsClass_b152 : UiElement
    {
        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(b152)->frameSimpleUiElementAsInterface(b152)")]
        public IUiElement FrameSimpleUiElementAsInterface_b152;
    }
}
