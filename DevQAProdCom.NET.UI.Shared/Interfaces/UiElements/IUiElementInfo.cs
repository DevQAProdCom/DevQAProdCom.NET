namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElementInfo
    {
        IUiElementInstantiationStageInfo InstantiationStage { get; }
        IUiElementFindStageInfo? FindStage { get; }
        public void ClearFindStageData();
    }
}
