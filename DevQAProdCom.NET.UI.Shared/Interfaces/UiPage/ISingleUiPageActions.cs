namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface ISingleUiPageActions : IUiPageActions
    {
        public void GoToPage(params KeyValuePair<string, string>[] placeholderValues);
        public void WaitForLoad();
        public Uri GetPageUrl(params KeyValuePair<string, string>[] placeholderValues);
    }
}
