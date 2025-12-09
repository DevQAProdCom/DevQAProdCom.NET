using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IFindUiElementsWithParentParameter
    {
        public TUiElement Find<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
        public TUiElement Find<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
        public TUiElement Find<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;

        public IUiElementsList<TUiElement> FindAll<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> FindAll<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> FindAll<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
    }
}
