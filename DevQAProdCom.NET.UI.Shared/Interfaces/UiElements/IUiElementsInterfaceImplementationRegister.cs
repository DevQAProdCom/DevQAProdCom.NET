using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElementsInterfaceImplementationRegister
    {
        public Type GetUiElementImplementationType(Type type);
        public Type GetTypeOfIUiElementImplementation();
        public Type GetTypeOfIUiElementsListImplementation();

        public UiElementsInterfaceImplementationRegister RegisterUiElementImplementationType<TInterface, TImplementation>()
            where TImplementation : TInterface
            where TInterface : IParentUiElement;
    }
}
