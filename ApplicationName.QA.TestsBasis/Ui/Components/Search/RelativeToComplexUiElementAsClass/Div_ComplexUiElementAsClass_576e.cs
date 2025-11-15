using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToComplexUiElementAsClass
{
    public class Div_ComplexUiElementAsClass_576e : UiElement
    {
        [Find(Use.IdContains, "page->complexUiElementAsClass(576e)->uiElementsList(e141)_simpleUiElementAsInterface(e141)")]
        public IUiElementsList<IUiElement> UiElementsListOfSimpleUiElementsAsInterface_e141;
    }
}
