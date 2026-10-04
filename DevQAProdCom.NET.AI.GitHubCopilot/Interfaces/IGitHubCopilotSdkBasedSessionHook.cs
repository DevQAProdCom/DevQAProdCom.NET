using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotSdkBasedSessionHook : IHaveStringIdentifier, IHaveDescription
    {
        public void ApplyTo(SessionHooks sessionHooks);
    }
}
