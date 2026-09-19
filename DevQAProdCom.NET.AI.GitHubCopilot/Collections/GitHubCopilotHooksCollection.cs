using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotHooksCollection
    {
        private List<IHook> _hooks = new();
        IHookSearcher hookSearcher

        public GitHubCopilotHooksCollection(IHookSearcher hookSearcher, ILogger logger)
        {
        }

       
    

        public List<IHook> GetByEventTriggerName(string eventTriggerName)
        {

        }

        public List<IHook> GetFromFileAndEventTriggerName(string fileLocator, string eventTriggerName)
        {

        }

      

        private void AddHooksFromDefaultDirectories()
        {
            var hooks = hookSearcher.SearchInDefaultLocations();

            //foreach
            //AddHook
        }
    }
}
