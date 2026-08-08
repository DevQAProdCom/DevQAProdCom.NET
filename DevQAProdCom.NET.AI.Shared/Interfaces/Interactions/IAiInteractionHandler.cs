namespace DevQAProdCom.NET.AI.Shared.Interfaces.Interactions
{
    public interface IAiInteractionHandler
    {
        public void HandleEvent(string @event, IAiInteractionDataBank interactionDataBank);
        public void Finally();
    }
}
