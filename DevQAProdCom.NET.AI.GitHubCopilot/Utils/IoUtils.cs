using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Utils
{
    public interface IoUtils
    {
        public static List<string> GetCopilotAgents(string rootDirectory)
        {
            var gitHubAgentsDirectory = Const.Directories.GetGitHubAgentsDirectory(rootDirectory);
            var agentFiles = new List<string>();

            if (Directory.Exists(gitHubAgentsDirectory))
            {
                agentFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(gitHubAgentsDirectory).Select(x => x.FullName));
            }

            if (Directory.Exists(rootDirectory))
            {
                agentFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(rootDirectory, $"*{FilesConstants.AGENT_MD}", SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return agentFiles.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        public static List<string> GetCopilotInstructions(string rootDirectory)
        {
            var instructionsDirectory = Const.Directories.GetGitHubInstructionsDirectory(rootDirectory);
            var instructionFiles = new List<string>();

            if (Directory.Exists(instructionsDirectory))
            {
                instructionFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(instructionsDirectory).Select(x => x.FullName));
            }

            if (Directory.Exists(rootDirectory))
            {
                instructionFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(rootDirectory, $"*{FilesConstants.INSTRUCTIONS_MD}", SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return instructionFiles.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
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
