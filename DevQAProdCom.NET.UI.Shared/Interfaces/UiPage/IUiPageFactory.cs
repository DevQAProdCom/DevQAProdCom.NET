using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    public interface IUiPageFactory : IUiElementsFactory, IUiElementsInstantiator
    {
        public IUiPage CreatePage<TUiPage>() where TUiPage : IUiPage;
    }
}
