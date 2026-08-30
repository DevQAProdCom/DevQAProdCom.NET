namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants
{
    internal partial class Const
    {
        internal static class AiAgents
        {
            internal static class Names
            {
                internal const string READ_WRITE_AGENT = "read-write-agent";
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

                internal const string ANSWER_QUESTION_WHAT_IS_MY_FAVORITE_ANIMAL = "Answer question 'What is my favorite animal?'";
            }
        }
    }
}
