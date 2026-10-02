namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotSdkBasedSessionHooksCollection : IEnumerable<IGitHubCopilotSdkBasedSessionHook>
    {
        public string CollectionIdentifier { get; }
        public IGitHubCopilotSdkBasedSessionHook GetByIdentifier(string identifier);
        public bool TryGetByIdentifierOrDefault(string identifier, out IGitHubCopilotSdkBasedSessionHook? hook);
        public IGitHubCopilotSdkBasedSessionHook Add(IGitHubCopilotSdkBasedSessionHook hook);
    }
}
