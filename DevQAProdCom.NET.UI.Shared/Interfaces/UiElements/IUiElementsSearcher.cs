using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElementsSearcher
    {
        TUiElement FindUiElement<TUiElement>(string name, Use method, string criteria) where TUiElement : IUiElement;
        TUiElement FindUiElement<TUiElement>(string name, List<IUiElementsFindInfo> findOptions) where TUiElement : IUiElement;

        IUiElementsList<TUiElement> FindUiElements<TUiElement>(string name, Use method, string criteria) where TUiElement : IUiElement;
        IUiElementsList<TUiElement> FindUiElements<TUiElement>(string name, List<IUiElementsFindInfo> findOptions) where TUiElement : IUiElement;
    }
}
