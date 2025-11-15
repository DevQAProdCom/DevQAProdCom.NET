using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToShadowRootHostComplexUiElementAsClass
{
    public class ShadowRootHost_ComplexUiElementAsClass_ba6b : UiElement
    {
        [ShadowRootHost(Use.IdContains, "page->shadowRootHostComplexUiElementAsClass(ba6b)->uiElementsList(ba6b)_shadowRootHostSimpleUiElementAsInterface(ba6b)")]
        public IUiElementsList<IUiElement> UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_ba6b;
    }
}
