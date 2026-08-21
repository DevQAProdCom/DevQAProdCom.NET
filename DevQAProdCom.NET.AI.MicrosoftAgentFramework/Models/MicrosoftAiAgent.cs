using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Models
{
    public class MicrosoftAiAgent : IMicrosoftAiAgent
    {
        public AIAgent AiAgent { get; set; }
        public Func<ValueTask>? DisposeProviderResourcesAsync { get; set; }

        public ValueTask DisposeAsync()
        {
            if (DisposeProviderResourcesAsync != null)
            {
                return DisposeProviderResourcesAsync();
            }

            return ValueTask.CompletedTask;
        }
    }
}
