using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.OperativeClasses;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses
{
    public class PlaywrightKeyMatcher : BaseKeyMatcher<string>
    {
        public PlaywrightKeyMatcher()
        {
            KeysMatchDict.Add(Key.Null.ToString(), "Unidentified");
            KeysMatchDict.Add(Key.Cancel.ToString(), "Cancel");
            KeysMatchDict.Add(Key.Help.ToString(), "Help");
            KeysMatchDict.Add(Key.Backspace.ToString(), "Backspace");
            KeysMatchDict.Add(Key.Tab.ToString(), "Tab");
            KeysMatchDict.Add(Key.Clear.ToString(), "Clear");
            KeysMatchDict.Add(Key.Return.ToString(), "Return");
            KeysMatchDict.Add(Key.Enter.ToString(), "Enter");
            KeysMatchDict.Add(Key.Shift.ToString(), "Shift");
            KeysMatchDict.Add(Key.LeftShift.ToString(), "Shift");
            KeysMatchDict.Add(Key.Control.ToString(), "Control");
            KeysMatchDict.Add(Key.LeftControl.ToString(), "Control");
            KeysMatchDict.Add(Key.Alt.ToString(), "Alt");
            KeysMatchDict.Add(Key.LeftAlt.ToString(), "Alt");
            KeysMatchDict.Add(Key.Pause.ToString(), "Pause");
            KeysMatchDict.Add(Key.Escape.ToString(), "Escape");
            KeysMatchDict.Add(Key.Space.ToString(), "Space");
            KeysMatchDict.Add(Key.PageUp.ToString(), "PageUp");
            KeysMatchDict.Add(Key.PageDown.ToString(), "PageDown");
            KeysMatchDict.Add(Key.End.ToString(), "End");
            KeysMatchDict.Add(Key.Home.ToString(), "Home");
            KeysMatchDict.Add(Key.Left.ToString(), "ArrowLeft");
            KeysMatchDict.Add(Key.ArrowLeft.ToString(), "ArrowLeft");
            KeysMatchDict.Add(Key.Up.ToString(), "ArrowUp");
            KeysMatchDict.Add(Key.ArrowUp.ToString(), "ArrowUp");
            KeysMatchDict.Add(Key.Right.ToString(), "ArrowRight");
            KeysMatchDict.Add(Key.ArrowRight.ToString(), "ArrowRight");
            KeysMatchDict.Add(Key.Down.ToString(), "ArrowDown");
            KeysMatchDict.Add(Key.ArrowDown.ToString(), "ArrowDown");
            KeysMatchDict.Add(Key.Insert.ToString(), "Insert");
            KeysMatchDict.Add(Key.Delete.ToString(), "Delete");
            KeysMatchDict.Add(Key.Semicolon.ToString(), ";");
            KeysMatchDict.Add(Key.Equal.ToString(), "Equal");
            KeysMatchDict.Add(Key.NumberPad0.ToString(), "0");
            KeysMatchDict.Add(Key.NumberPad1.ToString(), "1");
            KeysMatchDict.Add(Key.NumberPad2.ToString(), "2");
            KeysMatchDict.Add(Key.NumberPad3.ToString(), "3");
            KeysMatchDict.Add(Key.NumberPad4.ToString(), "4");
            KeysMatchDict.Add(Key.NumberPad5.ToString(), "5");
            KeysMatchDict.Add(Key.NumberPad6.ToString(), "6");
            KeysMatchDict.Add(Key.NumberPad7.ToString(), "7");
            KeysMatchDict.Add(Key.NumberPad8.ToString(), "8");
            KeysMatchDict.Add(Key.NumberPad9.ToString(), "9");
            KeysMatchDict.Add(Key.Multiply.ToString(), "*");
            KeysMatchDict.Add(Key.Add.ToString(), "+");
            KeysMatchDict.Add(Key.Separator.ToString(), ",");
            KeysMatchDict.Add(Key.Subtract.ToString(), "-");
            KeysMatchDict.Add(Key.Decimal.ToString(), ".");
            KeysMatchDict.Add(Key.Divide.ToString(), "/");
            KeysMatchDict.Add(Key.F1.ToString(), "F1");
            KeysMatchDict.Add(Key.F2.ToString(), "F2");
            KeysMatchDict.Add(Key.F3.ToString(), "F3");
            KeysMatchDict.Add(Key.F4.ToString(), "F4");
            KeysMatchDict.Add(Key.F5.ToString(), "F5");
            KeysMatchDict.Add(Key.F6.ToString(), "F6");
            KeysMatchDict.Add(Key.F7.ToString(), "F7");
            KeysMatchDict.Add(Key.F8.ToString(), "F8");
            KeysMatchDict.Add(Key.F9.ToString(), "F9");
            KeysMatchDict.Add(Key.F10.ToString(), "F10");
            KeysMatchDict.Add(Key.F11.ToString(), "F11");
            KeysMatchDict.Add(Key.F12.ToString(), "F12");
            KeysMatchDict.Add(Key.Meta.ToString(), "Meta");
            KeysMatchDict.Add(Key.Command.ToString(), "Command");
            KeysMatchDict.Add(Key.ZenkakuHankaku.ToString(), "ZenkakuHankaku");
        }

        //protected override bool TryFindMatch(string key, out string result)
        //{
        //    return KeysMatchDict.TryGetValue(key, out result);
        //}

        //protected override string FindMatch(string key)
        //{
        //    if (KeysMatchDict.TryGetValue(key, out var match))
        //        return match;
        //    return key;
        //}
    }
}
