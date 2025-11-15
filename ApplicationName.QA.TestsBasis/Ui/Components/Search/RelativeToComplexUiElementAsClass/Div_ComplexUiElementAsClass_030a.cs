using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToComplexUiElementAsClass
{
    public class Div_ComplexUiElementAsClass_030a : UiElement
    {
        [Frame(Use.IdContains, "page->complexUiElementAsClass(030a)->uiElementsList(030a)_frameSimpleUiElementAsInterface(030a)")]
        public IUiElementsList<IUiElement> UiElementsListOfFrameSimpleUiElementsAsInterface_030a;
    }
}
