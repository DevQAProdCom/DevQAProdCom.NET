using ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToUiELementsListOfComplexUiElementsAsClass;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class UiElementSearchRelativeToUiElementsListOfComplexUiElementsAsClassTestPage : UiElementSearchBasePage
    {
        public override string RelativeUri => @"/TestPage_UiElementsSearchRelativeToUiElementsListOfComplexUiElementsAsClass";

        [Find(Use.IdContains, "page->uiElementsList(8921)_complexUiElementAsClass(8921)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_8921> Page_UiElementsListOfComplexUiElementsAsClass_8921;

        [Find(Use.IdContains, "page->uiElementsList(8e8c)_complexUiElementAsClass(8e8c)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_8e8c> Page_UiElementsListOfComplexUiElementsAsClass_8e8c;

        [Find(Use.IdContains, "page->uiElementsList(79c4)_complexUiElementAsClass(79c4)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_79c4> Page_UiElementsListOfComplexUiElementsAsClass_79c4;

        [Find(Use.IdContains, "page->uiElementsList(c0d6)_complexUiElementAsClass(c0d6)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_c0d6> Page_UiElementsListOfComplexUiElementsAsClass_c0d6;

        [Find(Use.IdContains, "page->uiElementsList(4596)_complexUiElementAsClass(4596)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_4596> Page_UiElementsListOfComplexUiElementsAsClass_4596;

        [Find(Use.IdContains, "page->uiElementsList(9240)_complexUiElementAsClass(9240)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_9240> Page_UiElementsListOfComplexUiElementsAsClass_9240;

        [Find(Use.IdContains, "page->uiElementsList(68b1)_complexUiElementAsClass(68b1)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_68b1> Page_UiElementsListOfComplexUiElementsAsClass_68b1;

        [Find(Use.IdContains, "page->uiElementsList(52fa)_complexUiElementAsClass(52fa)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_52fa> Page_UiElementsListOfComplexUiElementsAsClass_52fa;

        [Find(Use.IdContains, "page->uiElementsList(d413)_complexUiElementAsClass(d413)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_d413> Page_UiElementsListOfComplexUiElementsAsClass_d413;

        [Find(Use.IdContains, "page->uiElementsList(49e5)_complexUiElementAsClass(49e5)")]
        public IUiElementsList<Div_ComplexUiElementAsClass_49e5> Page_UiElementsListOfComplexUiElementsAsClass_49e5;
    }
}
