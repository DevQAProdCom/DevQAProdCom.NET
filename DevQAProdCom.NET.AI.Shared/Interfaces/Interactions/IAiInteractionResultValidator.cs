using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Interfaces.Interactions
{
    public interface IAiInteractionResultValidator
    {
        public IValidate Validate(IAiInteractionDataBank? interactionDataBank = null);
    }
}
