using DevQAProdCom.NET.Global.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace DevQAProdCom.NET.API.Builders
{
    public class AppUriBuilder
    {
        private UriBuilder _uriBuilder { get; }
        private Dictionary<string, string> _pathParameters { get; set; }

        public AppUriBuilder()
        {
            _uriBuilder = new UriBuilder();
        }

        public AppUriBuilder(string uri)
        {
            if (uri != null)
                _uriBuilder = new UriBuilder(uri);
        }

        public AppUriBuilder(Uri uri)
        {
            if (uri != null)
                _uriBuilder = new UriBuilder(uri);
        }

        public AppUriBuilder WithScheme(string scheme)
        {
            _uriBuilder.Scheme = scheme;
            return this;
        }

        public AppUriBuilder WithHost(string host)
        {
            _uriBuilder.Host = host;
            return this;
        }

        public AppUriBuilder WithPort(int port)
        {
            _uriBuilder.Port = port;
            return this;
        }

        public AppUriBuilder WithPath(string path)
        {
            _uriBuilder.Path = path;
            return this;
        }

        public AppUriBuilder WithPathParameter(string key, string value)
        {
            _pathParameters ??= new Dictionary<string, string>();
            _pathParameters.Upsert(key, value);

            return this;
        }

        public AppUriBuilder WithQueryStringKeyValues(string key, StringValues stringValues, bool shouldBeReplaced = false)
        {
            var queryStringKeysValues = QueryHelpers.ParseNullableQuery(_uriBuilder.Query);

            queryStringKeysValues ??= new Dictionary<string, StringValues>();

            if (!queryStringKeysValues.ContainsKey(key))
                queryStringKeysValues.Add(key, stringValues);
            else
            {
                if (shouldBeReplaced)
                    queryStringKeysValues[key] = stringValues;
                else
                    queryStringKeysValues[key] = queryStringKeysValues[key].Union(stringValues).Distinct().ToArray();
            }

            _uriBuilder.Query = string.Empty;

            foreach (var queryStringKeyValues in queryStringKeysValues)
                foreach (var value in queryStringKeyValues.Value)
                    _uriBuilder.Query = QueryHelpers.AddQueryString(_uriBuilder.Query, queryStringKeyValues.Key, value);

            return this;
        }

        public AppUriBuilder WithQueryStringKeysValues(Dictionary<string, StringValues> keysValues, bool shouldBeReplaced = false)
        {
            if (keysValues?.Count() > 0)
                foreach (var keyValues in keysValues)
                    WithQueryStringKeyValues(keyValues.Key, keyValues.Value, shouldBeReplaced);

            return this;
        }

        public Uri Build()
        {
            _uriBuilder.Path = ReplaceAlternativePathParametersWithValues(_uriBuilder.Path, _pathParameters);
            return _uriBuilder.Uri;
        }

        private string ReplaceAlternativePathParametersWithValues(string path, Dictionary<string, string> pathParameters)
        {
            if (!string.IsNullOrEmpty(path) && pathParameters?.Count() > 0)
            {
                var pathSections = path.Split('/').ToList();

                foreach (var pathParameter in pathParameters)
                {
                    var parameterPlaceholderToSubstitute = string.Concat("%7B", pathParameter.Key, "%7D");
                    var indexOfParameterPlaceholder = pathSections.IndexOf(parameterPlaceholderToSubstitute);

                    if (indexOfParameterPlaceholder > -1)
                        pathSections[indexOfParameterPlaceholder] = pathParameter.Value;
                }

                return string.Join('/', pathSections.Where(x => x != null && !x.StartsWith("%7B") && !x.EndsWith("%7D")));
            }

            return path;
        }
    }
}
