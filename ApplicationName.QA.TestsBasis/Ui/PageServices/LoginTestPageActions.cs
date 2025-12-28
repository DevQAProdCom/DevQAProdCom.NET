using ApplicationName.QA.TestsBasis.Ui.Pages;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class LoginTestPageActions : SingleUiPageActions<LoginTestPage>
    {
        public LoginTestPageActions(IUiInteractor uiInteractor) : base(uiInteractor)
        {
        }

        public void Login(string username, string password)
        {
            GoToPage();
            Page.UserName.SetText(username);
            Page.Password.SetText(password);
            Page.LoginButton.Click();

            Wait.Create().Until(() =>
            {
                var url = UiTab.GetTabUriAsString();
                return url.EndsWith("HomeTestPage");
            });
        }
    }
}
