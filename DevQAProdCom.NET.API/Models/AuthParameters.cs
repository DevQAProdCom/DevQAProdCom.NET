using System.Net;
using DevQAProdCom.NET.API.Interfaces;

namespace DevQAProdCom.NET.API.Models
{
    public class AuthParameters : IAuthParameters
    {
        public NetworkCredential Credentials { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public AuthParameters(NetworkCredential credentials = null, Dictionary<string, object> data = null)
        {
            Credentials = credentials;
            Data = data;
        }
    }
}
