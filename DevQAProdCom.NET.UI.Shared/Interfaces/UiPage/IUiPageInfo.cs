namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface IUiPageInfo
    {
        public string ApplicationName { get; set; }
        public string PageName { get; set; }
        public string BaseUri { get; set; }
        public string RelativeUri { get; set; }
        public string GetActualUriAsString();
        public Uri GetActualUri();
        public string GetDefinedUriAsString(params KeyValuePair<string, string>[] placeholderValues);
        public Uri GetDefinedUri(params KeyValuePair<string, string>[] placeholderValues);
        public bool IsOpened(params KeyValuePair<string, string>[] placeholderValues);
        public bool IsOpenedWhenUriStartsWith(string? uriStartWith = null, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);
        public bool IsOpenedWhenUriEndsWith(string? uriEndsWith = null, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);
        public bool IsOpenedWhenUriEquals(string? uriEquals = null, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);
    }
}
