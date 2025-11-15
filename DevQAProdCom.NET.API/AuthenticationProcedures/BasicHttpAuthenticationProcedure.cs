using System.Net.Http.Headers;
using System.Text;
using DevQAProdCom.NET.API.Interfaces;
using DevQAProdCom.NET.API.Models;

namespace DevQAProdCom.NET.API.AuthenticationProcedures
{
    public class BasicHttpAuthenticationProcedure : IHttpAuthenticationProcedure
    {
        public IHttpAuthParameters GetHttpAuthenticationParameters(IAuthParameters authParameters)
        {
            if (string.IsNullOrEmpty(authParameters?.Credentials?.UserName) || string.IsNullOrEmpty(authParameters?.Credentials?.Password))
                throw new ArgumentException("UserName and Password should not be null or empty.");

            string authenticationString = authParameters.Credentials.UserName + ":" + authParameters.Credentials.Password;
            byte[] isoEncodedCredentials = Encoding.GetEncoding("ISO-8859-1").GetBytes(authenticationString);
            string base64EncodedCredentials = Convert.ToBase64String(isoEncodedCredentials);

            var authenticationHeaderValue = new AuthenticationHeaderValue("Basic", base64EncodedCredentials).ToString();
            var httpAuthParameters = new HttpAuthParameters() { Headers = new List<Header>() { new Header("Authorization", authenticationHeaderValue) } };

            return httpAuthParameters;
        }
    }
}
