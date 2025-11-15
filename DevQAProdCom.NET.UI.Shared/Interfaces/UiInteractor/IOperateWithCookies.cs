using DevQAProdCom.NET.UI.Shared.Constants;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IOperateWithCookies
    {
        public void SetCookie(string name, string value, string domain, string? path = SharedUiConstants.DefaultCookiePathValue);
        public void SetCookie(IUiInteractorCookie cookie);
        public void SetCookies(params IUiInteractorCookie[] cookies);

        public IUiInteractorCookie? GetCookie(string name);
        public List<IUiInteractorCookie> GetAllCookies();

        public void ClearCookies(params string[] names);
        public void ClearAllCookies();
    }
}
