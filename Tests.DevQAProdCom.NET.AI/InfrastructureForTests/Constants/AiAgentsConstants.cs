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
                internal const string PLAYWRIGHT_MCP_AGENT = "playwright-mcp-agent";
                internal const string READ_WRITE_AGENT_WITH_2_CONFIGURATIONS = "read-write-agent-with-2-configurations";
                internal const string CHECK_CUSTOM_FILE_BASED_HOOKS_FIELD_WITH_FILE_BASED_HOOK_AGENT = "check-custom-hooks-field-with-file-based-hook-agent";
                internal const string CHECK_CUSTOM_HOOKS_FIELD_WITH_SDK_BASED_HOOK_AGENT = "check-custom-hooks-field-with-sdk-based-hook-agent";
                internal const string CHECK_CUSTOM_HOOKS_FIELD_WITH_FILE_AND_SDK_BASED_HOOKS_AGENT = "check-custom-hooks-field-with-file-and-sdk-based-hooks-agent";
                internal const string CHECK_CUSTOM_MCP_SERVERS_FIELD_MCP_JSON_FILE_WITH_MCP_SERVERS_TOP_LEVEL_OBJECT_AGENT = "check-custom-mcp-servers-field-mcp-json-file-with-mcp-servers-top-level-object-agent";
                internal const string CHECK_CUSTOM_MCP_SERVERS_FIELD_MCP_SERVERS_DIRECTORY_WITH_GENERAL_TOP_LEVEL_OBJECT_AGENT = "check-custom-mcp-servers-field-mcp-servers-directory-with-general-top-level-object-agent";
                internal const string CHECK_CUSTOM_MCP_SERVERS_FIELD_SDK_BASED_AGENT = "check-custom-mcp-servers-field-sdk-based-agent";
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
