namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractorCookie
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string? Domain { get; }
        public string? Path { get; }
        public DateTime? Expires { get; set; }

        /// <summary>
        /// Is added to support Playwright Cookie Expires property which is in seconds since Unix epoch
        /// </summary>
        public float? ExpiresInSeconds { get; set; }
        public bool? HttpOnly { get; set; }
        public bool? Secure { get; set; }
        public string? SameSite { get; set; }
    }
}
