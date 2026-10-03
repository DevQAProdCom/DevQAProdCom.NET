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
            public const string MCP_SERVERS = "mcp-servers";
            public const string HOOKS = "hooks";

            public static string GetGitHubDirectory(string? rootDirectory = null)
            {
                return string.IsNullOrEmpty(rootDirectory)
                    ? GITHUB
                    : Path.Combine(rootDirectory, GITHUB);
            }

            public static string GetGitHubAgentsDirectory(string? rootDirectory = null)
            {
                return Path.Combine(GetGitHubDirectory(rootDirectory), AGENTS);
            }

            public static string GetGitHubInstructionsDirectory(string? rootDirectory = null)
            {
                return Path.Combine(GetGitHubDirectory(rootDirectory), INSTRUCTIONS);
            }

            public static string GetGitHubSkillsDirectory(string? rootDirectory = null)
            {
                return Path.Combine(GetGitHubDirectory(rootDirectory), SKILLS);
            }

            public static string GetGitHubMcpServersDirectory(string? rootDirectory = null)
            {
                return Path.Combine(GetGitHubDirectory(rootDirectory), MCP_SERVERS);
            }

            public static string GetGitHubHooksDirectory(string? rootDirectory = null)
            {
                return Path.Combine(GetGitHubDirectory(rootDirectory), HOOKS);
            }
        }
    }
}
