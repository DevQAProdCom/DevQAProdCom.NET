using System.Net;
using DevQAProdCom.NET.API.Interfaces;
using DevQAProdCom.NET.API.Models;
using DevQAProdCom.NET.Global.Extensions;

namespace DevQAProdCom.NET.API.Builders
{
    public class HttpRequestMessageBuilder
    {
        private HttpRequestMessage _message { get; }
        private Cookies _cookies { get; set; }
        private List<Header> _headers { get; set; }

        public HttpRequestMessageBuilder()
        {
            _message = new HttpRequestMessage();
        }

        public HttpRequestMessageBuilder(HttpEndpoint endpoint) : this()
        {
            WithEndpoint(endpoint);
        }

        public HttpRequestMessageBuilder(HttpRequestMessage message)
        {
            if (message.Headers?.Count() > 0)
            {
                _headers = new List<Header>();

                foreach (var header in message.Headers)
                {
                    if (header.Key != "Cookie")
                        _headers.Add(new Header(header.Key, header.Value.ToArray()));
                    else
                        _cookies = new Cookies(header.Value.ElementAt(0));
                }
            }

            _message = message.CloneJson<HttpRequestMessage>();
            _message.Headers.Clear();
        }

        public HttpRequestMessageBuilder WithMethod(HttpMethod method)
        {
            _message.Method = method;
            return this;
        }

        public HttpRequestMessageBuilder WithRequestUri(Uri? uri)
        {
            _message.RequestUri = uri;
            return this;
        }

        public HttpRequestMessageBuilder WithHeaders(params Header[] headers)
        {
            if (headers?.Length > 0)
            {
                _headers ??= new List<Header>();
                _headers.AddRange(headers);
            }

            return this;
        }

        public HttpRequestMessageBuilder WithHeadersAddOrReplace(params Header[] headers)
        {
            if (headers?.Length > 0)
            {
                _headers ??= new List<Header>();

                var namesOfHeadersForRemoval = headers.Select(x => x.Name);
                _headers.RemoveAll(x => namesOfHeadersForRemoval.Contains(x.Name));

                _headers.AddRange(headers);
            }

            return this;
        }

        public HttpRequestMessageBuilder WithCookies(params Cookie[] cookies)
        {
            if (cookies?.Length > 0)
            {
                _cookies ??= new Cookies();
                _cookies.AddCookies(cookies);
            }

            return this;
        }

        public HttpRequestMessageBuilder WithCookiesAddOrReplace(params Cookie[] cookies)
        {
            if (cookies?.Length > 0)
            {
                _cookies ??= new Cookies();
                _cookies.AddOrReplaceCookies(cookies);
            }

            return this;
        }

        public HttpRequestMessageBuilder WithOptions(HttpRequestOptions options)
        {
            if (options?.Count() > 0)
            {
                foreach (var option in options)
                    _message.Options.Set(new HttpRequestOptionsKey<object?>(option.Key), option.Value);
            }

            return this;
        }

        public HttpRequestMessageBuilder WithEndpoint(HttpEndpoint endpoint)
        {
            if (endpoint != null)
            {
                WithMethod(endpoint.Method)
                    .WithRequestUri(endpoint.RequestUri.Build())
                    .WithHeaders(endpoint.RequestHeaders?.ToArray())
                    .WithCookies(endpoint.RequestCookies?.Items?.ToArray())
                    .WithOptions(endpoint.RequestOptions);
            }

            return this;
        }

        public HttpRequestMessageBuilder WithHttpContent(HttpContent content)
        {
            _message.Content = content;
            return this;
        }

        public HttpRequestMessageBuilder WithAuthentication(IHttpAuthenticationProcedure authProcedure, IAuthParameters authParameters)
        {
            if (authProcedure != null && authParameters != null)
            {
                var httpAuthParameters = authProcedure.GetHttpAuthenticationParameters(authParameters);
                AddHttpAuthParameters(httpAuthParameters);
            }

            return this;
        }

        private void AddHttpAuthParameters(IHttpAuthParameters httpAuthParameters)
        {
            WithHeaders(httpAuthParameters?.Headers?.ToArray());
            WithCookies(httpAuthParameters?.Cookies?.Items?.ToArray());

            var uriWithAuthParameters = new AppUriBuilder(_message?.RequestUri)
                .WithQueryStringKeysValues(httpAuthParameters?.QueryStringKeysValues, shouldBeReplaced: true)
                .Build();
            WithRequestUri(uriWithAuthParameters);
        }

        private void ApplyHeaders()
        {
            if (_headers?.Count > 0)
                foreach (var header in _headers)
                    _message.Headers.Add(header.Name, header.Values);
        }

        private void ApplyCookies()
        {
            if (_cookies?.Items?.Count > 0)
                _message.Headers.Add("Cookie", _cookies.GetCookiesString());
        }

        public HttpRequestMessage Build()
        {
            ApplyHeaders();
            ApplyCookies();
            return _message;
        }
    }
}
