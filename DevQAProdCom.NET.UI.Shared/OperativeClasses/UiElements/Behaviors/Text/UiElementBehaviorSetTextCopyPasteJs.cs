using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class UiElementBehaviorSetTextCopyPasteJs(IBehaviorParameters parameters) : BaseUiElementBehaviorTextJs(parameters), IUiElementBehaviorSetTextCopyPasteJs
    {
        public void SetTextCopyPasteJs(string text)
        {
            UiElement.MouseHover();
            UiElement.ClickJs();
            UiElement.AddBehavior<IUiElementBehaviorClearText>().ClearText();
            UiElement.AddBehavior<IUiElementBehaviorCopyPasteTextJs>().CopyPasteTextJs(text);
        }
    }
}
