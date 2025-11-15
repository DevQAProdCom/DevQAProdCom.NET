using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search
{
    public interface IFindParametersWithSearchResult : IFindParameters
    {
        public FindState FindState { get; }
        public Uri? UriBeforeSearch { get; set; }
        public Uri? UriAfterSearch { get; set; }
        public List<IFindParametersWithSearchResult>? FindChain { get; set; }
        public int? TotalAmountOfElementsFound { get; set; }
        public Exception? Exception { get; set; }
    }
}
