using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements
{
    public class UiElementFindStageInfo : IUiElementFindStageInfo
    {
        public IFindParametersWithSearchResult FindParametersWithSearchResult { get; internal set; }

        public UiElementFindStageInfo(IFindParametersWithSearchResult findParametersWithSearchResult)
        {
            FindParametersWithSearchResult = findParametersWithSearchResult;
        }
    }
}
