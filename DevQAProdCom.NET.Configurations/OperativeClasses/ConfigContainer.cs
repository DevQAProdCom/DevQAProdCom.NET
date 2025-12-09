using System.Collections;
using DevQAProdCom.NET.Configurations.Constants;
using DevQAProdCom.NET.Configurations.Extensions;
using DevQAProdCom.NET.Configurations.Interfaces;
using DevQAProdCom.NET.Global.Enumerations.Files;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DevQAProdCom.NET.Configurations.OperativeClasses
{
    public class ConfigContainer : IConfigContainer
    {
        #region General Properties
        public virtual List<string> EnvVariableNames { get; } = new List<string> { Const.ENVIRONMENT, Const.ENV };
        protected virtual string? _environment { get; }
        public string? Environment
        {
            get
            {
                string? environment = GetEnvironmentNameFromDefaultLocations();
                if (!string.IsNullOrEmpty(environment))
                    return environment;

                //Can be used if override happens in derived class
                if (!string.IsNullOrEmpty(_environment))
                    return _environment;

                _log.Warning("Environment name is not provided for configurations.");
                return null;
            }
        }

        protected virtual string? GetEnvironmentNameFromDefaultLocations()
        {
            //Try get from environment variables
            foreach (var envVariableName in EnvVariableNames)
                if (TryGetEnvironmentVariable(envVariableName, out string? envVariableValue) && !string.IsNullOrEmpty(envVariableValue))
                    return envVariableValue;

            //Try get from file
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(DefaultBaseConfigurationsFolder, Const.Files.EnvironmentJson), optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine(DefaultSharedAcrossEnvironmentsConfigurationsFolder, Const.Files.EnvironmentJson), optional: true, reloadOnChange: true);
            string? environment = configurationBuilder.Build().GetSection(Const.ENVIRONMENT.ToLower()).Value;

            return environment;
        }

        public virtual string DefaultBaseConfigurationsFolder => Const.Directories.Configurations;
        public virtual string DefaultEnvironmentSpecificConfigurationsFolder => Path.Combine(DefaultBaseConfigurationsFolder, Environment);
        public virtual string DefaultSharedAcrossEnvironmentsConfigurationsFolder => Path.Combine(DefaultBaseConfigurationsFolder, Const.Directories.Shared);

        private bool _isInitialStaticConfigurationCompleted = false;

        #endregion General Properties

        private static readonly object _lock = new object();

        private IConfigurationBuilder? _b;
        protected IConfigurationBuilder _builder
        {
            get
            {
                if (_b == null)
                    lock (_lock)
                        _b ??= new ConfigurationBuilder();

                return _b;
            }
        }

        private IConfigurationRoot? _r;
        public IConfigurationRoot Root
        {
            get
            {
                if (_r == null)
                    lock (_lock)
                        _r ??= _builder.Build();

                return _r;
            }
        }

        private readonly ILogger _log;

        public ConfigContainer(ILogger log)
        {
            _log = log;

            if (!_isInitialStaticConfigurationCompleted)
                lock (_lock)
                {
                    if (!_isInitialStaticConfigurationCompleted)
                    {
                        AddConfigurationsFromDefaultLocations();
                        IncludeAdditionalOrRewriteConfigurationsAddedFromDefaultLocations();
                        _isInitialStaticConfigurationCompleted = true;
                    }
                }
        }

        public void InitializeInstance(IConfigurationBuilder builder)
        {
            lock (_lock)
            {
                _b = builder;
                _r = _builder.Build();
            }
        }

        #region Static Initial Configuration

        protected virtual void AddConfigurationsFromDefaultLocations()
        {
            lock (_lock)
            {
                _builder.AddEnvironmentVariables();
                AddConfigurationsFromDefaultDirectory();
            }
        }

        protected virtual void IncludeAdditionalOrRewriteConfigurationsAddedFromDefaultLocations() { } //UpsertConfigurations
        protected virtual void AddConfigurationsFromDefaultDirectory()
        {
            //!!! Preserve order. Cause override happens. !!!

            //A) All files of default directory Base files on top level of "Configurations" folder are added to configuration first
            AddConfigurationsFromDirectory(DefaultBaseConfigurationsFolder, optional: true);

            //B) Files in "Configuration\Shared" across envs folder overwrite Base files
            //Only specific sub directories of default directory: a) "{ENVIRONMENT}" specific directory b) "Shared" directory with configurations shared across environments
            AddConfigurationsFromDirectory(DefaultSharedAcrossEnvironmentsConfigurationsFolder, optional: true);

            //C) Files in Env specific folder ("Configurations\QA" or "Configurations\DEV") overwrite all previous
            if (!string.IsNullOrEmpty(Environment))
                AddConfigurationsFromDirectory(DefaultEnvironmentSpecificConfigurationsFolder, optional: true);
        }

        #endregion Static Initial Configuration

        #region Dynamic Configuration

        public IConfigContainer AddConfigurationSource(IConfigurationSource source)
        {
            lock (_lock)
            {
                _builder.Add(source);
                _r = _builder.Build();
            }

            return this;
        }

        public IConfigContainer RemoveConfigurationSource(IConfigurationSource source)
        {
            lock (_lock)
            {
                _builder.Sources.Remove(source);
                _r = _builder.Build();
            }

            return this;
        }

        public IConfigContainer Config(Action<IConfigurationBuilder> action) // TODO entityName for logging
        {
            lock (_lock)
            {
                action(_builder);
                _r = _builder.Build();
            }

            return this;
        }

        #endregion Dynamic Configuration

        #region Extract Configuration Values

        #region Get/TryGet


        /// <summary>
        /// Use to get single value, like string, int, double etc. Requires full path up to the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyPath"></param>
        /// <returns>Found value, if exists in configuration, otherwise throws exception.</returns>
        /// <exception cref="Exception"></exception>
        private T? GetRequiredValue<T>(string keyPath) //GetPrimitiveValue
        {
            RetrieveValue(keyPath, out T? value, shouldThrowExceptionIfAny: true);
            return value;
        }

        /// <summary>
        /// Use to try to get single value, like string, int, double etc. Requires full path up to the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyPath"></param>
        /// <param name="value"></param>
        /// <returns>True and found value, if exists in configuration, otherwise false with default value. Doesn't throw exception, if not found.</returns>
        private bool TryGetValue<T>(string keyPath, out T? value)
        {
            return RetrieveValue(keyPath, out value, shouldThrowExceptionIfAny: false);
        }

        private bool RetrieveValue<T>(string keyPath, out T? value, bool shouldThrowExceptionIfAny)
        {
            if (ContainsSection(keyPath, shouldThrowExceptionIfAny))
                try
                {
                    value = Root.GetValue<T?>(keyPath);
                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error($"{GetUnableToExtractConfigurationExceptionMessage(keyPath)} {ex.Message}");

                    if (shouldThrowExceptionIfAny)
                        throw new Exception(GetUnableToExtractConfigurationExceptionMessage(keyPath), ex);
                }

            value = default;
            return false;
        }

        /// <summary>
        /// Use to get whole entity with all deserialized fields and properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyPath"></param>
        /// <returns>Found entity value, if exists in configuration, otherwise throws exception.</returns>
        /// <exception cref="Exception"></exception>
        private T? GetRequiredEntity<T>(string keyPath)
        {
            RetrieveEntity(keyPath, out T? entity, shouldThrowExceptionIfAny: true);
            return entity;
        }

        /// <summary>
        /// Use to try to get not single value, but whole entity (class) with one or more properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyPath"></param>
        /// <param name="value"></param>
        /// <returns>True and found entity value, if exists in configuration, otherwise false with default entity value. Doesn't throw exception, if not found.</returns>
        private bool TryGetEntity<T>(string keyPath, out T? entity)
        {
            return RetrieveEntity(keyPath, out entity, shouldThrowExceptionIfAny: false);
        }

        private bool RetrieveEntity<T>(string keyPath, out T? entity, bool shouldThrowExceptionIfAny)
        {
            if (ContainsSection(keyPath, shouldThrowExceptionIfAny))
                try
                {

                    var section = Root.GetSection(keyPath);
                    entity = section.Get<T>();
                    return true;

                }
                catch (Exception ex)
                {
                    _log.Error($"{GetUnableToExtractConfigurationExceptionMessage(keyPath)} {ex.Message}");

                    if (shouldThrowExceptionIfAny)
                        throw new Exception(GetUnableToExtractConfigurationExceptionMessage(keyPath), ex);
                }

            entity = default;
            return false;
        }

        public T? Get<T>(string keyPath)
        {
            Type type = typeof(T);
            Type? underlyingType = Nullable.GetUnderlyingType(type);

            if (type.IsPrimitive || type == typeof(string) || underlyingType?.IsPrimitive == true || underlyingType == typeof(string))
                return GetRequiredValue<T>(keyPath);
            else if (type.IsClass || typeof(IEnumerable).IsAssignableFrom(type) || underlyingType?.IsClass == true || typeof(IEnumerable).IsAssignableFrom(underlyingType))
                return GetRequiredEntity<T>(keyPath);

            throw new NotSupportedException($"Unsupported type '{type.Name}'. Use either GetRequiredValue or GetRequiredEntity methods directly.");
        }

        public bool TryGet<T>(string keyPath, out T? result)
        {
            Type type = typeof(T);
            Type? underlyingType = Nullable.GetUnderlyingType(type);

            if (type.IsPrimitive || type == typeof(string) || underlyingType?.IsPrimitive == true || underlyingType == typeof(string))
                return TryGetValue(keyPath, out result);
            else if (type.IsClass || typeof(IEnumerable).IsAssignableFrom(type) || underlyingType?.IsClass == true || typeof(IEnumerable).IsAssignableFrom(underlyingType))
                return TryGetEntity(keyPath, out result);

            throw new NotSupportedException($"Unsupported type '{type.Name}'. Use either TryGetValue or TryGetEntity methods directly");
        }

        #endregion Get/TryGet

        #region GetConnectionString/TryGetConnectionString

        public string GetConnectionString(string name)
        {
            RetrieveConnectionsString(name, out string? connectionString, shouldThrowExceptionIfAny: true);
            return connectionString!;
        }

        public bool TryGetConnectionString(string name, out string? connectionString)
        {
            return RetrieveConnectionsString(name, out connectionString, shouldThrowExceptionIfAny: false);
        }

        private bool RetrieveConnectionsString(string name, out string? connectionString, bool shouldThrowExceptionIfAny)
        {
            string keyPath = $"{Const.Sections.ConnectionStrings}:{name}";

            if (ContainsSection(keyPath, shouldThrowExceptionIfAny))
                try
                {
                    connectionString = Root.GetConnectionString(name);
                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error($"{GetUnableToExtractConfigurationExceptionMessage(keyPath)} {ex.Message}");

                    if (shouldThrowExceptionIfAny)
                        throw new Exception(GetUnableToExtractConfigurationExceptionMessage(keyPath), ex);
                }

            connectionString = null;
            return false;
        }

        #endregion GetConnectionString/TryGetConnectionString

        #region GetEnvironmentVariable/TryGetEnvironmentVariable

        public string? GetEnvironmentVariable(string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            RetrieveEnvironmentVariable(name, out string? envVariable, shouldThrowExceptionIfAny: true, stringComparison);
            return envVariable;
        }

        public T? GetEnvironmentVariable<T>(string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            RetrieveEnvironmentVariable(name, out string? envVariable, shouldThrowExceptionIfAny: true, stringComparison);

            try
            {
                return (T)Convert.ChangeType(envVariable, typeof(T));
            }
            catch (Exception ex)
            {
                string message = GetUnableToConvertEnvironmentVariableExceptionMessage(name, envVariable, typeof(T));
                _log.Error(message);
                throw new Exception(message, ex);
            }
        }

        public bool TryGetEnvironmentVariable(string name, out string? envVariable, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            return RetrieveEnvironmentVariable(name, out envVariable, shouldThrowExceptionIfAny: false, stringComparison);
        }

        public bool TryGetEnvironmentVariable<T>(string name, out T? envVariable, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            if (!RetrieveEnvironmentVariable(name, out string? envVariableValue, shouldThrowExceptionIfAny: false, stringComparison))
            {
                envVariable = default;
                return false;
            }

            try
            {
                envVariable = (T)Convert.ChangeType(envVariableValue, typeof(T));
                return true;
            }
            catch (Exception ex)
            {
                string message = GetUnableToConvertEnvironmentVariableExceptionMessage(name, envVariableValue, typeof(T));
                _log.Error(message);
                throw new Exception(message, ex);
            }
        }

        private bool RetrieveEnvironmentVariable(string name, out string? envVariable, bool shouldThrowExceptionIfAny, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            List<string?> envVariablesWithName = GetEnvironmentVariables(name, stringComparison);

            if (envVariablesWithName.Count == 1)
            {
                envVariable = envVariablesWithName[0];
                return true;
            }
            else if (envVariablesWithName.Count > 1)
            {
                string message = $"Several environment variables were found using name '{name}': [{string.Join(",", envVariablesWithName)}]. Comparison option used: 'StringComparison.{stringComparison}'.";
                _log.Error(message);
                throw new Exception(message);
            }
            else
            {
                string message = $"Environment variable '{name}' was not found. Comparison option used: 'StringComparison.{stringComparison}'.";

                if (shouldThrowExceptionIfAny)
                {
                    _log.Error(message);
                    throw new Exception(message);
                }
                else
                {
                    _log.Warning(message);
                    envVariable = null;
                    return false;
                }
            }
        }

        #endregion GetEnvironmentVariable/TryGetEnvironmentVariable

        #region Bind/TryBind

        public T? Bind<T>(string? keyPath = null)
        {
            T? bindToEntity = default;
            RetrieveBind<T>(out bindToEntity, shouldThrowExceptionIfAny: true, keyPath);

            return bindToEntity;
        }

        public bool TryBind<T>(out T? bindToEntity, string? keyPath = null)
        {
            return RetrieveBind<T?>(out bindToEntity, shouldThrowExceptionIfAny: false, keyPath);
        }

        private bool RetrieveBind<T>(out T? bindToEntity, bool shouldThrowExceptionIfAny, string? keyPath = null)
        {
            try
            {
                bindToEntity = Activator.CreateInstance<T>();
            }
            catch (Exception ex)
            {
                _log.Error(GetUnableToBindConfigurationExceptionMessage(typeof(T), keyPath));
                throw new Exception(GetUnableToBindConfigurationExceptionMessage(typeof(T), keyPath), ex);
            }

            if (!TryBind(bindToEntity!, shouldThrowExceptionIfAny: shouldThrowExceptionIfAny, keyPath))
            {
                bindToEntity = default;
                return false;
            }
            else
                return true;
        }

        public void Bind(object bindToEntity, string? keyPath = null)
        {
            TryBind(bindToEntity, shouldThrowExceptionIfAny: true, keyPath);
        }

        public bool TryBind(object bindToEntity, string? keyPath = null)
        {
            return TryBind(bindToEntity, shouldThrowExceptionIfAny: false, keyPath);
        }

        private bool TryBind(object bindToEntity, bool shouldThrowExceptionIfAny, string? keyPath = null)
        {
            if (!string.IsNullOrEmpty(keyPath))
            {
                if (ContainsSection(keyPath, shouldThrowExceptionIfAny))
                {
                    try
                    {
                        Root.Bind(keyPath, bindToEntity);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        LogError(ex);
                    }
                }
            }
            else
            {
                try
                {
                    Root.Bind(bindToEntity);
                    return true;

                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }

            return false;

            void LogError(Exception ex)
            {
                _log.Error($"{GetUnableToBindConfigurationExceptionMessage(bindToEntity.GetType(), keyPath)}. {ex.Message}");

                if (shouldThrowExceptionIfAny)
                    throw new Exception(GetUnableToBindConfigurationExceptionMessage(bindToEntity.GetType(), keyPath), ex);
            }
        }

        #endregion Bind/TryBind

        #endregion Extract Configuration Values

        #region Auxiliary Methods

        public bool ContainsSection(string keyPath)
        {
            var sections = GetAllSections();

            if (sections.Select(s => s.Path).Any(x => x.Equals(keyPath, StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        }

        #endregion Auxiliary Methods

        #region Inner Methods

        public List<IConfigurationSection> GetAllSections()
        {
            var sections = new List<IConfigurationSection>();
            var rootChildren = Root.GetChildren();

            foreach (var section in rootChildren)
            {
                sections.Add(section);
                GetSubSections(section, sections);
            }

            return sections;
        }

        private void GetSubSections(IConfigurationSection section, List<IConfigurationSection> sections)
        {
            var subSections = section.GetChildren();
            foreach (var subSection in subSections)
            {
                sections.Add(subSection);
                GetSubSections(subSection, sections);
            }
        }

        public IConfigContainer AddConfigurationsFromDirectory(string directoryPath, bool optional = false)
        {
            if (Directory.Exists(directoryPath))
            {
                var directory = new DirectoryInfo(directoryPath);
                return AddConfigurationsFromDirectory(directory);
            }

            if (!optional) //If directrory doesnt exist and is not optional, throw exception
                throw new DirectoryNotFoundException($"No such directory with configurations exists: '{directoryPath}'.");

            return this;
        }

        public IConfigContainer AddConfigurationsFromDirectory(DirectoryInfo directory)
        {
            var files = GetConfigurationFilesRecursively(directory).ToArray();
            AddConfigurationsFromFiles(files);
            return this;
        }

        private IConfigContainer AddConfigurationsFromFiles(params FileInfo[] files)
        {
            lock (_lock)
            {
                foreach (var file in files)
                {
                    if (file.Exists)
                    {
                        if (!file.Name.StartsWith("ignore", StringComparison.OrdinalIgnoreCase))
                        {
                            if (file.Extension.Equals(FileExtension.Json.GetDescriptionAttributeValue(), StringComparison.OrdinalIgnoreCase))
                            {
                                //_builder.AddJsonFile(file.FullName, optional: true, reloadOnChange: true); //TODO Add Config Option to Use either Standard or Custom Serializer
                                _builder.AddJsonFileWithCustomJsonSerializerForStringNullAndEnumerableEmpty(file.FullName, optional: true, reloadOnChange: true);
                            }
                            else if (file.Extension.Equals(FileExtension.Xml.GetDescriptionAttributeValue(), StringComparison.OrdinalIgnoreCase))
                            {
                                //_builder.AddXmlFile(file.FullName, optional: true, reloadOnChange: true); //TODO Add Config Option to Use either Standard or Custom Serializer
                                _builder.AddXmlFileWithCustomXmlSerializerForStringNullAndEnumerableEmpty(file.FullName, optional: true, reloadOnChange: true);
                            }
                            else if (file.Extension.Equals(FileExtension.Ini.GetDescriptionAttributeValue(), StringComparison.OrdinalIgnoreCase))
                            {
                                _builder.AddIniFile(file.FullName, optional: true, reloadOnChange: true);
                            }
                            else
                                _log.Warning($"Unsupported file extension '{file.Extension}'. File '{file.FullName}' is not added to configuration.");
                        }
                        else
                            _log.Debug($"Ignored file is not added to configuration: '{file.FullName}'.");
                    }
                    else
                    {
                        _log.Warning($"File '{file.FullName}' does not exist.");
                    }
                }

                _r = _builder.Build();

                return this;
            }
        }

        private IEnumerable<FileInfo> GetConfigurationFilesRecursively(DirectoryInfo directory)
        {
            var files = new List<FileInfo>();

            if (!directory.Name.StartsWith("ignore", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var file in directory.GetFiles())
                {
                    if (!file.Name.StartsWith("ignore", StringComparison.OrdinalIgnoreCase))
                        files.Add(file);
                    else
                        _log.Debug($"File is not added to configuration: '{file.FullName}'.");
                }

                foreach (var subDirectory in directory.GetDirectories())
                {
                    var subDirectoryFiles = GetConfigurationFilesRecursively(subDirectory);
                    files.AddRange(subDirectoryFiles);
                }
            }
            else
                IoHelper.GetFilesInDirectory(directory.FullName).ForEach(f => _log.Debug($"Ignored file is not added to configuration: '{f.FullName}'."));

            return files;
        }

        private List<string?> GetEnvironmentVariables(string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            List<string?> envVariablesWithName = new();

            var allEnvVariables = System.Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry entry in allEnvVariables)
                if (string.Equals(entry.Key.ToString(), name, stringComparison))
                    envVariablesWithName.Add(entry.Value?.ToString());

            return envVariablesWithName;
        }

        private string GetSectionNotFoundExceptionMessage(string keyPath)
        {
            return $"Unable to find within configurations any section using key path '{keyPath}'.";
        }

        private string GetUnableToExtractConfigurationExceptionMessage(string keyPath)
        {
            return $"Unable to extract configuration using key path '{keyPath}'.";
        }

        private string GetUnableToBindConfigurationExceptionMessage(Type type, string? keyPath = null)
        {
            string baseMessage = $"Unable to bind configurations to entity of type '{type.FullName}'";

            if (string.IsNullOrEmpty(keyPath))
                return $"{baseMessage}.";
            else
                return $"{baseMessage} using key path '{keyPath}'.";
        }

        private string GetUnableToConvertEnvironmentVariableExceptionMessage(string name, string envVariableValue, Type type)
        {
            return $"Unable to convert environment variable '{name}' with value '{envVariableValue}' to type '{type.FullName}'.";
        }

        private bool ContainsSection(string keyPath, bool shouldThrowExceptionIfAny)
        {
            bool containsSection = ContainsSection(keyPath);

            if (!containsSection)
            {
                if (shouldThrowExceptionIfAny)
                {
                    _log.Error($"{GetSectionNotFoundExceptionMessage(keyPath)}");
                    throw new Exception(GetSectionNotFoundExceptionMessage(keyPath));
                }
                else
                    _log.Warning($"{GetSectionNotFoundExceptionMessage(keyPath)}");
            }

            return containsSection;
        }

        public void Rebuild()
        {
            _r ??= _builder.Build();
        }

        #endregion Inner Methods
    }
}
