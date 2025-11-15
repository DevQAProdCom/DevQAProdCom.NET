using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components
{
    public class TestListItemA : UiElement
    {
        [Find(Use.NameEquals, "div-1")]
        public Div1 DivA;
    }
}
