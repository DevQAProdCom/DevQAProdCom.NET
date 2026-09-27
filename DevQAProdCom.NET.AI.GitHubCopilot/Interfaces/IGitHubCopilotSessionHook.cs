using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotSessionHook
    {
        public string Identifier { get; set; }
        public void Assign(SessionHooks sessionHooks);
    }
}
