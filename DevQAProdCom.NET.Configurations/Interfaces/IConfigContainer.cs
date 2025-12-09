using Microsoft.Extensions.Configuration;

namespace DevQAProdCom.NET.Configurations.Interfaces
{
    public interface IConfigContainer
    {
        IConfigurationRoot Root { get; }
        string Environment { get; }

        #region Dynamic Configuration

        IConfigContainer AddConfigurationSource(IConfigurationSource source);
        IConfigContainer RemoveConfigurationSource(IConfigurationSource source);
        IConfigContainer Config(Action<IConfigurationBuilder> action);
        IConfigContainer AddConfigurationsFromDirectory(string directoryPath, bool optional = false);
        IConfigContainer AddConfigurationsFromDirectory(DirectoryInfo directory);

        #endregion Dynamic Configuration

        #region Extract Configuration Values

        #region Get/TryGet

        T? Get<T>(string keyPath);
        bool TryGet<T>(string keyPath, out T? value);

        #endregion Get/TryGet

        #region GetConnectionString/TryGetConnectionString

        string GetConnectionString(string name);
        bool TryGetConnectionString(string name, out string? connectionString);

        #endregion GetConnectionString/TryGetConnectionString

        #region GetEnvironmentVariable/TryGetEnvironmentVariable

        string? GetEnvironmentVariable(string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);
        T? GetEnvironmentVariable<T>(string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);

        bool TryGetEnvironmentVariable(string name, out string? envVariable, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);
        bool TryGetEnvironmentVariable<T>(string name, out T? envVariable, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);

        #endregion GetEnvironmentVariable/TryGetEnvironmentVariable

        #region Bind/TryBind

        T? Bind<T>(string? keyPath = null);
        bool TryBind<T>(out T? bindToEntity, string? keyPath = null);
        void Bind(object bindToEntity, string? keyPath = null);
        bool TryBind(object bindToEntity, string? keyPath = null);

        #endregion Bind/TryBind

        #endregion Extract Configuration Values

        #region Auxiliary Methods

        bool ContainsSection(string sectionKey);

        #endregion Auxiliary Methods
    }
}
