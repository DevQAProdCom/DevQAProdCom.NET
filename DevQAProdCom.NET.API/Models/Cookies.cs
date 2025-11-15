using System.Net;

namespace DevQAProdCom.NET.API.Models
{
    public class Cookies
    {
        public List<Cookie> Items { get; set; }

        public Cookies()
        {
            Items = new List<Cookie>();
        }

        public Cookies(string name, string value)
        {
            AddCookie(name, value);
        }

        public Cookies(params Cookie[] cookies)
        {
            AddCookies(cookies);
        }

        public Cookies(string cookiesString)
        {
            if (!string.IsNullOrEmpty(cookiesString))
            {
                var cookies = cookiesString.Split(";").Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x));

                if (cookies.Count() > 0)
                {
                    Items ??= new List<Cookie>();

                    foreach (var cookie in cookies)
                    {
                        if (!string.IsNullOrEmpty(cookie))
                        {
                            int indexOf = cookie.IndexOf("=");

                            if (indexOf > -1)
                            {
                                var name = cookie.Substring(0, indexOf);
                                var value = cookie.Substring(indexOf + 1);
                                AddCookie(name, value);
                            }
                        }
                    }
                }
            }
        }

        public void AddCookie(string name, string value)
        {
            Items ??= new List<Cookie>();

            if (!string.IsNullOrEmpty(name))
                Items.Add(new Cookie(name, value));
        }

        public void AddCookies(params Cookie[] cookies)
        {
            Items ??= new List<Cookie>();

            if (cookies?.Length > 0)
                Items.AddRange(cookies);
        }

        public void AddOrReplaceCookie(string name, string value)
        {
            Items ??= new List<Cookie>();

            if (!string.IsNullOrEmpty(name))
            {
                RemoveCookies(name);
                Items.Add(new Cookie(name, value));
            }
        }

        public void AddOrReplaceCookies(params Cookie[] cookies)
        {
            Items ??= new List<Cookie>();

            if (cookies?.Length > 0)
            {
                RemoveCookies(cookies.Select(x => x.Name).ToArray());
                Items.AddRange(cookies);
            }
        }

        public void RemoveCookies(params string[] names)
        {
            if (names?.Length > 0)
                Items?.RemoveAll(x => names.Contains(x.Name));
        }

        public void Clear()
        {
            Items?.Clear();
        }

        public string GetCookiesString()
        {
            if (Items?.Count > 0)
                return Items?.Select(x => string.Concat(x.Name, "=", x.Value, ";"))
                    ?.Aggregate((x, y) => x + y);

            return null;
        }
    }
}
