using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IFindUiElements
    {
        TUiElement Find<TUiElement>(string method, string criteria, string? name = null) where TUiElement : IUiElement;
        TUiElement Find<TUiElement>(Use method, string criteria, string? name = null) where TUiElement : IUiElement;
        TUiElement Find<TUiElement>(List<IUiElementsFindInfo> findOptions, string? name = null) where TUiElement : IUiElement;

        IUiElementsList<TUiElement> FindAll<TUiElement>(string method, string criteria, string? name = null) where TUiElement : IUiElement;
        IUiElementsList<TUiElement> FindAll<TUiElement>(Use method, string criteria, string? name = null) where TUiElement : IUiElement;
        IUiElementsList<TUiElement> FindAll<TUiElement>(List<IUiElementsFindInfo> findOptions, string? name = null) where TUiElement : IUiElement;
    }
}
