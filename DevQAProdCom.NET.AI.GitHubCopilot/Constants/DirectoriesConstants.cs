namespace DevQAProdCom.NET.AI.GitHubCopilot.Constants
{
    internal static partial class Const
    {
        internal static class Directories
        {
            public const string GITHUB = ".github";
            public const string AGENTS = "agents";
            public const string INSTRUCTIONS = "instructions";
            public const string PRIMARY = "primary";
            public const string SUB_AGENTS = "subAgents";
            public static string GetGitHubAgentsDirectory(string? directory = null)
            {
                if (!string.IsNullOrEmpty(directory))
                {
                    return Path.Combine(directory, GITHUB, AGENTS);
                }

                return Path.Combine(GITHUB, AGENTS);
            }

            public static string GetGitHubInstructionsDirectory(string? directory = null)
            {
                if (!string.IsNullOrEmpty(directory))
                {
                    return Path.Combine(directory, GITHUB, INSTRUCTIONS);
                }

                return Path.Combine(GITHUB, INSTRUCTIONS);
            }
        }
    }
}
