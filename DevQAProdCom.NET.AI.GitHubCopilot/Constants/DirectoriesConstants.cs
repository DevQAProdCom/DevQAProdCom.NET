namespace DevQAProdCom.NET.AI.GitHubCopilot.Constants
{
    internal static partial class Const
    {
        internal static class Directories
        {
            public const string GITHUB = ".github";
            public const string AGENTS = "agents";

            public static string GetGitHubAgentsDirectory(string? initialDirectory = null)
            {
                if (!string.IsNullOrEmpty(initialDirectory))
                {
                    return Path.Combine(initialDirectory, GITHUB, AGENTS);
                }

                return Path.Combine(GITHUB, AGENTS);
            }
        }
    }
}
