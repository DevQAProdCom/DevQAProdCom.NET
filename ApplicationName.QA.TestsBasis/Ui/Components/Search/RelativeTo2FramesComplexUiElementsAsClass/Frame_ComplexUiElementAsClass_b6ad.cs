using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeTo2FramesComplexUiElementsAsClass
{
    public class Frame_ComplexUiElementAsClass_b6ad : UiElement
    {
        [Find(Use.IdContains, "page->frameComplexUiElementAsClass(d299)->frameComplexUiElementAsClass(b6ad)->uiElementsList(56d9)_simpleUiElementAsInterface(56d9)")]
        public IUiElementsList<IUiElement> UiElementsListOfSimpleUiElementsAsInterface_56d9;
    }
}
