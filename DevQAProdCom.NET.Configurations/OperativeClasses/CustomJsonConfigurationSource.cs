using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace DevQAProdCom.NET.Configurations.OperativeClasses;

public class CustomJsonConfigurationSource : JsonConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new CustomJsonConfigurationProvider(this);
    }
}
