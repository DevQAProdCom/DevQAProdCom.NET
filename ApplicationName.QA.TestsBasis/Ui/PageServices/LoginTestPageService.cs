using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;
using ApplicationName.QA.TestsBasis.Ui.Pages;

namespace ApplicationName.QA.TestsBasis.Ui.PageServices
{
    public class LoginTestPageService : SingleUiPageService<LoginTestPage>
    {
        public LoginTestPageService(IUiInteractor uiInteractor) : base(uiInteractor)
        {
        }

        public void Login(string username, string password)
        {
            GoToPage();
            _page.UserName.SetText(username);
            _page.Password.SetText(password);
            _page.LoginButton.MouseClick();

            Wait.Create().Until(() =>
            {
                var url = _uiTab.GetTabUrl();
                return url.EndsWith("HomeTestPage");
            });
        }
    }
}
