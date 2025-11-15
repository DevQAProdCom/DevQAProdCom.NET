namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface IUiPageInfo
    {
        public string ApplicationName { get; }
        public string PageName { get; }
        public string BaseUri { get; }
        public string RelativeUri { get; }
    }
}
