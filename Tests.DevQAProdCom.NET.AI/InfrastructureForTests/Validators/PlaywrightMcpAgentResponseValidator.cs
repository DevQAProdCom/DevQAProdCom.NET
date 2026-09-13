using System.Text;
using System.Text.RegularExpressions;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Models;
using DevQAProdCom.NET.Global.Utils;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Validators
{
    public class PlaywrightMcpAgentResponseValidator(string expectedOutputFilePath) : IAiInteractionResultValidator
    {
        public PlaywrightMcpAgentResponseValidator(PlaywrightMcpAgentRequestModel requestModel)
            : this(requestModel.FilePath)
        {
        }

        public IValidate Validate(IAiInteractionDataBank? interactionDataBank = null)
        {
            var error = GetValidationErrors(expectedOutputFilePath);

            return new ValidationModel
            {
                Error = error
            };
        }

        public static string? GetValidationErrors(string expectedOutputFilePath)
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

            var urlError = ValidateContainsUrl(expectedOutputFilePath, outputContent);
            if (!string.IsNullOrEmpty(urlError))
            {
                errors.AppendLine(urlError);
            }

            return errors.Length > 0 ? errors.ToString().TrimEnd() : null;
        }

        private static string? ValidateFileExists(string expectedOutputFilePath)
        {
            if (!IoUtils.FileExists(expectedOutputFilePath))
            {
                return $"Output file '{expectedOutputFilePath}' was not created. " +
                       "The agent must create the output file at the specified FilePath location.";
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

        private static string? ValidateContainsUrl(string expectedOutputFilePath, string outputContent)
        {
            var match = Regex.Match(outputContent, @"https?://[^\s""'<>]+", RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return $"Output file '{expectedOutputFilePath}' does not contain a URL-formatted string. " +
                       "The agent must write a valid URL to the output file.";
            }

            if (!Uri.TryCreate(match.Value, UriKind.Absolute, out _))
            {
                return $"Output file '{expectedOutputFilePath}' contains a value that looks like a URL but is not a valid absolute URI: '{match.Value}'.";
            }

            return null;
        }
    }
}
