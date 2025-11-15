using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToFrameComplexUiElementAsClass
{
    public class Frame_ComplexUiElementAsClass_50c2 : UiElement
    {
        [Frame(Use.IdContains, "page->frameComplexUiElementAsClass(50c2)->uiElementsList(50c2)_frameSimpleUiElementAsInterface(50c2)")]
        public IUiElementsList<IUiElement> UiElementsListOfFrameSimpleUiElementsAsInterface_50c2;
    }
}
