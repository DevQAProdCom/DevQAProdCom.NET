using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class UiElementSearchBasePage : BaseAppUiPage
    {
        [Find(Use.IdEquals, "page->topLevelSimpleUiElementAsInterface")]
        public IUiElement Page_TopLevelSimpleUiElementAsInterface;
    }
}
