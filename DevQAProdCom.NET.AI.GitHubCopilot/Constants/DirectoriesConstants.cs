namespace DevQAProdCom.NET.AI.GitHubCopilot.Constants
{
    internal static partial class Const
    {
        internal static class Directories
        {
            public const string GITHUB = ".github";
            public const string AGENTS = "agents";
            public const string INSTRUCTIONS = "instructions";
            public const string SKILLS = "skills";

            public static string GetGitHubAgentsDirectory(string? directory = null)
            {
                return string.IsNullOrEmpty(directory)
                    ? Path.Combine(GITHUB, AGENTS)
                    : Path.Combine(directory, GITHUB, AGENTS);
            }

            public static string GetGitHubInstructionsDirectory(string? directory = null)
            {
                return string.IsNullOrEmpty(directory)
                    ? Path.Combine(GITHUB, INSTRUCTIONS)
                    : Path.Combine(directory, GITHUB, INSTRUCTIONS);
            }

            public static string GetGitHubSkillsDirectory(string? directory = null)
            {
                return string.IsNullOrEmpty(directory)
                    ? Path.Combine(GITHUB, SKILLS)
                    : Path.Combine(directory, GITHUB, SKILLS);
            }
        }
    }
}
