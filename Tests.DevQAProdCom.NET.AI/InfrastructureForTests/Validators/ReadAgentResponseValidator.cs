using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Models;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Validators
{
    public class ReadAgentResponseValidator(string expectedContent) : IAiInteractionResultValidator
    {
        public IValidate Validate(IAiInteractionDataBank? interactionDataBank = null)
        {
            var error = GetValidationErrors(interactionDataBank, expectedContent);

            return new ValidationModel
            {
                Error = error
            };
        }

        private static string? GetValidationErrors(IAiInteractionDataBank? interactionDataBank, string expectedContent)
        {
            if (interactionDataBank == null)
            {
                return "Interaction data bank is null. The agent did not produce any response.";
            }

            var actualContent = string.Join(string.Empty, interactionDataBank.Data
                .Values
                .SelectMany(stringBuilders => stringBuilders)
                .Select(stringBuilder => stringBuilder.ToString()));

            if (actualContent.Contains(expectedContent))
            {
                return null;
            }

            return $"Expected content was not found in the agent response. Expected: '{expectedContent}'. Actual response: '{actualContent}'.";
        }
    }
}
