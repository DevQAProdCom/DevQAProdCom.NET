using System.Collections.Concurrent;
using DevQAProdCom.NET.Configurations.OperativeClasses;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Moq;
using Tests.DevQAProdCom.NET.Configurations.Constants;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Enumerations;

namespace Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.BaseClasses
{
    internal class BaseTest
    {
        protected readonly ConcurrentBag<string> _tempDirectories = new ConcurrentBag<string>();

        protected Mock<ConfigContainer> ArrangeIConfigContainer(Env env, out string tempDirectory, params string[] includeFilesWithNames)
        {
            var mockLogger = new Mock<ILogger>();
            var configContainer = new Mock<ConfigContainer>(mockLogger.Object) { CallBase = true };
            tempDirectory = $"{Const.Directories.ConfigurationsTemp} {Guid.NewGuid().ToString().Substring(0, 5)}";
            configContainer.Setup(c => c.DefaultBaseConfigurationsFolder).Returns(tempDirectory);

            IoHelper.CopyDirectory(Const.Directories.Configurations, tempDirectory, (filePath) => includeFilesWithNames
            .Any(fileName => fileName == Path.GetFileName(filePath)) || Path.GetFileName(filePath) == Const.Files.EnvironmentJson);
            JsonHelper.UpdateJsonValue(Path.Combine(tempDirectory, Const.Files.EnvironmentJson), Const.ENVIRONMENT.ToLower(), env.ToString());

            _tempDirectories.Add(tempDirectory);

            return configContainer;
        }

        protected Mock<ConfigContainer> ArrangeNoSpecificIConfigContainer()
        {
            return ArrangeIConfigContainer(Env.QA, out string tempDirectory, Const.Files.JSON_AppSettingsTestsDev);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            foreach (var tempDirectory in _tempDirectories)
                Directory.Delete(tempDirectory, true);
        }
    }
}
