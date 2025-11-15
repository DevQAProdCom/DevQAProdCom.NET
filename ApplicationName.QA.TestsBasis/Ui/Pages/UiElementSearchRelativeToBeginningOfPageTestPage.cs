using ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToBeginningOfPage;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class UiElementSearchRelativeToBeginningOfPageTestPage : UiElementSearchBasePage
    {
        public override string RelativeUri => @"/TestPage_UiElementsSearchRelativeToBeginningOfPage";

        [Find(Use.IdEquals, "page->simpleUiElementAsInterface(e424)")]
        public IUiElement Page_SimpleUiElementAsInterface_e424;

        [Find(Use.IdEquals, "page->complexUiElementAsClass(4fe1)")]
        public Div_ComplexUiElementAsClass_4fe1 Page_ComplexUiElementAsClass_4fe1;

        [Find(Use.IdContains, "page->uiElementsList(36d9)_simpleUiElementAsInterface(36d9)")]
        public IUiElementsList<IUiElement> Page_UiElementsListOfSimpleUiElementsAsInterface_36d9;

        [Find(Use.IdContains, "page->uiElementsList(e125)_complexUiElementAsClass(e125)")]
        public IUiElementsList<IUiElement> Page_UiElementsListOfComplexUiElementsAsClass_e125;

        [Frame(Use.IdEquals, "page->frameSimpleUiElementAsInterface(c7bd)")]
        public IUiElement Page_FrameSimpleUiElementAsInterface_c7bd;

        [Frame(Use.IdContains, "page->uiElementsList(1fc6)_frameSimpleUiElementAsInterface(1fc6)")]
        public IUiElementsList<IUiElement> Page_UiElementsListOfFrameSimpleUiElementsAsInterface_1fc6;

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(07e0)")]
        public Frame_ComplexUiElementAsClass_07e0 Page_FrameComplexUiElementAsClass_07e0;

        [ShadowRootHost(Use.IdEquals, "page->shadowRootHostSimpleUiElementAsInterface(4d55)")]
        public IUiElement Page_ShadowRootHostSimpleUiElementAsInterface_4d55;

        [ShadowRootHost(Use.IdContains, "page->uiElementsList(bbd6)_shadowRootHostSimpleUiElementAsInterface(bbd6)")]
        public IUiElementsList<IUiElement> Page_UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_bbd6;

        [ShadowRootHost(Use.IdEquals, "page->shadowRootHostComplexUiElementAsClass(740c)")]
        public ShadowRootHost_ComplexUiElementAsClass_740c Page_ShadowRootHostComplexUiElementAsClass_740c;
    }
}
