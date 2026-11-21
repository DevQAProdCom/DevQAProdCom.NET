using System.ComponentModel;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Enumerations
{
    public enum SessionEvent
    {
        [Description("assistant.message")]
        AssistantMessage,

        [Description("assistant.usage")]
        AssistantUsage,

        [Description("session.start")]
        SessionStart,

        [Description("session.shutdown")]
        SessionShutdown,

        [Description("subagent.selected")]
        SubagentSelected,

        [Description("subagent.started")]
        SubagentStarted,

        [Description("subagent.completed")]
        SubagentCompleted,

        [Description("subagent.failed")]
        SubagentFailed
    }
}
