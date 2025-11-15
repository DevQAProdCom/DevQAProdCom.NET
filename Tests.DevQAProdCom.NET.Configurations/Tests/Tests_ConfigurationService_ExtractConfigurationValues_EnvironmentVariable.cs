using DevQAProdCom.NET.Configurations.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Configurations.TestData;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.BaseClasses;

namespace Tests.DevQAProdCom.NET.Configurations.Tests
{
    internal class Tests_ConfigurationService_ExtractConfigurationValues_EnvironmentVariable : BaseTest
    {
        private IConfigContainer? _configContainer;
        private IConfigContainer ConfigContainer => _configContainer ??= ArrangeNoSpecificIConfigContainer().Object;

        #region GetEnvironmentVariable

        #region StringValue

        [Test]
        public void Should_GetEnvironmentVariable_Existing_StringValue_StringComparison_OrdinalIgnoreCase_ByDefault()
        {
            //Arrange
            (string ExpectedEnvVarName, string ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithStringType();

            //Act
            var actualValue = ConfigContainer.GetEnvironmentVariable(ExpectedEnvVarName.ToLower());

            //Assert
            actualValue.Should().Be(ExpectedEnvVarValue);
        }

        [Test]
        public void Should_GetEnvironmentVariable_Existing_StringValue_StringComparison_Ordinal()
        {
            //Arrange
            (string ExpectedEnvVarName, string ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithStringType();

            //Act
            var actualValue = ConfigContainer.GetEnvironmentVariable(ExpectedEnvVarName, StringComparison.Ordinal);

            //Assert
            actualValue.Should().Be(ExpectedEnvVarValue);
        }

        [Test]
        public void Should_GetEnvironmentVariable_Not_Existing_StringValue_StringComparison_Ordinal_Throw_Exception()
        {
            //Arrange
            (string ExpectedEnvVarName, string ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithStringType();

            //Act
            Action act = () => ConfigContainer.GetEnvironmentVariable(ExpectedEnvVarName.ToUpper(), StringComparison.Ordinal);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetEnvironmentVariableNotFoundExceptionMessage(ExpectedEnvVarName, StringComparison.Ordinal));
        }

        #endregion StringValue

        #region TValue

        [Test]
        public void Should_GetEnvironmentVariable_Existing_TValue_StringComparison_OrdinalIgnoreCase_ByDefault()
        {
            //Arrange
            (string ExpectedEnvVarName, double ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithDoubleType();

            //Act
            var actualValue = ConfigContainer.GetEnvironmentVariable<double>(ExpectedEnvVarName.ToLower());

            //Assert
            actualValue.Should().Be(ExpectedEnvVarValue);
        }

        [Test]
        public void Should_GetEnvironmentVariable_Existing_TValue_StringComparison_Ordinal()
        {
            //Arrange
            (string ExpectedEnvVarName, double ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithDoubleType();

            //Act
            double actualValue = ConfigContainer.GetEnvironmentVariable<double>(ExpectedEnvVarName, StringComparison.Ordinal);

            //Assert
            actualValue.Should().Be(ExpectedEnvVarValue);
        }

        [Test]
        public void Should_GetEnvironmentVariable_Not_Existing_TValue_StringComparison_Ordinal_Throw_Exception()
        {
            //Arrange
            (string ExpectedEnvVarName, double ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithDoubleType();

            //Act
            Action act = () => ConfigContainer.GetEnvironmentVariable<double?>(ExpectedEnvVarName.ToUpper(), StringComparison.Ordinal);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetEnvironmentVariableNotFoundExceptionMessage(ExpectedEnvVarName, StringComparison.Ordinal));
        }

        [Test]
        public void Should_GetEnvironmentVariable_Existing_TValue_IncorrectType_StringComparison_Ordinal_Throw_Exception()
        {
            //Arrange
            (string ExpectedEnvVarName, double ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithDoubleType();

            //Act
            Action act = () => ConfigContainer.GetEnvironmentVariable<int>(ExpectedEnvVarName, StringComparison.Ordinal);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetUnableToConvertEnvironmentVariableExceptionMessage(ExpectedEnvVarName, ExpectedEnvVarValue.ToString(), typeof(int)));
        }

        #endregion TValue

        #endregion GetEnvironmentVariable

        #region TryGetEnvironmentVariable

        #region StringValue

        [Test]
        public void Should_TryGetEnvironmentVariable_Existing_StringValue_StringComparison_OrdinalIgnoreCase_ByDefault()
        {
            //Arrange
            (string ExpectedEnvVarName, string ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithStringType();

            //Act
            bool actualIsOperationSuccessful = ConfigContainer.TryGetEnvironmentVariable(ExpectedEnvVarName.ToLower(), out string? actualValue);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().Be(ExpectedEnvVarValue);
            }
        }

        [Test]
        public void Should_TryGetEnvironmentVariable_Existing_StringValue_StringComparison_Ordinal()
        {
            //Arrange
            (string ExpectedEnvVarName, string ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithStringType();

            //Act
            bool actualIsOperationSuccessful = ConfigContainer.TryGetEnvironmentVariable(ExpectedEnvVarName, out string? actualValue, StringComparison.Ordinal);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().Be(ExpectedEnvVarValue);
            }
        }

        [Test]
        public void Should_TryGetEnvironmentVariable_Not_Existing_StringValue_StringComparison_Ordinal_Not_Throw_Exception()
        {
            //Arrange
            (string ExpectedEnvVarName, string ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithStringType();

            //Act
            Func<bool> func = () => ConfigContainer.TryGetEnvironmentVariable(ExpectedEnvVarName.ToUpper(), out string? actualValue, StringComparison.Ordinal);

            //Assert
            using (new AssertionScope())
            {
                func.Should().NotThrow();

                bool actualIsOperationSuccessful = ConfigContainer.TryGetEnvironmentVariable(ExpectedEnvVarName.ToUpper(), out string? actualValue, StringComparison.Ordinal);
                actualIsOperationSuccessful.Should().BeFalse();
                actualValue.Should().Be(null);
            }
        }

        [Test]
        public void Should_TryGetEnvironmentVariable_Existing_TValue_IncorrectType_StringComparison_Ordinal_Throw_Exception()
        {
            //Arrange
            (string ExpectedEnvVarName, double ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithDoubleType();

            //Act
            Action act = () => ConfigContainer.TryGetEnvironmentVariable<int>(ExpectedEnvVarName, out int actualValue, StringComparison.Ordinal);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetUnableToConvertEnvironmentVariableExceptionMessage(ExpectedEnvVarName, ExpectedEnvVarValue.ToString(), typeof(int)));
        }

        #endregion StringValue

        #region TValue

        [Test]
        public void Should_TryGetEnvironmentVariable_Existing_TValue_StringComparison_OrdinalIgnoreCase_ByDefault()
        {
            //Arrange
            (string ExpectedEnvVarName, double ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithDoubleType();

            //Act
            bool actualIsOperationSuccessful = ConfigContainer.TryGetEnvironmentVariable<double>(ExpectedEnvVarName.ToLower(), out double actualValue);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().Be(ExpectedEnvVarValue);
            }
        }

        [Test]
        public void Should_TryGetEnvironmentVariable_Existing_TValue_StringComparison_Ordinal()
        {
            //Arrange
            (string ExpectedEnvVarName, double ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithDoubleType();

            //Act
            bool actualIsOperationSuccessful = ConfigContainer.TryGetEnvironmentVariable<double>(ExpectedEnvVarName, out double actualValue, StringComparison.Ordinal);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().Be(ExpectedEnvVarValue);
            }
        }

        [Test]
        public void Should_TryGetEnvironmentVariable_Not_Existing_TValue_StringComparison_Ordinal_Not_Throw_Exception()
        {
            //Arrange
            (string ExpectedEnvVarName, string ExpectedEnvVarValue) = Arrange_EnvironmentVariable_WithStringType();

            //Act
            Func<bool> func = () => ConfigContainer.TryGetEnvironmentVariable<double?>(ExpectedEnvVarName.ToUpper(), out double? actualValue, StringComparison.Ordinal);

            //Assert
            using (new AssertionScope())
            {
                func.Should().NotThrow();

                bool actualIsOperationSuccessful = ConfigContainer.TryGetEnvironmentVariable(ExpectedEnvVarName.ToUpper(), out double? actualValue, StringComparison.Ordinal);
                actualIsOperationSuccessful.Should().BeFalse();
                actualValue.Should().Be(null);
            }
        }

        #endregion TValue

        #endregion TryGetEnvironmentVariable

        private (string ExpectedEnvVarName, string ExpectedEnvVarValue) Arrange_EnvironmentVariable_WithStringType()
        {
            (string name, string value) = ExpectedValues.GetEnvironmentVariableWithStringType();
            Environment.SetEnvironmentVariable(name, value);
            return (name, value);
        }

        private (string ExpectedEnvVarName, double ExpectedEnvVarValue) Arrange_EnvironmentVariable_WithDoubleType()
        {
            (string name, double value) = ExpectedValues.GetEnvironmentVariableWithDoubleType();
            Environment.SetEnvironmentVariable(name, value.ToString());
            return (name, value);
        }
    }
}
