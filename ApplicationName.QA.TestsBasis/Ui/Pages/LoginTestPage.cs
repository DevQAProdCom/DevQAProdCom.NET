using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.UiElements.OperativeClasses;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class LoginTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/LoginTestPage";

        [Find(Use.IdEquals, "username")]
        public InputText UserName;

        [Find(Use.ClassNameEquals, "password")]
        public InputText Password;

        [Find(Use.DataTestIdContains, "login")]
        public IUiElement LoginButton;
    }
}
