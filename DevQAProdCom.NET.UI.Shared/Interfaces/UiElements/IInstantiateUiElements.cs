using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IInstantiateUiElements
    {
        public TUiElement InstantiateUiElement<TUiElement>(string name, Use method, string criteria, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement;
        public TUiElement InstantiateUiElement<TUiElement>(string name, List<IUiElementsFindInfo> findOptions, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(string name, Use method, string criteria, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(string name, List<IUiElementsFindInfo> findOptions, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement;
    }
}
