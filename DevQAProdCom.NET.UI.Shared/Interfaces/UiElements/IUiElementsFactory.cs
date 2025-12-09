namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElementsFactory
    {
        public object CreateUiElement(Type @type, IUiElementInfo uiElementInfo, params KeyValuePair<string, object>[] nativeObjects);
        public object CreateUiElementsList(Type @type, IUiElementInfo uiElementInfo);
    }
}
