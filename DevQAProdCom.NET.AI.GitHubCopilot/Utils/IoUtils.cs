using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Utils
{
    public interface IoUtils
    {
        public static List<string> GetCopilotAgents(string rootDirectory)
        {
            var gitHubAgentsDirectory = Const.Directories.GetGitHubAgentsDirectory(rootDirectory);

            if (!Directory.Exists(gitHubAgentsDirectory))
            {
                return new List<string>();
            }

            return GlobalIoUtils.GetMarkdownFiles(gitHubAgentsDirectory);
        }

        public static List<string> GetCopilotInstructions(string directory)
        {
            var instructionsDirectory = Path.Combine(directory, Const.Directories.GetGitHubInstructionsDirectory());

            if (!Directory.Exists(instructionsDirectory))
            {
                return new List<string>();
            }

            return GlobalIoUtils.GetMarkdownFiles(instructionsDirectory);
        }

        public static List<string> GetCopilotSkills(string rootDirectory)
        {
            if (!Directory.Exists(rootDirectory))
            {
                return new List<string>();
            }

            return GlobalIoUtils.GetFilesInDirectory(rootDirectory, "SKILL.md", SearchOption.AllDirectories)
                .Select(x => x.FullName)
                .ToList();
        }
    }
}
