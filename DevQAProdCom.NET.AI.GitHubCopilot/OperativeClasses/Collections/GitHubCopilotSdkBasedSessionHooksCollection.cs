using System.Collections;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Collections
{
    public class GitHubCopilotSdkBasedSessionHooksCollection : IGitHubCopilotSdkBasedSessionHooksCollection
    {
        public string CollectionIdentifier { get; }

        protected List<IGitHubCopilotSdkBasedSessionHook> SessionHooks { get; set; } = new List<IGitHubCopilotSdkBasedSessionHook>();

        protected ILogger Logger;

        public GitHubCopilotSdkBasedSessionHooksCollection(ILogger logger, string? collectionIdentifier = null)
        {
            Logger = logger;
            CollectionIdentifier = collectionIdentifier ?? Guid.NewGuid().ToString();
        }

        public virtual IGitHubCopilotSdkBasedSessionHook GetByIdentifier(string identifier)
        {
            if (TryGetByIdentifierOrDefault(identifier, out var sessionHook))
            {
                return sessionHook!;
            }
            else
            {
                throw new KeyNotFoundException($"GitHub Copilot session hook with identifier '{identifier}' is not found in the collection.");
            }
        }

        public virtual bool TryGetByIdentifierOrDefault(string identifier, out IGitHubCopilotSdkBasedSessionHook? sessionHook)
        {
            sessionHook = SessionHooks.SingleOrDefault(x => x.Identifier == identifier);
            return sessionHook != null;
        }

        public virtual IGitHubCopilotSdkBasedSessionHook Add(IGitHubCopilotSdkBasedSessionHook sessionHook)
        {
            SessionHooks.Add(sessionHook);
            return sessionHook;
        }

        public IEnumerator<IGitHubCopilotSdkBasedSessionHook> GetEnumerator()
        {
            return SessionHooks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
