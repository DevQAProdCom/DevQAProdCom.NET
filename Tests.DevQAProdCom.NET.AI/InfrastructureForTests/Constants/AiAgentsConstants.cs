namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants
{
    internal partial class Const
    {
        internal static class AiAgents
        {
            internal static class Names
            {
                internal const string READ_WRITE_AGENT = "read-write-agent";
                internal const string READ_AGENT = "read-agent";
                internal const string WRITE_AGENT = "write-agent";
                internal const string ORCHESTRATOR_READ_WRITE_AGENT = "orchestrator-read-write-agent";
                internal const string ANSWER_QUESTIONS_AGENT = "answer-questions-agent";
                internal const string CHECK_CUSTOM_INSTRUCTIONS_FIELD_ANSWER_QUESTIONS_AGENT = "check-custom-instructions-field-answer-questions-agent";
                internal const string CHECK_CUSTOM_SKILLS_FIELD_ANSWER_QUESTIONS_AGENT = "check-custom-skills-field-answer-questions-agent";
            }

            internal static class Prompts
            {
                internal static string GetReadWriteAgentPrompt(string filePathToRead, string outputFolderToWrite)
                {
                    return $"Execute '{Names.READ_WRITE_AGENT}' with next parameters: file_path_to_read = {filePathToRead} and output_folder_to_write = {outputFolderToWrite}";
                }

                internal static string GetReadAgentPrompt(string filePathToRead)
                {
                    return $"file_path_to_read = {filePathToRead}";
                }

                internal static string GetWriteAgentPrompt(string outputFilePathToWrite, IEnumerable<string> data)
                {
                    return $"{{\"outputFilePathToWrite\":\"{outputFilePathToWrite}\",\"data\":{System.Text.Json.JsonSerializer.Serialize(data)}}}";
                }

                internal static string GetOrchestratorReadWriteAgentPrompt(IEnumerable<string> filePathsToRead, string outputFilePathToWrite)
                {
                    var filePathsJson = System.Text.Json.JsonSerializer.Serialize(filePathsToRead);
                    return $"{{\"filePathsToRead\":{filePathsJson},\"outputFilePathToWrite\":\"{outputFilePathToWrite}\"}}";
                }
            }
        }
    }
}
