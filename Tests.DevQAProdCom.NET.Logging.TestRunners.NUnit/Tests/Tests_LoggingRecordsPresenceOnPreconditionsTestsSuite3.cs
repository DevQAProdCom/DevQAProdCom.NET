using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Hooks;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Models;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.TestData;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Tests
{
    [Order(1)]
    internal class Tests_LoggingRecordsPresenceOnPreconditionsTestsSuite3 : BaseTest
    {
        #region [SuiteStart]

        [Test]
        public void Should_Log_Contain_Suite3_SuiteStart_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite3.Single(x => x.TestSuiteName == SharedTestConstants.Suite3FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteStart.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestSuiteName == SharedTestConstants.Suite3FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteStart.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.StartTime));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.StartTime.Should().MatchRegex(DateTimeRegex);
            }
        }

        #endregion [SuiteStart]

        #region Suite3_Test_Skipped

        #region [TestExecutionEnd] Suite3_Test_Skipped

        [Test]
        public void Should_Log_Contain_Suite3_Test_Skipped_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite3.Single(x => x.TestMethodName == SharedTestConstants.Suite3TestSkipped
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite3TestSkipped
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

        #endregion [TestExecutionEnd] Suite3_Test_Skipped

        #endregion Suite3_Test_Skipped

        #region [SuiteEnd]

        [Test]
        public void Should_Log_Contain_Suite3_SuiteEnd_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite3.Single(x => x.TestSuiteName == SharedTestConstants.Suite3FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestSuiteName == SharedTestConstants.Suite3FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteEnd.ToString());

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
