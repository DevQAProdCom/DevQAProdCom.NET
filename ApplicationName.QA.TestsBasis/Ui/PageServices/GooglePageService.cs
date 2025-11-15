using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class GooglePageService : SingleUiPageService<GooglePage>
    {
        public GooglePageService(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultTab) : base(uiInteractor, tabName)
        {
        }

        public void Search(string searchText)
        {
            //Page.SearchTextArea.AddText(searchText);
        }

        public override void WaitForLoad() { }

        public override Uri GetPageUrl(params KeyValuePair<string, string>[] placeholderValues) //TODO Page Url or Uri - add Uri Builder
        {
            return new Uri("https://www.google.com/");
        }
    }
}
