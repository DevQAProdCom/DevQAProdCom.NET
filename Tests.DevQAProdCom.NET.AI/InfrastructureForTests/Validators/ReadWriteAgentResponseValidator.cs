using System.Text;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Models;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Validators
{
    public class ReadWriteAgentResponseValidator(string inputFilePath, string expectedOutputFilePath, string expectedContent) : IAiInteractionResultValidator
    {
        public IValidate Validate(IAiInteractionDataBank? interactionDataBank = null)
        {
            var error = GetValidationErrors(expectedOutputFilePath, expectedContent, inputFilePath);

            return new ValidationModel
            {
                Error = error
            };
        }

        public static string? GetValidationErrors(string expectedOutputFilePath, string expectedContent, string inputFilePath)
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

            var jsonFormatError = ValidateJsonFormat(expectedOutputFilePath, outputContent, out var contentModel);
            if (!string.IsNullOrEmpty(jsonFormatError))
            {
                errors.AppendLine(jsonFormatError);
                return errors.ToString().TrimEnd();
            }

            var contentMatchError = ValidateContentMatch(contentModel, expectedContent, expectedOutputFilePath, inputFilePath);
            if (!string.IsNullOrEmpty(contentMatchError))
            {
                errors.AppendLine(contentMatchError);
            }

            return errors.Length > 0 ? errors.ToString().TrimEnd() : null;
        }

        private static string? ValidateFileExists(string expectedOutputFilePath)
        {
            if (!File.Exists(expectedOutputFilePath))
            {
                return $"Output file '{expectedOutputFilePath}' was not created. " +
                       "The agent must create the output file in the specified output folder " +
                       "by inserting '_copilot' before the file extension of the input file name.";
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

        private static string? ValidateJsonFormat(string expectedOutputFilePath, string outputContent, out ContentModel? contentModel)
        {
            contentModel = null;

            try
            {
                contentModel = outputContent.FromJson<ContentModel>();
                return null;
            }
            catch (Exception ex)
            {
                return $"Output file '{expectedOutputFilePath}' does not contain valid JSON. Error: {ex.Message}. " +
                       "The content must be written as a JSON object with a single 'content' property, " +
                       "where the value is the raw content taken from the input file.";
            }
        }

        private static string? ValidateContentMatch(ContentModel? actualContentModel, string expectedContent, string expectedOutputFilePath, string inputFilePath)
        {
            if (actualContentModel?.Content != expectedContent)
            {
                return $"The 'content' property in output file '{expectedOutputFilePath}' does not match the raw content of the input file '{inputFilePath}'. " +
                       "The 'content' value must be the exact, unmodified content taken from the input file.";
            }

            return null;
        }
    }
}
