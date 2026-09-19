using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses
{
    public class GitHubCopilotHookSearcher : IHookSearcher
    {
        public List<string>? Locations { get; set; }

        public GitHubCopilotHookSearcher(List<string> locations)
        {
            this.Locations = locations ??= new List<string>(IoUtils.GetCopilotHooks(Directory.GetCurrentDirectory()));
        }

        public virtual List<IHook> SearchInDefaultLocations(bool userExtendedSearch = true)
        {

            return new List<IHook>();
        }

        public virtual List<IHook> Search(string path, bool userExtendedSearch = true)
        {

        }
    }
}
