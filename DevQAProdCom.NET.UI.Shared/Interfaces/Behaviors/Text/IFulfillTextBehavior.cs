using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Traits.Text;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Text
{
    public interface IFulfillTextBehavior : IBehavior, ITypeTextTrait, ISetTextTrait, IAppendTextTrait, ISetTextContentJsTrait, ISetInnerHtmlJsTrait, ICopyPasteTextTrait
    {
    }
}
