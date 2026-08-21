using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces
{
    public interface IMicrosoftAiAgent : IAsyncDisposable
    {
        public AIAgent AiAgent { get;set; }
        public Func<ValueTask> DisposeProviderResourcesAsync { get;set; }
    }
}
