using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Hooks;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Models;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.TestData;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Tests
{
    [Order(1)]
    internal class Tests_LoggingRecordsPresenceOnPreconditionsTestsSuite2 : BaseTest
    {
        #region [SuiteStart]

        [Test]
        public void Should_Log_Contain_Suite2_SuiteStart_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.TestSuiteName == SharedTestConstants.Suite2FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteStart.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestSuiteName == SharedTestConstants.Suite2FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteStart.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.LogEntryOrderNumber)
                        .Excluding(x => x.StartTime));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.LogEntryOrderNumber.Should().ContainAny("0", "1", "2");
                actualFullLogRecord?.StartTime.Should().MatchRegex(DateTimeRegex);
            }
        }

        #endregion [SuiteStart]

        #region [OneTimeSetUp]

        [Test]
        public void Should_Log_Contain_Suite2_PerScenarioBaseTest_OneTimeSetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a496a7f4 && x.TestSuiteName == SharedTestConstants.Suite2FullNameWithPreconditionTests);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a496a7f4 && x.TestSuiteName == SharedTestConstants.Suite2FullNameWithPreconditionTests);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.LogEntryOrderNumber)
                        .Excluding(x => x.TestSuiteId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestSuiteId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.LogEntryOrderNumber.Should().ContainAny("0", "1", "2");
            }
        }

        [Test]
        public void Should_Log_Contain_Suite2_ClassWithTest_OneTimeSetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a00a2624);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a00a2624);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                      .Excluding(x => x.ThreadId)
                      .Excluding(x => x.LogEntryOrderNumber)
                      .Excluding(x => x.TestSuiteId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestSuiteId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.LogEntryOrderNumber.Should().ContainAny("0", "1", "2");
            }
        }

        #endregion [OneTimeSetUp]

        #region Suite2_Test_Error_Warning_Fatal_AssertFail

        #region [TestStart] Suite2_Test_Error_Warning_Fatal_AssertFail

        [Test]
        public void Should_Log_Contain_Suite2_Test_Error_Warning_Fatal_AssertFail_TestStart_Data()
        {
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestStart.ToString());
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestStart.ToString());

            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId)
                        .Excluding(x => x.StartTime)
                        .Excluding(x => x.LogRecordId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.StartTime.Should().MatchRegex(DateTimeRegex);
            }
        }

        #endregion [TestStart] Suite2_Test_Error_Warning_Fatal_AssertFail

        #region [SetUp] Suite2_Test_Error_Warning_Fatal_AssertFail

        [Test]
        public void Should_Log_Contain_Suite2_Test_Error_Warning_Fatal_AssertFail_PerScenarioBaseTest_SetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3eea371 && x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3eea371 && x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite2_Test_Error_Warning_Fatal_AssertFail_ClassWithTest_SetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.aacfe03b && x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.aacfe03b && x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [SetUp] Suite2_Test_Error_Warning_Fatal_AssertFail

        #region [Execution] Suite2_Test_Error_Warning_Fatal_AssertFail

        [Test]
        public void Should_Log_Contain_Suite2_Test_Error_Warning_Fatal_AssertFail_TestExecution_Data()
        {
            //GIVEN
            var expectedWarningLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a34e3e88);
            var expectedErrorLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a4359598);
            var expectedFatalLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.cb31b274);

            //WHEN
            var actualWarningFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a34e3e88);
            var actualErrorFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a4359598);
            var actualFatalFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.cb31b274);

            //THEN
            using (new AssertionScope())
            {
                //Warning Type Log Message
                actualWarningFullLogRecord.Should().BeEquivalentTo(expectedWarningLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualWarningFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualWarningFullLogRecord?.ThreadId.Should().HaveValue();
                actualWarningFullLogRecord?.TestId.Should().MatchRegex(IdPattern);

                //Error Type Log Message
                actualErrorFullLogRecord.Should().BeEquivalentTo(expectedErrorLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualErrorFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualErrorFullLogRecord?.ThreadId.Should().HaveValue();
                actualErrorFullLogRecord?.TestId.Should().MatchRegex(IdPattern);

                //Fatal Type Log Message
                actualFatalFullLogRecord.Should().BeEquivalentTo(expectedFatalLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFatalFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFatalFullLogRecord?.ThreadId.Should().HaveValue();
                actualFatalFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [Execution] Suite2_Test_Error_Warning_Fatal_AssertFail

        #region [TearDown] Suite2_Test_Error_Warning_Fatal_AssertFail

        [Test]
        public void Should_Log_Contain_Suite2_Test_Error_Warning_Fatal_AssertFail_ClassWithTest_TearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a36b092c && x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a36b092c && x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite2_Test_Error_Warning_Fatal_AssertFail_PerScenarioBaseTest_TearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a0f4db90 && x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a0f4db90 && x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [TearDown] Suite2_Test_Error_Warning_Fatal_AssertFail

        #region [TestExecutionEnd] Suite2_Test_Error_Warning_Fatal_AssertFail

        [Test]
        public void Should_Log_Contain_Suite2_Test_Error_Warning_Fatal_AssertFail_TestExecutionEnd_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite2TestErrorWarningFatalAssertFail
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId)
                        .Excluding(x => x.EndTime)
                        .Excluding(x => x.Duration)
                        .Excluding(x => x.StackTrace));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.EndTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.Duration.Should().MatchRegex(TimeSpanRegex);
                actualFullLogRecord?.StackTrace.Should().Contain(expectedLogRecord.StackTrace);
            }
        }

        #endregion [TestExecutionEnd] Suite2_Test_Error_Warning_Fatal_AssertFail

        #endregion Suite2_Test_Error_Warning_Fatal_AssertFail

        #region Suite2_Test_Skipped

        #region [TestExecutionEnd] Suite2_Test_Skipped

        [Test]
        public void Should_Log_Contain_Suite2_Test_Skipped_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.TestMethodName == SharedTestConstants.Suite2TestSkipped
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite2TestSkipped
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId)
                        .Excluding(x => x.EndTime)
                        .Excluding(x => x.Duration));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.EndTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.Duration.Should().MatchRegex(TimeSpanRegex);
            }
        }

        #endregion [TestExecutionEnd] Suite2_Test_Skipped

        #endregion Suite2_Test_Skipped

        #region [OneTimeTearDown]

        [Test]
        public void Should_Log_Contain_Suite2_ClassWithTest_OneTimeTearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.f008a311);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.f008a311);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestSuiteId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestSuiteId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite2_PerScenarioBaseTest_OneTimeTearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a2320c90 && x.TestSuiteName == SharedTestConstants.Suite2FullNameWithPreconditionTests);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a2320c90 && x.TestSuiteName == SharedTestConstants.Suite2FullNameWithPreconditionTests);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.TestSuiteId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestSuiteId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [OneTimeTearDown]

        #region [SuiteEnd]

        [Test]
        public void Should_Log_Contain_Suite2_SuiteEnd_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite2.Single(x => x.TestSuiteName == SharedTestConstants.Suite2FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestSuiteName == SharedTestConstants.Suite2FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteEnd.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.EndTime)
                        .Excluding(x => x.Duration));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.EndTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.Duration.Should().MatchRegex(TimeSpanRegex);
            }
        }

        #endregion [SuiteEnd]
    }
}
