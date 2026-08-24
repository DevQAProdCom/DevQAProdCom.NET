using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Factories;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class BaseTest
    {


        protected ILogger Log = DependencyInjection.DiContainer.Instance.Log;
    }
}
