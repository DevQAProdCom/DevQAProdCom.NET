namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface ISingleUiPageService : IUiPageService
    {
        public void GoToPage(params KeyValuePair<string, string>[] placeholderValues);
        public void WaitForLoad();
        public Uri GetPageUrl(params KeyValuePair<string, string>[] placeholderValues);
    }
}
