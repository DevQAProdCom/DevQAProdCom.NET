using DevQAProdCom.NET.Configurations.OperativeClasses;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace ApplicationName.QA.Global.Configurations
{
    public class GlobalConfigContainer : ConfigContainer
    {
        public GlobalConfigContainer(ILogger log) : base(log)
        {
        }
    }
}
