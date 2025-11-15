using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class GooglePage : UiPage
    {
        public override string BaseUri => "https://google.com/";

        [Find(Use.XPath, "//textarea[@type='search']")]
        public IUiElement SearchTextArea;
    }
}
