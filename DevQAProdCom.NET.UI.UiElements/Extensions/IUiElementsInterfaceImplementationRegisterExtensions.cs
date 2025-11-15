using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;
using DevQAProdCom.NET.UI.UiElements.OperativeClasses;

namespace DevQAProdCom.NET.UI.UiElements.Extensions
{
    public static class IUiElementsInterfaceImplementationRegisterExtensions
    {
        public static IUiElementsInterfaceImplementationRegister AddTypicalUiElementsPresetInterfaceImplementationEntries(this IUiElementsInterfaceImplementationRegister register)
        {
            register.RegisterUiElementImplementationType<IInputFile, InputFile>()
                .RegisterUiElementImplementationType<IInputText, InputText>()
                .RegisterUiElementImplementationType<IButton, Button>()
                .RegisterUiElementImplementationType<ILabel, Label>();

            return register;
        }
    }
}
