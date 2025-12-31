using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class TestPage_UiElementBehaviors_General : BaseAppUiPage
    {
        public override string RelativeUri => @"/TestPage_UiElementBehaviors_General";

        [Find(Use.IdEquals, "uiElementBehavior_removeAttributeJs_style_input")]
        public IInputText UiElementBehaviorRemoveAttributeJsStyleInput;
    }
}
