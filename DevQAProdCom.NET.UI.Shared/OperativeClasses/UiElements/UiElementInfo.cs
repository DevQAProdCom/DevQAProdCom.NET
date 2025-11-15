using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements
{
    public class UiElementInfo : IUiElementInfo
    {
        public IUiElementInstantiationStageInfo InstantiationStage { get; internal set; }
        public IUiElementFindStageInfo? FindStage { get; internal set; }

        public UiElementInfo(IUiElementInstantiationStageInfo instantiationStageInfo)
        {
            InstantiationStage = instantiationStageInfo;
        }

        public UiElementInfo(IUiElementInstantiationStageInfo instantiationStageInfo, IUiElementFindStageInfo findStageInfo) : this(instantiationStageInfo)
        {
            FindStage = findStageInfo;
        }

        public void ClearFindStageData()
        {
            FindStage = null;
        }
    }
}
