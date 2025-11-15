using ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToComplexUiElementAsClass;
using ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToFrameComplexUiElementAsClass;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class UiElementSearchRelativeToFrameComplexUiElementAsClassTestPage : UiElementSearchBasePage
    {
        public override string RelativeUri => @"/TestPage_UiElementsSearchRelativeToFrameComplexUiElementAsClass";

        #region Page -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(965d)")]
        public Frame_ComplexUiElementAsClass_965d Page_FrameComplexUiElementAsClass_965d;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(c39d)")]
        public Frame_ComplexUiElementAsClass_c39d Page_FrameComplexUiElementAsClass_c39d;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(7c15)")]
        public Frame_ComplexUiElementAsClass_7c15 Page_FrameComplexUiElementAsClass_7c15;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(b0fc)")]
        public Frame_ComplexUiElementAsClass_b0fc Page_FrameComplexUiElementAsClass_b0fc;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(b152)")]
        public Frame_ComplexUiElementAsClass_b152 Page_FrameComplexUiElementAsClass_b152;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(50c2)")]
        public Frame_ComplexUiElementAsClass_50c2 Page_FrameComplexUiElementAsClass_50c2;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(e77c)")]
        public Frame_ComplexUiElementAsClass_e77c Page_FrameComplexUiElementAsClass_e77c;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(d481)")]
        public Frame_ComplexUiElementAsClass_d481 Page_FrameComplexUiElementAsClass_d481;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(b37b)")]
        public Frame_ComplexUiElementAsClass_b37b Page_FrameComplexUiElementAsClass_b37b;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        [Frame(Use.IdEquals, "page->frameComplexUiElementAsClass(8acd)")]
        public Frame_ComplexUiElementAsClass_8acd Page_FrameComplexUiElementAsClass_8acd;

        #endregion Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
    }
}
