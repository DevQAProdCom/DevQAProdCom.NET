using System.Text;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Models;
using DevQAProdCom.NET.Global.Utils;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Validators
{
    public class OrchestratorReadWriteAgentResponseValidator(string expectedOutputFilePath, IEnumerable<string> expectedData) : IAiInteractionResultValidator
    {
        public OrchestratorReadWriteAgentResponseValidator(OrchestratorReadWriteAgentRequestModel requestModel, IEnumerable<string> expectedData)
            : this(requestModel.OutputFilePathToWrite, expectedData)
        {
        }

        public IValidate Validate(IAiInteractionDataBank? interactionDataBank = null)
        {
            var error = GetValidationErrors(expectedOutputFilePath, expectedData);

            return new ValidationModel
            {
                Error = error
            };
        }

        public static string? GetValidationErrors(string expectedOutputFilePath, IEnumerable<string> expectedData)
        {
            var errors = new StringBuilder();

            var fileExistsError = ValidateFileExists(expectedOutputFilePath);
            if (!string.IsNullOrEmpty(fileExistsError))
            {
                errors.AppendLine(fileExistsError);
                return errors.ToString().TrimEnd();
            }

            var fileReadError = ValidateFileReadable(expectedOutputFilePath, out var outputContent);
            if (!string.IsNullOrEmpty(fileReadError))
            {
                errors.AppendLine(fileReadError);
                return errors.ToString().TrimEnd();
            }

            var jsonFormatError = ValidateJsonFormat(expectedOutputFilePath, outputContent, out var dataModel);
            if (!string.IsNullOrEmpty(jsonFormatError))
            {
                errors.AppendLine(jsonFormatError);
                return errors.ToString().TrimEnd();
            }

            var dataMatchError = ValidateDataMatch(expectedOutputFilePath, dataModel, expectedData);
            if (!string.IsNullOrEmpty(dataMatchError))
            {
                errors.AppendLine(dataMatchError);
            }

            return errors.Length > 0 ? errors.ToString().TrimEnd() : null;
        }

        private static string? ValidateFileExists(string expectedOutputFilePath)
        {
            if (!IoUtils.FileExists(expectedOutputFilePath))
            {
                return $"Output file '{expectedOutputFilePath}' was not created. " +
                       "The orchestrator must invoke the write-agent to create the output file at the specified outputFilePathToWrite location.";
            }

            return null;
        }

        private static string? ValidateFileReadable(string expectedOutputFilePath, out string outputContent)
        {
            outputContent = string.Empty;

            try
            {
                outputContent = File.ReadAllText(expectedOutputFilePath);
                return null;
            }
            catch (Exception ex)
            {
                return $"Failed to read output file '{expectedOutputFilePath}'. Error: {ex.Message}. " +
                       "The agent must create a readable output file.";
            }
        }

        private static string? ValidateJsonFormat(string expectedOutputFilePath, string outputContent, out DataModel? dataModel)
        {
            dataModel = null;

            try
            {
                dataModel = outputContent.FromJson<DataModel>();
                return null;
            }
            catch (Exception ex)
            {
                return $"Output file '{expectedOutputFilePath}' does not contain valid JSON. Error: {ex.Message}. " +
                       "The content must be written as a JSON object with a single 'data' property, " +
                       "where the value is an array of strings.";
            }
        }

        private static string? ValidateDataMatch(string expectedOutputFilePath, DataModel? dataModel, IEnumerable<string> expectedData)
        {
            var expectedDataList = expectedData.ToList();

            if (dataModel?.Data == null)
            {
                return $"Output file '{expectedOutputFilePath}' does not contain a valid 'data' array. " +
                       "The response must include a 'data' property with an array value.";
            }

            var actualDataList = dataModel.Data.ToList();

            if (actualDataList.Count != expectedDataList.Count)
            {
                return $"Output file '{expectedOutputFilePath}' contains {actualDataList.Count} data entries, " +
                       $"but {expectedDataList.Count} were expected. The 'data' array must contain one entry for each file in 'filePathsToRead'.";
            }

            for (int i = 0; i < expectedDataList.Count; i++)
            {
                if (actualDataList[i] != expectedDataList[i])
                {
                    return $"Output file '{expectedOutputFilePath}' data entry at index {i} does not match the expected content. " +
                           "Each entry must be the exact, unmodified content of the corresponding input file.";
                }
            }

            return null;
        }
    }
}
