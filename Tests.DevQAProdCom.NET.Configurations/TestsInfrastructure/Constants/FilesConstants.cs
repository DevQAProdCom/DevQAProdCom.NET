namespace Tests.DevQAProdCom.NET.Configurations.Constants;
internal partial class Const
{
    public static class Files
    {
        public static string EnvironmentJson = $"{ENVIRONMENT.ToLower()}.json";
        public const string JSON_AppSettingsTestsDev = "appsettings.Tests.DEV.json";
        public const string XML_AppSettingsTestsQa = "appsettings.Tests.QA.xml";
        public const string INI_AppSettingsTestsQa = "appsettings.Tests.QA.ini";
    }
}
