using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotSdkBasedSessionHook : IHaveStringIdentifier, IHaveDescription
    {
        public abstract void ApplyTo(SessionHooks sessionHooks);
    }
}
