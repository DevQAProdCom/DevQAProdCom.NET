using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Shared.Extensions.UiElement
{
    public static class UiElementKeyboardExtensions
    {
        public static void PressEnter(this IUiElement uiElement)
        {
            uiElement.AddBehavior<IUiElementBehaviorPressKeysSimultaneously>().PressKeysSimultaneously(Key.Enter);
        }

        public static void PressArrowDown(this IUiElement uiElement)
        {
            uiElement.AddBehavior<IUiElementBehaviorPressKeysSimultaneously>().PressKeysSimultaneously(Key.ArrowDown);
        }

        public static void PressArrowLeft(this IUiElement uiElement)
        {
            uiElement.AddBehavior<IUiElementBehaviorPressKeysSimultaneously>().PressKeysSimultaneously(Key.ArrowLeft);
        }

        public static void PressEsc(this IUiElement uiElement)
        {
            uiElement.AddBehavior<IUiElementBehaviorPressKeysSimultaneously>().PressKeysSimultaneously(Key.Escape);
        }

        public static void PressTab(this IUiElement uiElement)
        {
            uiElement.AddBehavior<IUiElementBehaviorPressKeysSimultaneously>().PressKeysSimultaneously(Key.Tab);
        }
    }
}
