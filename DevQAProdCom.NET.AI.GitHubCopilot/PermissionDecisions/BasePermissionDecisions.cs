namespace DevQAProdCom.NET.AI.GitHubCopilot.PermissionDecisions
{
    public class BasePermissionDecisions
    {
        protected string GetToolsNotAllowedMessage(params string[] tools)
        {
            if (tools.Length <= 0)
                throw new ArgumentException("At least one tool must be specified.", nameof(tools));

            var toolOrTools = tools.Length > 1 ? "tools are" : "tool is";
            return $"Usage of '{string.Join(", ", tools)}' {toolOrTools} not allowed. Check the list of allowed tools and use any other allowed tool to solve the task";
        }
    }
}
