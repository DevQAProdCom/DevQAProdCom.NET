namespace Tests.DevQAProdCom.NET.AI.Constants
{
    internal partial class Const
    {
        internal static class AiAgents
        {
            internal static class Names
            {
                internal const string READ_WRITE_AGENT = "Read Write Agent";
            }

            internal static class Prompts
            {
                internal static string GetReadWriteAgentPrompt(string filePathToRead, string outputFolderToWrite)
                {
                    return $"Execute '{Names.READ_WRITE_AGENT}' with next parameters: file_path_to_read = {filePathToRead} and output_folder_to_write = {outputFolderToWrite}";
                }
            }
        }
    }
}
