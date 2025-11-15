using DevQAProdCom.NET.Configurations.OperativeClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace DevQAProdCom.NET.Configurations.Extensions;

public static class CustomJsonConfigurationExtensions
{
    public static IConfigurationBuilder AddJsonFileWithCustomJsonSerializerForStringNullAndEnumerableEmpty(this IConfigurationBuilder builder, string path, bool optional = false, bool reloadOnChange = false)
    {
        var source = new CustomJsonConfigurationSource
        {
            FileProvider = new PhysicalFileProvider(Path.GetDirectoryName(path)),
            Path = Path.GetFileName(path),
            Optional = optional,
            ReloadOnChange = reloadOnChange
        };
        builder.Add(source);
        return builder;
    }

    public static IConfigurationBuilder AddXmlFileWithCustomXmlSerializerForStringNullAndEnumerableEmpty(this IConfigurationBuilder builder, string path, bool optional = false, bool reloadOnChange = false)
    {
        var source = new CustomXmlConfigurationSource
        {
            FileProvider = new PhysicalFileProvider(Path.GetDirectoryName(path)),
            Path = Path.GetFileName(path),
            Optional = optional,
            ReloadOnChange = reloadOnChange
        };
        builder.Add(source);
        return builder;
    }
}
