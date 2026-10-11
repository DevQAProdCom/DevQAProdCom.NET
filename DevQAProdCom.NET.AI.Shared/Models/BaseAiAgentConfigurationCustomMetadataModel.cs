using DevQAProdCom.NET.AI.Shared.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using YamlDotNet.Serialization;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class BaseAiAgentConfigurationCustomMetadataModel : IAiAgentConfigurationCustomMetadata
    {
        [YamlMember(Alias = "configurations")]
        public List<IAiAgentConfigurationWithoutAiEntityIdentifiers>? Configurations { get; set; } = new List<IAiAgentConfigurationWithoutAiEntityIdentifiers>();

        public IAiAgentConfigurationWithoutAiEntityIdentifiers GetConfiguration(string? identifier = null)
        {
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
                throw new InvalidOperationException($"No configuration available for identifier {identifier ?? "default"}.");
            }

            if (configurations.Count > 1)
            {
                throw new InvalidOperationException($"Multiple configurations available for identifier {identifier ?? "default"}.");
            }

            return configurations.First();
        }
    }
}
