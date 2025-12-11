using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSimpleUiElement(this Type type) => type == typeof(IUiElement);
        public static bool IsComplexUiElementAsClass(this Type type) => typeof(IUiElement).IsAssignableFrom(type) && ((type.IsClass && !type.IsAbstract) || type.IsInterface);
        public static bool IsUiElement(this Type type) => type.IsSimpleUiElement() || type.IsComplexUiElementAsClass();

        public static bool IsListOfSimpleUiElements(this Type type) => type == typeof(IUiElementsList<IUiElement>);
        public static bool IsListOfComplexUiElements(this Type type) => type.IsAssignableToType(typeof(IUiElementsList<IUiElement>));
        public static bool IsUiElementsList(this Type type) => type.IsListOfSimpleUiElements() || type.IsListOfComplexUiElements();
    }
}
