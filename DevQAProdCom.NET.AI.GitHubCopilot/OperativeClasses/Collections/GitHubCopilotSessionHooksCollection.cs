using System.Collections;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Collections
{
    public class GitHubCopilotSessionHooksCollection : IGitHubCopilotSessionHooksCollection
    {
        public string CollectionIdentifier { get; }

        protected List<IGitHubCopilotSessionHook> SessionHooks { get; set; } = new List<IGitHubCopilotSessionHook>();

        protected ILogger Logger;

        public GitHubCopilotSessionHooksCollection(ILogger logger, bool initializeFromDefaultLocations = false, string? collectionIdentifier = null)
        {
            Logger = logger;
            CollectionIdentifier = collectionIdentifier ?? Guid.NewGuid().ToString();

            if (initializeFromDefaultLocations)
            {
                InitializeCollectionFromDefaultLocations();
            }
        }

        public virtual IGitHubCopilotSessionHook GetByIdentifier(string identifier)
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

        public virtual bool TryGetByIdentifierOrDefault(string identifier, out IGitHubCopilotSessionHook? sessionHook)
        {
            sessionHook = SessionHooks.SingleOrDefault(x => x.Identifier == identifier);
            return sessionHook != null;
        }

        public virtual IGitHubCopilotSessionHook Add(IGitHubCopilotSessionHook sessionHook)
        {
            SessionHooks.Add(sessionHook);
            return sessionHook;
        }

        protected virtual void InitializeCollectionFromDefaultLocations() { }

        public IEnumerator<IGitHubCopilotSessionHook> GetEnumerator()
        {
            return SessionHooks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
