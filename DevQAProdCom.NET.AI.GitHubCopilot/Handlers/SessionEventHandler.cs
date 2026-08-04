using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces;
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
            var sessionEvent = @event.FromJson<SessionEvent>()!;
            switch (sessionEvent.Type)
            {
                case Const.SessionEvents.ASSISTANT_MESSAGE when typeof(T) == typeof(AssistantMessageEvent):
                    return @event.FromJson<AssistantMessageEvent>() as T;

                default:
                    return null;
            }
        }
    }
}
