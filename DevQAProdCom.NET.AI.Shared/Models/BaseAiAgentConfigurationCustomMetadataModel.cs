using System.Text.Json.Serialization;
using DevQAProdCom.NET.AI.Shared.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class BaseAiAgentConfigurationCustomMetadataModel : IAiAgentConfigurationCustomMetadata
    {
        [JsonPropertyName("configurations")]
        [YamlMember(Alias = "configurations")]
        public List<IAiAgentConfigurationWithoutAiEntityIdentifiers>? Configurations { get; set; } = new List<IAiAgentConfigurationWithoutAiEntityIdentifiers>();

        public IAiAgentConfigurationWithoutAiEntityIdentifiers GetConfiguration(string? identifier = null)
        {
            if (TryGetConfiguration(identifier, out var configuration))
            {
                return configuration!;
            }

            throw new InvalidOperationException($"No configuration available for identifier {identifier ?? "null"}.");
        }

        public bool TryGetConfiguration(string? identifier, out IAiAgentConfigurationWithoutAiEntityIdentifiers? configuration)
        {
            configuration = null;

            List<IAiAgentConfigurationWithoutAiEntityIdentifiers>? configurations;

            if (identifier == null)
            {
                configurations = Configurations?.Where(x => string.IsNullOrEmpty(x.ConfigurationIdentifier) || x.ConfigurationIdentifier == SharedAiConstants.Configurations.DEFAULT).ToList();
            }
            else
            {
                configurations = Configurations?.Where(x => x.ConfigurationIdentifier == identifier).ToList();
            }

            if (configurations == null || configurations.Count == 0)
            {
                return false;
            }

            if (configurations.Count > 1)
            {
                throw new InvalidOperationException($"Multiple configurations available for identifier {identifier ?? "null/string.Empty/default"}.");
            }

            configuration = configurations.First();
            return true;
        }
    }
}
