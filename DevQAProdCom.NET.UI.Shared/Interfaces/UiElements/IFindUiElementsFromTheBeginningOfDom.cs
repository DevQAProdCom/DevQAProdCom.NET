namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IFindUiElementsFromTheBeginningOfDom
    {
        public TUiElement Find<TUiElement>(string? name = null) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> FindAll<TUiElement>(string? name = null) where TUiElement : IUiElement;
    }
}
