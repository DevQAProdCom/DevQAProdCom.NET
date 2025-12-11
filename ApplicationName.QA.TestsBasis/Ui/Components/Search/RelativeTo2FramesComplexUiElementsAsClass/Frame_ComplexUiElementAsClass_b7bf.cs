using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeTo2FramesComplexUiElementsAsClass
{
    public class Frame_ComplexUiElementAsClass_b7bf : UiElement
    {
        [Find(Use.IdEquals, "page->frameComplexUiElementAsClass(e720)->frameComplexUiElementAsClass(b7bf)->simpleUiElementAsInterface(b7bf)")]
        public IUiElement SimpleUiElementAsInterface_b7bf;
    }
}
