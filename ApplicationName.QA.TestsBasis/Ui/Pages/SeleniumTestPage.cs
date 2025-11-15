using ApplicationName.QA.TestsBasis.Ui.Components.SeleniumTestPage;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class SeleniumTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/TestPage_Selenium";

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        [Frame(Use.IdContains, "frame")]
        public IUiElementsList<FrameComplexUiElementAsClass_9ac4> Page_UiElementsListOfFrameComplexUiElementsAsClass_9ac4;

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
    }
}
