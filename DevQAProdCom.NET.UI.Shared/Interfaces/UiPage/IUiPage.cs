using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Other;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Traits.Mouse;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface IUiPage :
        IUiPageInfo,
        IFindUiElementsWithParentParameter,
        IFindUiElementsFromTheBeginningOfDom,
        IExecuteJavaScript,
        IHaveNativeObjects,
        IAddBehavior,

        IUiInteractionTraitKeysDown,
        IUiInteractionTraitKeysUp,
        IUiInteractionTraitPressKey,
        IUiInteractionTraitPressKeysSequentially,
        IUiInteractionTraitPressKeysSimultaneously,

        IUiPageTraitMouseMove,
        IUiPageTraitMouseMoveJs,
        IUiPageTraitMouseScroll,
        IUiPageTraitMouseScrollHorizontally,
        IUiPageTraitMouseScrollVertically,

        IUiInteractionTraitRefresh
    {
        public IUiInteractorTab UiTab { get; }

        public void GoTo(params KeyValuePair<string, string>[] placeholderValues);
        public void WaitForLoaded(Func<bool>? waitTillFunc = null, TimeSpan? timeout = null);
    }
}
