using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Utils
{
    public interface IoUtils
    {
        public static List<string> GetCopilotAgents(string rootDirectory, bool useExtendedSearch = false)
        {
            GlobalIoUtils.CheckDirectoryMustExist(rootDirectory);
            var agentFiles = new List<string>();

            var gitHubAgentsDirectory = Const.Directories.GetGitHubAgentsDirectory(rootDirectory);
            if (Directory.Exists(gitHubAgentsDirectory))
            {
                agentFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(gitHubAgentsDirectory).Select(x => x.FullName));
            }

            if (useExtendedSearch)
            {
                agentFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(rootDirectory, $"*{FilesConstants.AGENT_MD}", SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return agentFiles.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        public static List<string> GetCopilotInstructions(string rootDirectory, bool useExtendedSearch = false)
        {
            GlobalIoUtils.CheckDirectoryMustExist(rootDirectory);
            var instructionFiles = new List<string>();

            var instructionsDirectory = Const.Directories.GetGitHubInstructionsDirectory(rootDirectory);
            if (Directory.Exists(instructionsDirectory))
            {
                instructionFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(instructionsDirectory).Select(x => x.FullName));
            }

            if (useExtendedSearch)
            {
                instructionFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(rootDirectory, $"*{FilesConstants.INSTRUCTIONS_MD}", SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return instructionFiles.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        public static List<string> GetCopilotSkills(string rootDirectory, bool useExtendedSearch = false)
        {
            GlobalIoUtils.CheckDirectoryMustExist(rootDirectory);
            var skillFiles = new List<string>();

            List<string> GetSkills(string directory) => GlobalIoUtils.GetFilesInDirectory(directory, $"*{FilesConstants.SKILL_MD}", SearchOption.AllDirectories).Select(x => x.FullName).ToList();

            if (useExtendedSearch)
            {
                skillFiles.AddRange(GetSkills(rootDirectory));
                return skillFiles;
            }

            var skillsDirectory = Const.Directories.GetGitHubSkillsDirectory(rootDirectory);
            if (Directory.Exists(skillsDirectory))
            {
                skillFiles.AddRange(GetSkills(skillsDirectory));
            }

            return skillFiles;
        }
    }
}
