using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiInteractionResultValidator
    {
        public IValidate Validate(IAiInteractionDataBank? interactionDataBank = null);
    }
}
