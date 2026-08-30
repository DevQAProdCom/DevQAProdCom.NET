using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations.Files;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Constants
{
    internal class FilesConstants
    {
        public static readonly string AGENT_MD = $"agent.{FileExtension.Md.GetDescriptionAttributeValue()}";
        public static readonly string INSTRUCTIONS_MD = $"instructions.{FileExtension.Md.GetDescriptionAttributeValue()}";
        public static readonly string SKILLS_MD = $"SKILL{FileExtension.Md.GetDescriptionAttributeValue()}";

        public static string GetGitHubAgentFileName(string agentName, string? directory = null)
        {
            var fileName = $"{agentName}.{AGENT_MD}";

            return string.IsNullOrEmpty(directory)
                ? fileName
                : Path.Combine(directory, fileName);
        }

        public static string GetGitHubInstructionFileName(string instructionName, string? directory = null)
        {
            var fileName = $"{instructionName}.{INSTRUCTIONS_MD}";

            return string.IsNullOrEmpty(directory)
                ? fileName
                : Path.Combine(directory, fileName);
        }

        public static string GetGitHubSkillFilePath(string skillName, string? directory = null)
        {
            var fileName = $"{SKILLS_MD}";

            return string.IsNullOrEmpty(directory)
                ? fileName
                : Path.Combine(directory, skillName, fileName);
        }
    }
}
