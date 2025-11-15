using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Xml;

namespace DevQAProdCom.NET.Configurations.OperativeClasses;

public class CustomXmlConfigurationSource : XmlConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new CustomXmlConfigurationProvider(this);
    }
}

