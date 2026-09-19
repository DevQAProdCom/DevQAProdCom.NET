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
            public const string HOOKS = "hooks";

            public static string GetGitHubAgentsDirectory(string? rootDirectory = null)
            {
                return string.IsNullOrEmpty(rootDirectory)
                    ? Path.Combine(GITHUB, AGENTS)
                    : Path.Combine(rootDirectory, GITHUB, AGENTS);
            }

            public static string GetGitHubInstructionsDirectory(string? rootDirectory = null)
            {
                return string.IsNullOrEmpty(rootDirectory)
                    ? Path.Combine(GITHUB, INSTRUCTIONS)
                    : Path.Combine(rootDirectory, GITHUB, INSTRUCTIONS);
            }

            public static string GetGitHubSkillsDirectory(string? rootDirectory = null)
            {
                return string.IsNullOrEmpty(rootDirectory)
                    ? Path.Combine(GITHUB, SKILLS)
                    : Path.Combine(rootDirectory, GITHUB, SKILLS);
            }

            public static string GetGitHubHooksDirectory(string? rootDirectory = null)
            {
                return string.IsNullOrEmpty(rootDirectory)
                    ? Path.Combine(GITHUB, HOOKS)
                    : Path.Combine(rootDirectory, GITHUB, HOOKS);
            }
        }
    }
}
