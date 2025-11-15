using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.OperativeClasses;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public abstract class UiElementBehavior<T> : BaseBehavior where T : IUiElement
    {
        protected T UiElement { get; }

        public UiElementBehavior(IBehaviorParameters parameters) : base(parameters)
        {
            UiElement = parameters.Get<T>(SharedUiConstants.IUiElement);
        }
    }
}
