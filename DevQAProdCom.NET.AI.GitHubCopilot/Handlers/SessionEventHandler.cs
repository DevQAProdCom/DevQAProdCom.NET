using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Handlers
{
    public abstract class SessionEventHandler : IAiInteractionHandler
    {
        public abstract void HandleEvent(string @event, IAiInteractionDataBank interactionDataBank);
        public virtual void Finally() { }

        protected ILogger Logger { get; }

        public SessionEventHandler(ILogger logger)
        {
            Logger = logger;
        }

        protected T? GetEvent<T>(string @event, string type) where T : class
        {
            var normalizedEvent = @event.Replace("\"$type\":", "\"type\":"); //To avoid deserialization issues with the $type property in the event JSON

            if (normalizedEvent.Contains($"\"type\":\"{type}\""))
            {
                return normalizedEvent.FromJson<T>();
            }

            return null;
        }
    }
}
