namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractorCookie
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string? Domain { get; }
        public string? Path { get; }
        public DateTime? Expires { get; set; }
        public bool? HttpOnly { get; set; }
        public bool? Secure { get; set; }
        public string? SameSite { get; set; }
    }
}
