using System.ComponentModel;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Enumerations
{
    public enum SessionEvent
    {
        [Description("assistant.message")]
        AssistantMessage,

        [Description("subagent.started")]
        SubagentStarted
    }
}
