using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public class UiInteractorCookie : IUiInteractorCookie, IEquatable<UiInteractorCookie>
    {
        public string Name { get; set; } = default!;
        public string Value { get; set; } = default!;
        public string? Domain { get; }
        public string? Path { get; }
        public DateTime? Expires { get; set; }
        public bool? HttpOnly { get; set; }
        public bool? Secure { get; set; }
        public string? SameSite { get; set; }

        public UiInteractorCookie(string name, string value, string domain, string? path = "/")
        {
            Name = name;
            Value = value;

            if (string.IsNullOrEmpty(path))
                Path = "/";
            else
                Path = path;

            Domain = domain;
        }

        public UiInteractorCookie(string name, string value, Uri uri) : this(name, value, uri.Host, uri.AbsolutePath)
        {
        }

        // Override Equals to compare properties
        public override bool Equals(object? obj)
        {
            if (obj is UiInteractorCookie other)
            {
                return Equals(other);
            }
            return false;
        }

        public bool Equals(UiInteractorCookie? other)
        {
            if (other is null)
            {
                return false;
            }

            // Compare the relevant properties
            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(Value, other.Value, StringComparison.Ordinal) &&
                   string.Equals(Domain, other.Domain, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(Path, other.Path, StringComparison.Ordinal) &&
                   Nullable.Equals(Expires, other.Expires) &&
                   HttpOnly == other.HttpOnly &&
                   Secure == other.Secure &&
                   string.Equals(SameSite, other.SameSite, StringComparison.OrdinalIgnoreCase);
        }

        // Override GetHashCode to produce the same hash for equal objects
        public override int GetHashCode()
        {
            return HashCode.Combine(
                Name?.ToLowerInvariant(),
                Value,
                Domain?.ToLowerInvariant(),
                Path,
                Expires,
                HttpOnly,
                Secure,
                SameSite?.ToLowerInvariant()
            );
        }
    }
}
