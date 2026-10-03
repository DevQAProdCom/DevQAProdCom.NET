using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations.Files;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Utils
{
    public interface IoUtils
    {
        public static List<string> GetFilesWithCopilotAgents(string rootDirectory, bool useExtendedSearch = false)
        {
            GlobalIoUtils.CheckDirectoryMustExist(rootDirectory);
            var agentFiles = new List<string>();

            var gitHubAgentsDirectory = Const.Directories.GetGitHubAgentsDirectory(rootDirectory);
            if (Directory.Exists(gitHubAgentsDirectory))
            {
                agentFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(gitHubAgentsDirectory, searchOption: SearchOption.AllDirectories).Select(x => x.FullName));
            }

            if (useExtendedSearch)
            {
                agentFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(rootDirectory, searchPattern: $"*{Const.Files.Extensions.AGENT_MD}", searchOption: SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return agentFiles.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        public static List<string> GetFilesWithCopilotInstructions(string rootDirectory, bool useExtendedSearch = false)
        {
            GlobalIoUtils.CheckDirectoryMustExist(rootDirectory);
            var instructionFiles = new List<string>();

            var instructionsDirectory = Const.Directories.GetGitHubInstructionsDirectory(rootDirectory);
            if (Directory.Exists(instructionsDirectory))
            {
                instructionFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(instructionsDirectory, searchOption: SearchOption.AllDirectories).Select(x => x.FullName));
            }

            if (useExtendedSearch)
            {
                instructionFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(rootDirectory, searchPattern: $"*{Const.Files.Extensions.INSTRUCTIONS_MD}", searchOption: SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return instructionFiles.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        public static List<string> GetFilesWithCopilotSkills(string rootDirectory, bool useExtendedSearch = false)
        {
            GlobalIoUtils.CheckDirectoryMustExist(rootDirectory);
            var skillFiles = new List<string>();

            List<string> GetSkills(string directory) => GlobalIoUtils.GetFilesInDirectory(directory, $"*{Const.Files.Extensions.SKILL_MD}", SearchOption.AllDirectories).Select(x => x.FullName).ToList();

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

        public static List<string> GetFilesWithCopilotMcpServers(string rootDirectory, bool useExtendedSearch = false)
        {
            GlobalIoUtils.CheckDirectoryMustExist(rootDirectory);
            var mcpServersFiles = new List<string>();

            //From direct file .github/mcp.json
            var gitHubDirectory = Const.Directories.GetGitHubDirectory(rootDirectory);
            var baseMcpJsonFilePath = Path.Combine(gitHubDirectory, Const.Files.Extensions.MCP_JSON);

            if (GlobalIoUtils.FileExists(baseMcpJsonFilePath))
            {
                mcpServersFiles.Add(baseMcpJsonFilePath);
            }

            //From .github/mcp-servers/{*}mcp.json
            var mcpServersDirectory = Const.Directories.GetGitHubMcpServersDirectory(rootDirectory);
            var mcpServersDirectoryFiles = GlobalIoUtils.GetFilesInDirectory(mcpServersDirectory, searchPattern: $"*{FileExtension.Json.GetDescriptionAttributeValue()}", searchOption: SearchOption.AllDirectories).Select(x => x.FullName);
            mcpServersFiles.AddRange(mcpServersDirectoryFiles);

            //From any files that end with mcp.json in the root directory and all subdirectories
            if (useExtendedSearch)
            {
                mcpServersFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(rootDirectory, searchPattern: $"*{Const.Files.Extensions.MCP_JSON}", searchOption: SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return mcpServersFiles.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        public static List<string> GetFilesWithCopilotHooks(string rootDirectory, bool useExtendedSearch = false)
        {
            GlobalIoUtils.CheckDirectoryMustExist(rootDirectory);
            var hooksFiles = new List<string>();

            var gitHubHooksDirectory = Const.Directories.GetGitHubHooksDirectory(rootDirectory);

            if (Directory.Exists(gitHubHooksDirectory))
            {
                hooksFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(gitHubHooksDirectory, searchPattern: $"*{FileExtension.Json.GetDescriptionAttributeValue()}", searchOption: SearchOption.AllDirectories).Select(x => x.FullName));
            }

            if (useExtendedSearch)
            {
                hooksFiles.AddRange(GlobalIoUtils.GetFilesInDirectory(rootDirectory, searchPattern: $"*{Const.Files.Extensions.HOOKS_JSON}", searchOption: SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return hooksFiles.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }
    }
}
