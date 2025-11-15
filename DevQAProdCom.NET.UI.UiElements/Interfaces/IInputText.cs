using DevQAProdCom.NET.UI.Shared.Interfaces.Traits.Text;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.UiElements.Interfaces
{
    public interface IInputText : IParentUiElement, IGetTextTrait, ISetTextTrait, IAppendTextTrait, IClearTextTrait
    {
    }
}
