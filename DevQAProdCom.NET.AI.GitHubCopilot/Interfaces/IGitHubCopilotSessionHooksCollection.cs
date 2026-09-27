namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotSessionHooksCollection : IEnumerable<IGitHubCopilotSessionHook>
    {
        public string CollectionIdentifier { get; }
        public IGitHubCopilotSessionHook GetByIdentifier(string identifier);
        public bool TryGetByIdentifierOrDefault(string identifier, out IGitHubCopilotSessionHook? mcpServer);
        public IGitHubCopilotSessionHook Add(IGitHubCopilotSessionHook mcpServer);
    }
}
