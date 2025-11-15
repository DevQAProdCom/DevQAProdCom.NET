using ApplicationName.QA.TestsBasis.Ui.PageServices;

namespace Tests.DevQAProdCom.NET.UI.BaseTestClasses
{
    internal class ExecuteJavaScript_PerScenarioBaseTest : PerScenarioBaseTest
    {
        [ThreadStatic] protected static TestPage2Service? _testPage2Service;

        protected const string ExpectedBackgroundColorBeforeScriptExecution = "154, 205, 50";
        protected const string ExpectedBackgroundColorAfterScriptExecution = "0, 255, 255";
        protected const string BackgroundColorPropertyName = "background-color";
        protected const int ExpectedBoundingClientRectWidth = 350;
        protected const int ExpectedBoundingClientRectHeight = 50;

        [ThreadStatic] protected static KeyValuePair<string, object> _visibleButtonArgument;
        protected static TimeSpan _expectedTimeout = TimeSpan.FromSeconds(10);
        protected KeyValuePair<string, object> _timeoutArgument = new("timeoutArgument", _expectedTimeout.TotalMilliseconds);
    }
}
