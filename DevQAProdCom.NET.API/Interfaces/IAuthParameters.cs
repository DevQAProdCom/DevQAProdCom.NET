using System.Net;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.API.Interfaces
{
    public interface IAuthParameters : IHaveHeterogeneousKeyValueData
    {
        NetworkCredential Credentials { get; set; }
    }
}
