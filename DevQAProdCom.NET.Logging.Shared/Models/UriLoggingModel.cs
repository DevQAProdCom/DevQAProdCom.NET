namespace DevQAProdCom.NET.Logging.Shared.Models
{
    public class UriLoggingModel
    {
        public string FullUri { get; }
        public string Scheme { get; }
        public string Host { get; }
        public int Port { get; }
        public string Path { get; }
        public string Query { get; }
        public string Fragment { get; }
        public string UserInfo { get; }

        public UriLoggingModel(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            FullUri = uri.ToString();
            Scheme = uri.Scheme;
            Host = uri.Host;
            Port = uri.IsDefaultPort ? -1 : uri.Port;
            Path = uri.AbsolutePath;
            Query = uri.Query;
            Fragment = uri.Fragment;
            UserInfo = uri.UserInfo;
        }
    }
}
