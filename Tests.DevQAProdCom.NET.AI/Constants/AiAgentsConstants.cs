namespace Tests.DevQAProdCom.NET.AI.Constants
{
    public partial class Const
    {
        public static class AiAgents
        {
            public static class Names
            {
                public const string READ_WRITE_AGENT = "Read Write Agent";
            }

            public static class Prompts
            {
                public static string GetReadWriteAgentPrompt(string filePathToRead, string outputFolderToWrite)
                {
                    return $"Execute '{Const.AiAgents.Names.READ_WRITE_AGENT}' with next parameters: file_path_to_read = {filePathToRead} and output_folder_to_write = {outputFolderToWrite}";
                }
            }
        }
    }
}
