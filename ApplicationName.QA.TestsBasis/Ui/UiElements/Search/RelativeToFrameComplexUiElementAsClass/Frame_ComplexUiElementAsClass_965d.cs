using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.UiElements.Search.RelativeToFrameComplexUiElementAsClass
{
    public class Frame_ComplexUiElementAsClass_965d : UiElement
    {
        [Find(Use.IdEquals, "page->frameComplexUiElementAsClass(965d)->simpleUiElementAsInterface(965d)")]
        public IUiElement SimpleUiElementAsInterface_965d;
    }
}
