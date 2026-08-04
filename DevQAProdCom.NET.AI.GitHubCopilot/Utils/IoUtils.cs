using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Utils
{
    public interface IoUtils
    {
        public static List<string> GetCopilotAgents(string initialFolder)
        {
            var agentsDirectory = Path.Combine(initialFolder, Const.Directories.GetGitHubAgentsDirectory());

            if (!Directory.Exists(agentsDirectory))
            {
                return new List<string>();
            }

            return GlobalIoUtils.GetMarkdownFiles(agentsDirectory);
        }
    }
}
