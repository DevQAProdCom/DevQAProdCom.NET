using System.Text;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Models;
using DevQAProdCom.NET.Global.Utils;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Validators
{
    public class AnswerQuestionsAgentReponseValidator(string expectedOutputFilePath, IEnumerable<string> expectedQuestions) : IAiInteractionResultValidator
    {
        public IValidate Validate(IAiInteractionDataBank? interactionDataBank = null)
        {
            var error = GetValidationErrors(expectedOutputFilePath, expectedQuestions);

            return new ValidationModel
            {
                Error = error
            };
        }

        public static string? GetValidationErrors(string expectedOutputFilePath, IEnumerable<string> expectedQuestions)
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

            var jsonFormatError = ValidateJsonFormat(expectedOutputFilePath, outputContent, out var responseModel);
            if (!string.IsNullOrEmpty(jsonFormatError))
            {
                errors.AppendLine(jsonFormatError);
                return errors.ToString().TrimEnd();
            }

            var questionsAndAnswersError = ValidateQuestionsAndAnswers(expectedOutputFilePath, responseModel, expectedQuestions);
            if (!string.IsNullOrEmpty(questionsAndAnswersError))
            {
                errors.AppendLine(questionsAndAnswersError);
            }

            return errors.Length > 0 ? errors.ToString().TrimEnd() : null;
        }

        private static string? ValidateFileExists(string expectedOutputFilePath)
        {
            if (!IoUtils.FileExists(expectedOutputFilePath))
            {
                return $"Output file '{expectedOutputFilePath}' was not created. " +
                       "The agent must create the output file at the specified file_path_to_write location.";
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

        private static string? ValidateJsonFormat(string expectedOutputFilePath, string outputContent, out AnswerQuestionsAgentReponseModel? responseModel)
        {
            responseModel = null;

            try
            {
                responseModel = outputContent.FromJson<AnswerQuestionsAgentReponseModel>();
                return null;
            }
            catch (Exception ex)
            {
                return $"Output file '{expectedOutputFilePath}' does not contain valid JSON. Error: {ex.Message}. " +
                       "The content must be written as a JSON object with a single 'questionsAndAnswers' property, " +
                       "where the value is an array of objects each containing a 'question' string and an 'answers' string array.";
            }
        }

        private static string? ValidateQuestionsAndAnswers(string expectedOutputFilePath, AnswerQuestionsAgentReponseModel? responseModel, IEnumerable<string> expectedQuestions)
        {
            var expectedQuestionsList = expectedQuestions.ToList();

            if (responseModel?.QuestionsAndAnswers == null)
            {
                return $"Output file '{expectedOutputFilePath}' does not contain a valid 'questionsAndAnswers' array. " +
                       "The response must include a 'questionsAndAnswers' property with an array value.";
            }

            var responseEntries = responseModel.QuestionsAndAnswers.ToList();
            var responseQuestions = responseEntries.Select(x => x.Question).ToList();

            var duplicateResponseQuestions = responseQuestions
                .Where(q => !string.IsNullOrEmpty(q))
                .GroupBy(q => q)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateResponseQuestions.Count > 0)
            {
                return $"Output file '{expectedOutputFilePath}' contains duplicate entries for the following questions: " +
                       $"{string.Join(", ", duplicateResponseQuestions)}. " +
                       "Each question must have exactly one corresponding entry in the 'questionsAndAnswers' array.";
            }

            var missingQuestions = expectedQuestionsList
                .Where(expected => !responseQuestions.Any(actual => actual == expected))
                .ToList();

            if (missingQuestions.Count > 0)
            {
                return $"Output file '{expectedOutputFilePath}' is missing answers for the following questions: " +
                       $"{string.Join(", ", missingQuestions)}. " +
                       "Every question from the user prompt must have a corresponding entry in the 'questionsAndAnswers' array.";
            }

            var unexpectedQuestions = responseQuestions
                .Where(actual => !expectedQuestionsList.Any(expected => expected == actual))
                .Where(q => !string.IsNullOrEmpty(q))
                .ToList();

            if (unexpectedQuestions.Count > 0)
            {
                return $"Output file '{expectedOutputFilePath}' contains unexpected entries for questions not in the user prompt: " +
                       $"{string.Join(", ", unexpectedQuestions)}. " +
                       "The 'questionsAndAnswers' array must contain only entries for questions from the user prompt.";
            }

            foreach (var expectedQuestion in expectedQuestionsList)
            {
                var matchingEntry = responseEntries.FirstOrDefault(x => x.Question == expectedQuestion);

                if (matchingEntry == null)
                {
                    return $"Output file '{expectedOutputFilePath}' is missing an entry for question '{expectedQuestion}'.";
                }

                if (matchingEntry.Answers == null || matchingEntry.Answers.Count == 0)
                {
                    return $"Output file '{expectedOutputFilePath}' contains an empty 'answers' array for question '{expectedQuestion}'. " +
                           "Each question must have at least one answer.";
                }

                if (!matchingEntry.Answers.Any(answer => !string.IsNullOrEmpty(answer)))
                {
                    return $"Output file '{expectedOutputFilePath}' contains only null or empty answers for question '{expectedQuestion}'. " +
                           "Each question must have at least one non-null, non-empty answer.";
                }
            }

            return null;
        }
    }
}
