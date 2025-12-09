using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors
{
    public abstract class UiElementBehavior<T> : UiPageBehavior where T : IUiElement
    {
        private T? _uiElement;
        protected T UiElement => _uiElement ??= Parameters.Get<T>(SharedUiConstants.IUiElement);

        public UiElementBehavior(IBehaviorParameters parameters) : base(parameters)
        {
            Parameters.ParamsDictionary.Add(SharedUiConstants.IUiPage, UiElement.UiPage);
        }
    }
}
