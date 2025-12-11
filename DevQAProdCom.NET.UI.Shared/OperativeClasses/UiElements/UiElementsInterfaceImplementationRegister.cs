using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements
{
    public class UiElementsInterfaceImplementationRegister : IUiElementsInterfaceImplementationRegister
    {
        public Dictionary<Type, Type> UiElementTypes { get; set; } = new Dictionary<Type, Type>();
        public Type GetTypeOfIUiElementImplementation() => UiElementTypes.GetValueOrDefault(typeof(IUiElement)) ?? throw new ArgumentNullException(nameof(UiElementTypes));
        public Type GetTypeOfIUiElementsListImplementation() => UiElementTypes.GetValueOrDefault(typeof(IUiElementsList<>)) ?? throw new ArgumentNullException(nameof(UiElementTypes));

        public UiElementsInterfaceImplementationRegister(Type typeOfIUiElementImplementation, Type typeOfIUiElementsListImplementation, params KeyValuePair<Type, Type>[] uiElementsInterfaceImplementationEntries)
        {
            UiElementTypes.Add(typeof(IUiElement), typeOfIUiElementImplementation);
            UiElementTypes.Add(typeof(IUiElementsList<>), typeOfIUiElementsListImplementation);
            UiElementTypes.Upsert(uiElementsInterfaceImplementationEntries);
        }

        public Type GetUiElementImplementationType(Type type)
        {
            if (UiElementTypes.TryGetValue(@type, out Type? elementType))
                return elementType;
            else
                return type;
        }

        public UiElementsInterfaceImplementationRegister RegisterUiElementImplementationType<TInterface, TImplementation>()
            where TImplementation : TInterface
            where TInterface : IUiElement
        {
            UiElementTypes.Upsert(typeof(TInterface), typeof(TImplementation));
            return this;
        }
    }
}
