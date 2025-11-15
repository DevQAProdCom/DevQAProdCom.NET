using DevQAProdCom.NET.Logging.Shared.Constans;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.Logging.Models
{
    public class UiElementsSearchLoggingModel
    {
        public string LogArea_L0 { get; } = SharedLoggingConstants.Framework;
        public string LogSubArea_L1 { get; } = SharedLoggingConstants.UiElementsSearch;
        public string LogSubArea_L2 { get; }
        public string UiElementFullName { get; }

        public UiElementsSearchLoggingModel(IUiElementInfo uiElementInfo, string logSubArea_L2)
        {
            LogSubArea_L2 = logSubArea_L2;
            UiElementFullName = uiElementInfo.InstantiationStage.FullName;
        }
    }
}
