using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.Logging.Models
{
    public class FindOptionLoggingModel
    {
        public string Method { get; }
        public string Criteria { get; }

        public FindOptionLoggingModel(IFindOption findOption)
        {
            Method  = findOption.Method;
            Criteria = findOption.Criteria;
        }
    }
}
