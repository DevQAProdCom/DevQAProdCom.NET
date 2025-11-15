using ApplicationName.QA.TestsBasis.Ui.Pages;
using ApplicationName.QA.TestsBasis.Ui.PageServices;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiElement_KeyboardBehavior : PerScenarioBaseTest
    {
        [ThreadStatic] private static KeyboardTestPage _keyboardTestPage;
        private const string CopyPasteValue = "CopyPasteValue";

        [SetUp]
        public void SetUp()
        {
            _keyboardTestPage = UiInteractor.Interact<KeyboardPageService>()._page;
        }

        [TestCase(Key.Null, "Unidentified", "0")] //Playwright Unknown key: "Unidentified"
        [TestCase(Key.Cancel, "Cancel", "3")] // Playwright Unknown key: "Cancel"
        [TestCase(Key.Help, "Help", "47")] //Playwright Unknown key: "Help"
        [TestCase(Key.Backspace, "Backspace", "8")]
        [TestCase(Key.Tab, "Tab", "9")]
        [TestCase(Key.Clear, "Clear", "12")] // Playwright Unknown key: "Clear"
        [TestCase(Key.Return, "", "")] // Playwright Unknown key: "Return"
        [TestCase(Key.Enter, "Enter", "13")]
        [TestCase(Key.Shift, "Shift", "16")]
        [TestCase(Key.LeftShift, "Shift", "16")]
        [TestCase(Key.Control, "Control", "17")]
        [TestCase(Key.LeftControl, "Control", "17")]
        [TestCase(Key.Alt, "Alt", "18")]
        [TestCase(Key.LeftAlt, "Alt", "18")]
        [TestCase(Key.Pause, "Pause", "19")]
        [TestCase(Key.Escape, "Escape", "27")]
        [TestCase(Key.Space, " ", "32")]
        [TestCase(Key.PageUp, "PageUp", "33")]
        [TestCase(Key.PageDown, "PageDown", "34")]
        [TestCase(Key.End, "End", "35")]
        [TestCase(Key.Home, "Home", "36")]
        [TestCase(Key.Left, "ArrowLeft", "37")]
        [TestCase(Key.ArrowLeft, "ArrowLeft", "37")]
        [TestCase(Key.Up, "ArrowUp", "38")]
        [TestCase(Key.ArrowUp, "ArrowUp", "38")]
        [TestCase(Key.Right, "ArrowRight", "39")]
        [TestCase(Key.ArrowRight, "ArrowRight", "39")]
        [TestCase(Key.Down, "ArrowDown", "40")]
        [TestCase(Key.ArrowDown, "ArrowDown", "40")]
        [TestCase(Key.Insert, "Insert", "45")]
        [TestCase(Key.Delete, "Delete", "46")]
        [TestCase(Key.Semicolon, ":", "186")]  //   Playwright  Expected actualValue to be ": KeyDown", but "; KeyDown" differs near "; K" (index 0). !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        [TestCase(Key.Equal, "=", "187")]
        [TestCase(Key.NumberPad0, "0", "96")]
        [TestCase(Key.NumberPad1, "1", "97")]
        [TestCase(Key.NumberPad2, "2", "98")]
        [TestCase(Key.NumberPad3, "3", "99")]
        [TestCase(Key.NumberPad4, "4", "100")]
        [TestCase(Key.NumberPad5, "5", "101")]
        [TestCase(Key.NumberPad6, "6", "102")]
        [TestCase(Key.NumberPad7, "7", "103")]
        [TestCase(Key.NumberPad8, "8", "104")]
        [TestCase(Key.NumberPad9, "9", "105")]
        [TestCase(Key.Multiply, "*", "106")] //Playwright Expected actualCode to be "106" with a length of 3, but "56" has a length of 2, differs near "56" (index 0).
        [TestCase(Key.Add, "+", "107")] //Playwright 187
        [TestCase(Key.Separator, "<", "188")]     //Expected actualValue to be "< KeyDown", but ", KeyDown" differs near ", K" (index 0).
        [TestCase(Key.Subtract, "-", "109")]//Playwright Expected actualCode to be "109", but "189" differs near "89" (index 1).
        [TestCase(Key.Decimal, ".", "110")]
        [TestCase(Key.Divide, "/", "111")] //Playwright Expected actualCode to be "111", but "191" differs near "91"
        [TestCase(Key.F1, "F1", "112")]
        [TestCase(Key.F2, "F2", "113")]
        [TestCase(Key.F3, "F3", "114")]
        [TestCase(Key.F4, "F4", "115")]
        [TestCase(Key.F5, "F5", "116")]
        [TestCase(Key.F6, "F6", "117")]
        [TestCase(Key.F7, "F7", "118")]
        [TestCase(Key.F8, "F8", "119")]
        [TestCase(Key.F9, "F9", "120")]
        [TestCase(Key.F10, "F10", "121")]
        [TestCase(Key.F11, "F11", "122")]
        [TestCase(Key.F12, "F12", "123")]
        [TestCase(Key.Meta, "Meta", "91")]
        [TestCase(Key.Command, "Meta", "91")] //Unknown key: "Command"  Unknown key: "Meta"
        [TestCase(Key.ZenkakuHankaku, "ZenkakuHankaku", "244")]
        public void Should_Process_KeyDown(string key, string expectedValue, string expectedCode)
        {
            //WHEN
            _keyboardTestPage.KeyDownEventInterceptorSection.AddBehavior<IKeyboardBehavior>().KeysDown(key);
            var actualValue = _keyboardTestPage.KeyDownValueInfo.GetText();
            var actualCode = _keyboardTestPage.KeyDownCodeInfo.GetText();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(expectedValue + $" {Const.KeyDown}");
                actualCode.Should().Be(expectedCode + $" {Const.KeyDown}");
            }
        }

        [TestCaseSource(nameof(KeysInfoTestCaseSource))]
        public void Should_Process_KeyDown_KeyUp_TestCaseSource(string key, string expectedValue, List<string> expectedCodes)
        {
            //WHEN
            _keyboardTestPage.KeyDownUpEventInterceptorSection.AddBehavior<IKeyboardBehavior>().KeysDown(key);
            var actualKeyDownValue = _keyboardTestPage.KeyDownUpValueInfo.GetText();
            var actualKeyDownCode = _keyboardTestPage.KeyDownUpCodeInfo.GetText();

            _keyboardTestPage.KeyDownUpEventInterceptorSection.AddBehavior<IKeyboardBehavior>().KeysUp(key);
            var actualKeyUpValue = _keyboardTestPage.KeyDownUpValueInfo.GetText();
            var actualKeyUpCode = _keyboardTestPage.KeyDownUpCodeInfo.GetText();

            //THEN
            using (new AssertionScope())
            {
                actualKeyDownValue.Should().Be(GetExpectedKeyDownString(expectedValue));
                expectedCodes.Select(x => GetExpectedKeyDownString(x)).Should().Contain(actualKeyDownCode);

                actualKeyUpValue.Should().Be(GetExpectedKeyUpString(expectedValue));
                expectedCodes.Select(x => GetExpectedKeyUpString(x)).Should().Contain(actualKeyUpCode);
            }
        }

        [TestCaseSource(nameof(KeysInfoTestCaseSource))]
        public void Should_Process_PressKeysSequentially_TestCaseSource(string key, string expectedValue, List<string> expectedCodes)
        {
            //WHEN
            _keyboardTestPage.KeyPressEventInterceptorSection.AddBehavior<IKeyboardBehavior>().PressKeysSequentially(key);

            var actualKeyDownValue = _keyboardTestPage.KeyPressEventInterceptorKeyDownValueInfo.GetText();
            var actualKeyDownCode = _keyboardTestPage.KeyPressEventInterceptorKeyDownCodeInfo.GetText();

            var actualKeyUpValue = _keyboardTestPage.KeyPressEventInterceptorKeyUpValueInfo.GetText();
            var actualKeyUpCode = _keyboardTestPage.KeyPressEventInterceptorKeyUpCodeInfo.GetText();

            //THEN
            using (new AssertionScope())
            {
                actualKeyDownValue.Should().Be(expectedValue);
                expectedCodes.Should().Contain(actualKeyDownCode);

                actualKeyUpValue.Should().Be(expectedValue);
                expectedCodes.Should().Contain(actualKeyUpCode);
            }
        }

        [Test]
        public void Should_Process_PressKeysSimultaneously_KeysCombination()
        {
            //GIVEN
            var actualTextBeforeCopyPaste = _keyboardTestPage.PasteTextBox.GetText();

            //WHEN
            _keyboardTestPage.CopyTextBox.AddBehavior<IUiElementMouseBehavior>().MouseDoubleClick();
            _keyboardTestPage.CopyTextBox.AddBehavior<IKeyboardBehavior>().PressKeysSimultaneously($"{Key.Control}+c");
            _keyboardTestPage.PasteTextBox.AddBehavior<IKeyboardBehavior>().PressKeysSimultaneously($"{Key.Control} + v");
            var actualTextAfterCopyPaste = _keyboardTestPage.PasteTextBox.GetText();

            //THEN
            using (new AssertionScope())
            {
                actualTextBeforeCopyPaste.Should().Be(string.Empty);
                actualTextAfterCopyPaste.Should().Be(CopyPasteValue);
            }
        }

        [Test]
        public void Should_Process_PressKeysSimultaneously_ParamsString()
        {
            //GIVEN
            var actualTextBeforeCopyPaste = _keyboardTestPage.PasteTextBox.GetText();

            //WHEN
            _keyboardTestPage.CopyTextBox.AddBehavior<IUiElementMouseBehavior>().MouseDoubleClick();
            _keyboardTestPage.CopyTextBox.AddBehavior<IKeyboardBehavior>().PressKeysSimultaneously(Key.Control, "c");
            _keyboardTestPage.PasteTextBox.AddBehavior<IKeyboardBehavior>().PressKeysSimultaneously(Key.Control, "v");
            var actualTextAfterCopyPaste = _keyboardTestPage.PasteTextBox.GetText();

            //THEN
            using (new AssertionScope())
            {
                actualTextBeforeCopyPaste.Should().Be(string.Empty);
                actualTextAfterCopyPaste.Should().Be(CopyPasteValue);
            }
        }

        [Test]
        public void Should_Process_PressKeysSimultaneously_ParamsKeys()
        {
            //GIVEN
            var actualTextBeforeInput = _keyboardTestPage.InputTextBox.GetText();

            //WHEN
            _keyboardTestPage.InputTextBox.AddBehavior<IKeyboardBehavior>().PressKeysSimultaneously(Key.Shift, Key.Equal);
            var actualTextAfterInput = _keyboardTestPage.InputTextBox.GetText();

            //THEN
            using (new AssertionScope())
            {
                actualTextBeforeInput.Should().Be(string.Empty);
                actualTextAfterInput.Should().Be("+");
            }
        }

        public static IEnumerable<TestCaseData> KeysInfoTestCaseSource()
        {
            yield return new TestCaseData(Key.Add, "+", new List<string>() { "107", "187" });
            yield return new TestCaseData(Key.Alt, "Alt", new List<string>() { "18" });
            yield return new TestCaseData(Key.ArrowDown, "ArrowDown", new List<string>() { "40" });
            yield return new TestCaseData(Key.ArrowLeft, "ArrowLeft", new List<string>() { "37" });
            yield return new TestCaseData(Key.ArrowRight, "ArrowRight", new List<string>() { "39" });
            yield return new TestCaseData(Key.ArrowUp, "ArrowUp", new List<string>() { "38" });
            yield return new TestCaseData(Key.Backspace, "Backspace", new List<string>() { "8" });
            yield return new TestCaseData(Key.Control, "Control", new List<string>() { "17" });
            yield return new TestCaseData(Key.Decimal, ".", new List<string>() { "110", "190" });
            yield return new TestCaseData(Key.Delete, "Delete", new List<string>() { "46" });
            yield return new TestCaseData(Key.Divide, "/", new List<string>() { "111", "191" });
            yield return new TestCaseData(Key.Down, "ArrowDown", new List<string>() { "40" });
            yield return new TestCaseData(Key.End, "End", new List<string>() { "35" });
            yield return new TestCaseData(Key.Enter, "Enter", new List<string>() { "13" });
            yield return new TestCaseData(Key.Equal, "=", new List<string>() { "187" });
            yield return new TestCaseData(Key.Escape, "Escape", new List<string>() { "27" });
            yield return new TestCaseData(Key.F1, "F1", new List<string>() { "112" });
            yield return new TestCaseData(Key.F2, "F2", new List<string>() { "113" });
            yield return new TestCaseData(Key.F3, "F3", new List<string>() { "114" });
            yield return new TestCaseData(Key.F4, "F4", new List<string>() { "115" });
            yield return new TestCaseData(Key.F5, "F5", new List<string>() { "116" });
            yield return new TestCaseData(Key.F6, "F6", new List<string>() { "117" });
            yield return new TestCaseData(Key.F7, "F7", new List<string>() { "118" });
            yield return new TestCaseData(Key.F8, "F8", new List<string>() { "119" });
            yield return new TestCaseData(Key.F9, "F9", new List<string>() { "120" });
            yield return new TestCaseData(Key.F10, "F10", new List<string>() { "121" });
            yield return new TestCaseData(Key.F11, "F11", new List<string>() { "122" });
            yield return new TestCaseData(Key.F12, "F12", new List<string>() { "123" });
            yield return new TestCaseData(Key.Home, "Home", new List<string>() { "36" });
            yield return new TestCaseData(Key.Insert, "Insert", new List<string>() { "45" });
            yield return new TestCaseData(Key.Left, "ArrowLeft", new List<string>() { "37" });
            yield return new TestCaseData(Key.LeftAlt, "Alt", new List<string>() { "18" });
            yield return new TestCaseData(Key.LeftControl, "Control", new List<string>() { "17" });
            yield return new TestCaseData(Key.LeftShift, "Shift", new List<string>() { "16" });
            yield return new TestCaseData(Key.Multiply, "*", new List<string>() { "56", "106" });
            yield return new TestCaseData(Key.NumberPad0, "0", new List<string>() { "48", "96" });
            yield return new TestCaseData(Key.NumberPad1, "1", new List<string>() { "49", "97" });
            yield return new TestCaseData(Key.NumberPad2, "2", new List<string>() { "50", "98" });
            yield return new TestCaseData(Key.NumberPad3, "3", new List<string>() { "51", "99" });
            yield return new TestCaseData(Key.NumberPad4, "4", new List<string>() { "52", "100" });
            yield return new TestCaseData(Key.NumberPad5, "5", new List<string>() { "53", "101" });
            yield return new TestCaseData(Key.NumberPad6, "6", new List<string>() { "54", "102" });
            yield return new TestCaseData(Key.NumberPad7, "7", new List<string>() { "55", "103" });
            yield return new TestCaseData(Key.NumberPad8, "8", new List<string>() { "56", "104" });
            yield return new TestCaseData(Key.NumberPad9, "9", new List<string>() { "57", "105" });
            yield return new TestCaseData(Key.PageDown, "PageDown", new List<string>() { "34" });
            yield return new TestCaseData(Key.PageUp, "PageUp", new List<string>() { "33" });
            yield return new TestCaseData(Key.Pause, "Pause", new List<string>() { "19" });
            yield return new TestCaseData(Key.Right, "ArrowRight", new List<string>() { "39" });
            yield return new TestCaseData(Key.Semicolon, ";", new List<string>() { "186" });
            yield return new TestCaseData(Key.Separator, ",", new List<string>() { "188" });
            yield return new TestCaseData(Key.Shift, "Shift", new List<string>() { "16" });
            yield return new TestCaseData(Key.Space, " ", new List<string>() { "32" });
            yield return new TestCaseData(Key.Subtract, "-", new List<string>() { "109", "189" });
            yield return new TestCaseData(Key.Tab, "Tab", new List<string>() { "9" });
            yield return new TestCaseData(Key.Up, "ArrowUp", new List<string>() { "38" });
        }
        private string GetExpectedKeyDownString(string parameter) => $"{parameter} {Const.KeyDown}";
        private string GetExpectedKeyUpString(string parameter) => $"{parameter} {Const.KeyUp}";
    }
}
