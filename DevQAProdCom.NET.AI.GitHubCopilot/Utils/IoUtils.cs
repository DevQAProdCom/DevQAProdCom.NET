using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Utils
{
    public interface IoUtils
    {
        public static List<string> GetCopilotAgents(string directory)
        {
            var agentsDirectory = Path.Combine(directory, Const.Directories.GetGitHubAgentsDirectory());

            if (!Directory.Exists(agentsDirectory))
            {
                return new List<string>();
            }

            return GlobalIoUtils.GetMarkdownFiles(agentsDirectory);
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

        public static List<string> GetCopilotSkills(string directory)
        {
            var skillsDirectory = Path.Combine(directory, Const.Directories.GetGitHubSkillsDirectory());

            if (!Directory.Exists(skillsDirectory))
            {
                return new List<string>();
            }

            return GlobalIoUtils.GetMarkdownFiles(skillsDirectory);
        }
    }
}
