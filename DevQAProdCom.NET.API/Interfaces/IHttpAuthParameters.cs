using DevQAProdCom.NET.API.Models;
using Microsoft.Extensions.Primitives;

namespace DevQAProdCom.NET.API.Interfaces
{
    public interface IHttpAuthParameters
    {
        public List<Header> Headers { get; set; }
        public Cookies Cookies { get; set; }
        public Dictionary<string, StringValues> QueryStringKeysValues { get; set; }
        public void ClearHttpAuthParameters();
    }
}
