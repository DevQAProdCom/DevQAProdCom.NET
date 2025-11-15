using DevQAProdCom.NET.Configurations.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Configurations.Constants;
using Tests.DevQAProdCom.NET.Configurations.TestData;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.BaseClasses;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Enumerations;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Models;

namespace Tests.DevQAProdCom.NET.Configurations.Tests
{
    internal class Tests_ConfigurationService_ExtractConfigurationValues_ClassEntity : BaseTest
    {
        #region Get

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        public void Should_Get_Existing_In_Configurations_ClassEntity_From_Json_Ini_Files(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualClassEntity = configContainer.Get<UserModelExtended>(keyPath);

            //Assert
            actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetClassEntityUserModelExtended(env));
        }

        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        public void Should_Get_Existing_In_Configurations_ClassEntity_From_Xml_File(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualClassEntity = configContainer.Get<UserModelExtendedXml>(keyPath);

            //Assert
            actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetClassEntityUserModelExtendedXml(env));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.ClassEntity_Default_Null)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.ClassEntity_Default_Null)]
        public void Should_Get_Existing_In_Configurations_ClassEntity_Default_Null(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualClassEntity = configContainer.Get<UserModel>(keyPath);

            //Assert
            actualClassEntity.Should().BeNull();
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_Get_Not_Existing_In_Configurations_ClassEntity_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Action act = () => configContainer.Get<UserModel>(keyPath);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetSectionNotFoundExceptionMessage(keyPath));
        }

        #endregion Get

        #region TryGet

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        public void Should_TryGet_Existing_In_Configurations_ClassEntity_From_Json_Ini_Files(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out UserModelExtended? actualClassEntity);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetClassEntityUserModelExtended(env));
            }
        }

        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        public void Should_TryGet_Existing_In_Configurations_ClassEntity_From_Xml_File(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out UserModelExtendedXml? actualClassEntity);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetClassEntityUserModelExtendedXml(env));
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.ClassEntity_Default_Null)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.ClassEntity_Default_Null)]
        public void Should_TryGet_Existing_In_Configurations_ClassEntity_Default_Null(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out UserModel? actualClassEntity);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualClassEntity.Should().BeNull();
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_TryGet_Not_Existing_In_Configurations_ClassEntity_Not_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Func<bool> func = () => configContainer.TryGet(keyPath, out UserModel? actualClassEntity);

            //Assert
            using (new AssertionScope())
            {
                func.Should().NotThrow();

                bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out UserModel? actualClassEntity);
                actualIsOperationSuccessful.Should().BeFalse();
                actualClassEntity.Should().BeNull();
            }
        }

        #endregion TryGet

        #region Bind

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev)]
        public void Should_Bind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_With_Activator_Create_InstanceT_From_Json_File(Env env, string fileName)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualClassEntity = configContainer.Bind<AppSettingsModelJson>();

            //Assert
            actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetAppSettingsModel(env));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        public void Should_Bind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_With_Activator_Create_InstanceT_And_KeyPath_Parameter_From_Json_File(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualClassEntity = configContainer.Bind<UserModelExtended>(keyPath);

            //Assert
            actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetClassEntityUserModelExtended(env));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev)]
        public void Should_Bind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_With_Object_Parameter_From_Json_File(Env env, string fileName)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            AppSettingsModelJson actualClassEntity = new();
            configContainer.Bind(actualClassEntity);

            //Assert
            actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetAppSettingsModel(env));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        public void Should_Bind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_With_Object_And_KeyPath_Parameters_From_Json_File(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            UserModelExtended actualClassEntity = new();
            configContainer.Bind(actualClassEntity, keyPath);

            //Assert
            actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetClassEntityUserModelExtended(env));
        }

        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa)]
        public void Should_Bind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_From_Xml_File(Env env, string fileName)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualClassEntity = configContainer.Bind<AppSettingsModelXml>();

            //Assert
            actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetAppSettingsModelXml(env));
        }

        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa)]
        public void Should_Bind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_From_Ini_File(Env env, string fileName)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualClassEntity = configContainer.Bind<AppSettingsModelIni>();

            //Assert
            actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetAppSettingsModelIni(env));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_Bind_Using_ConfigurationKeyName_Attributes_Not_Existing_In_Configurations_ClassEntity_With_Activator_Create_InstanceT_And_KeyPath_Parameter_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Action act = () => configContainer.Bind<UserModelExtended>(keyPath);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetSectionNotFoundExceptionMessage(keyPath));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_Bind_Using_ConfigurationKeyName_Attributes_Not_Existing_In_Configurations_ClassEntity_With_Object_And_KeyPath_Parameters_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            UserModelExtended actualClassEntity = new();
            Action act = () => configContainer.Bind(actualClassEntity, keyPath);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetSectionNotFoundExceptionMessage(keyPath));
        }

        #endregion Bind

        #region TryBind

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev)]
        public void Should_TryBind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_With_Activator_Create_InstanceT_From_Json_File(Env env, string fileName)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryBind<AppSettingsModelJson>(out AppSettingsModelJson actualClassEntity);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetAppSettingsModel(env));
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        public void Should_TryBind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_With_Activator_Create_InstanceT_And_KeyPath_Parameter_From_Json_File(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryBind<UserModelExtended>(out UserModelExtended actualClassEntity, keyPath);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetClassEntityUserModelExtended(env));
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev)]
        public void Should_TryBind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_With_Object_Parameter_From_Json_File(Env env, string fileName)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            AppSettingsModelJson actualClassEntity = new();
            bool actualIsOperationSuccessful = configContainer.TryBind(out actualClassEntity);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetAppSettingsModel(env));
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.ClassEntity_UserModelExtended)]
        public void Should_TryBind_Using_ConfigurationKeyName_Attributes_Existing_In_Configurations_ClassEntity_With_Object_And_KeyPath_Parameters_From_Json_File(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            UserModelExtended actualClassEntity = new();
            bool actualIsOperationSuccessful = configContainer.TryBind(out actualClassEntity, keyPath);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualClassEntity.Should().BeEquivalentTo(ExpectedValues.GetClassEntityUserModelExtended(env));
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_TryBind_Using_ConfigurationKeyName_Attributes_Not_Existing_In_Configurations_ClassEntity_With_Activator_Create_InstanceT_And_KeyPath_Parameter_Not_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Func<bool> func = () => configContainer.TryBind<UserModelExtended>(out UserModelExtended actualClassEntity, keyPath);

            //Assert
            using (new AssertionScope())
            {
                func.Should().NotThrow();

                bool actualIsOperationSuccessful = configContainer.TryBind<UserModelExtended>(out UserModelExtended actualClassEntity, keyPath);
                actualIsOperationSuccessful.Should().BeFalse();
                actualClassEntity.Should().Be(default);
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_TryBind_Using_ConfigurationKeyName_Attributes_Not_Existing_In_Configurations_ClassEntity_With_Object_And_KeyPath_Parameters_Not_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;
            UserModelExtended expectedClassEntity = new() { Email = ExpectedValues.GetStringValue(env)};

            //Act
            UserModelExtended actualClassEntity = new() { Email = ExpectedValues.GetStringValue(env) };
            Func<bool> func = () => configContainer.TryBind(actualClassEntity, keyPath);

            //Assert
            using (new AssertionScope())
            {
                func.Should().NotThrow();

                bool actualIsOperationSuccessful = configContainer.TryBind(actualClassEntity, keyPath);
                actualIsOperationSuccessful.Should().BeFalse();
                actualClassEntity.Should().BeEquivalentTo(expectedClassEntity);
            }
        }

        #endregion TryBind
    }
}
