using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;

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

        protected T? GetEvent<T>(string @event) where T : class
        {
            var normalizedEvent = @event.Replace("\"$type\":", "\"type\":"); //To avoid deserialization issues with the $type property in the event JSON
            var sessionEvent = normalizedEvent.FromJson<SessionEvent>()!;

            switch (sessionEvent.Type)
            {
                case Const.SessionEvents.ASSISTANT_MESSAGE when typeof(T) == typeof(AssistantMessageEvent):
                    {
                        var assistantMessageEvent = normalizedEvent.FromJson<AssistantMessageEvent>();
                        return assistantMessageEvent as T;
                    }

                default:
                    return null;
            }
        }
    }
}
