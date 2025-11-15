using ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeTo2FramesComplexUiElementsAsClass;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class UiElementSearchRelativeTo2FramesComplexUiElementsAsClassTestPage : UiElementSearchBasePage
    {
        public override string RelativeUri => @"/TestPage_UiElementsSearchRelativeTo2FramesComplexUiElementsAsClass";

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface
        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(e720)")]
        public Frame_ComplexUiElementAsClass_e720 Page_FrameComplexUiElementAsClass_e720;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Complex_UiElement_AsClass

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(ae97)")]
        public Frame_ComplexUiElementAsClass_ae97 Page_FrameComplexUiElementAsClass_ae97;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Complex_UiElement_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(d299)")]
        public Frame_ComplexUiElementAsClass_d299 Page_FrameComplexUiElementAsClass_d299;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(4f4d)")]
        public Frame_ComplexUiElementAsClass_4f4d Page_FrameComplexUiElementAsClass_4f4d;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        //Pairwise combination '2Frames -> 2Frames' is covered in scope of other test for combination 'Frame -> 2Frames'.
        //Search by 'Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface' inside solution.
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(78c2)")]
        public Frame_ComplexUiElementAsClass_78c2 Page_FrameComplexUiElementAsClass_78c2;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        //Pairwise combination '2Frames -> 2Frames' is covered in scope of other test for combination 'Frame -> 2Frames'.
        //Search by 'Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface' inside solution.
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(e8a7)")]
        public Frame_ComplexUiElementAsClass_e8a7 Page_FrameComplexUiElementAsClass_e8a7;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(3279)")]
        public Frame_ComplexUiElementAsClass_3279 Page_FrameComplexUiElementAsClass_3279;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(29f4)")]
        public Frame_ComplexUiElementAsClass_29f4 Page_FrameComplexUiElementAsClass_29f4;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
    }
}
