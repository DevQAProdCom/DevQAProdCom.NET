using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses
{
    public class PlaywrightKeyMatcher : BaseKeyMatcher<string>
    {
        public PlaywrightKeyMatcher()
        {
            KeysMatchDict.Add(Key.Null, "Unidentified");
            KeysMatchDict.Add(Key.Cancel, "Cancel");
            KeysMatchDict.Add(Key.Help, "Help");
            KeysMatchDict.Add(Key.Backspace, "Backspace");
            KeysMatchDict.Add(Key.Tab, "Tab");
            KeysMatchDict.Add(Key.Clear, "Clear");
            KeysMatchDict.Add(Key.Return, "Return");
            KeysMatchDict.Add(Key.Enter, "Enter");
            KeysMatchDict.Add(Key.Shift, "Shift");
            KeysMatchDict.Add(Key.LeftShift, "Shift");
            KeysMatchDict.Add(Key.Control, "Control");
            KeysMatchDict.Add(Key.LeftControl, "Control");
            KeysMatchDict.Add(Key.Alt, "Alt");
            KeysMatchDict.Add(Key.LeftAlt, "Alt");
            KeysMatchDict.Add(Key.Pause, "Pause");
            KeysMatchDict.Add(Key.Escape, "Escape");
            KeysMatchDict.Add(Key.Space, "Space");
            KeysMatchDict.Add(Key.PageUp, "PageUp");
            KeysMatchDict.Add(Key.PageDown, "PageDown");
            KeysMatchDict.Add(Key.End, "End");
            KeysMatchDict.Add(Key.Home, "Home");
            KeysMatchDict.Add(Key.Left, "ArrowLeft");
            KeysMatchDict.Add(Key.ArrowLeft, "ArrowLeft");
            KeysMatchDict.Add(Key.Up, "ArrowUp");
            KeysMatchDict.Add(Key.ArrowUp, "ArrowUp");
            KeysMatchDict.Add(Key.Right, "ArrowRight");
            KeysMatchDict.Add(Key.ArrowRight, "ArrowRight");
            KeysMatchDict.Add(Key.Down, "ArrowDown");
            KeysMatchDict.Add(Key.ArrowDown, "ArrowDown");
            KeysMatchDict.Add(Key.Insert, "Insert");
            KeysMatchDict.Add(Key.Delete, "Delete");
            KeysMatchDict.Add(Key.Semicolon, ";");
            KeysMatchDict.Add(Key.Equal, "Equal");
            KeysMatchDict.Add(Key.NumberPad0, "0");
            KeysMatchDict.Add(Key.NumberPad1, "1");
            KeysMatchDict.Add(Key.NumberPad2, "2");
            KeysMatchDict.Add(Key.NumberPad3, "3");
            KeysMatchDict.Add(Key.NumberPad4, "4");
            KeysMatchDict.Add(Key.NumberPad5, "5");
            KeysMatchDict.Add(Key.NumberPad6, "6");
            KeysMatchDict.Add(Key.NumberPad7, "7");
            KeysMatchDict.Add(Key.NumberPad8, "8");
            KeysMatchDict.Add(Key.NumberPad9, "9");
            KeysMatchDict.Add(Key.Multiply, "*");
            KeysMatchDict.Add(Key.Add, "+");
            KeysMatchDict.Add(Key.Separator, ",");
            KeysMatchDict.Add(Key.Subtract, "-");
            KeysMatchDict.Add(Key.Decimal, ".");
            KeysMatchDict.Add(Key.Divide, "/");
            KeysMatchDict.Add(Key.F1, "F1");
            KeysMatchDict.Add(Key.F2, "F2");
            KeysMatchDict.Add(Key.F3, "F3");
            KeysMatchDict.Add(Key.F4, "F4");
            KeysMatchDict.Add(Key.F5, "F5");
            KeysMatchDict.Add(Key.F6, "F6");
            KeysMatchDict.Add(Key.F7, "F7");
            KeysMatchDict.Add(Key.F8, "F8");
            KeysMatchDict.Add(Key.F9, "F9");
            KeysMatchDict.Add(Key.F10, "F10");
            KeysMatchDict.Add(Key.F11, "F11");
            KeysMatchDict.Add(Key.F12, "F12");
            KeysMatchDict.Add(Key.Meta, "Meta");
            KeysMatchDict.Add(Key.Command, "Command");
            KeysMatchDict.Add(Key.ZenkakuHankaku, "ZenkakuHankaku");
        }

        protected override string FindMatch(string keyIdentifier)
        {
            if (KeysMatchDict.TryGetValue(keyIdentifier, out var match))
                return match;
            return keyIdentifier;
        }
    }
}
