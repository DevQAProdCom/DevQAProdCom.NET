using ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToComplexUiElementAsClass;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class UiElementSearchRelativeToComplexUiElementAsClassTestPage : UiElementSearchBasePage
    {
        public override string RelativeUri => @"/TestPage_UiElementsSearchRelativeToComplexUiElementAsClass";

        #region Page -> Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        [Find(Use.IdEquals, "page->complexUiElementAsClass(86ea)")]
        public Div_ComplexUiElementAsClass_86ea Page_ComplexUiElementAsClass_86ea;

        #endregion Page -> Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        #region Page -> Complex_UiElement_AsClass -> Complex_UiElement_AsClass

        [Find(Use.IdEquals, "page->complexUiElementAsClass(0d51)")]
        public Div_ComplexUiElementAsClass_0d51 Page_ComplexUiElementAsClass_0d51;

        #endregion Page -> Complex_UiElement_AsClass -> Complex_UiElement_AsClass

        #region Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        [Find(Use.IdEquals, "page->complexUiElementAsClass(576e)")]
        public Div_ComplexUiElementAsClass_576e Page_ComplexUiElementAsClass_576e;

        #endregion Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        [Find(Use.IdEquals, "page->complexUiElementAsClass(44a5)")]
        public Div_ComplexUiElementAsClass_44a5 Page_ComplexUiElementAsClass_44a5;

        #endregion Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        [Find(Use.IdEquals, "page->complexUiElementAsClass(cd75)")]
        public Div_ComplexUiElementAsClass_cd75 Page_ComplexUiElementAsClass_cd75;

        #endregion Page -> Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        [Find(Use.IdEquals, "page->complexUiElementAsClass(030a)")]
        public Div_ComplexUiElementAsClass_030a Page_ComplexUiElementAsClass_030a;

        #endregion Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        [Find(Use.IdEquals, "page->complexUiElementAsClass(5905)")]
        public Div_ComplexUiElementAsClass_5905 Page_ComplexUiElementAsClass_5905;

        #endregion Page -> Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        [Find(Use.IdEquals, "page->complexUiElementAsClass(7275)")]
        public Div_ComplexUiElementAsClass_7275 Page_ComplexUiElementAsClass_7275;

        #endregion Page -> Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        [Find(Use.IdEquals, "page->complexUiElementAsClass(8939)")]
        public Div_ComplexUiElementAsClass_8939 Page_ComplexUiElementAsClass_8939;

        #endregion Page -> Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        [Find(Use.IdEquals, "page->complexUiElementAsClass(4f49)")]
        public Div_ComplexUiElementAsClass_4f49 Page_ComplexUiElementAsClass_4f49;

        #endregion Page -> Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
    }
}
