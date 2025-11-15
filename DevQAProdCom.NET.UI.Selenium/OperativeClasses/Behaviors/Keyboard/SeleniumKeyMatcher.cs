using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Keyboard
{
    public class SeleniumKeyMatcher : BaseKeyMatcher<string>
    {
        public SeleniumKeyMatcher()
        {
            KeysMatchDict.Add(Key.Null, Keys.Null);
            KeysMatchDict.Add(Key.Cancel, Keys.Cancel);
            KeysMatchDict.Add(Key.Help, Keys.Help);
            KeysMatchDict.Add(Key.Backspace, Keys.Backspace);
            KeysMatchDict.Add(Key.Tab, Keys.Tab);
            KeysMatchDict.Add(Key.Clear, Keys.Clear);
            KeysMatchDict.Add(Key.Return, Keys.Return);
            KeysMatchDict.Add(Key.Enter, Keys.Enter);
            KeysMatchDict.Add(Key.Shift, Keys.Shift);
            KeysMatchDict.Add(Key.LeftShift, Keys.LeftShift);
            KeysMatchDict.Add(Key.Control, Keys.Control);
            KeysMatchDict.Add(Key.LeftControl, Keys.LeftControl);
            KeysMatchDict.Add(Key.Alt, Keys.Alt);
            KeysMatchDict.Add(Key.LeftAlt, Keys.LeftAlt);
            KeysMatchDict.Add(Key.Pause, Keys.Pause);
            KeysMatchDict.Add(Key.Escape, Keys.Escape);
            KeysMatchDict.Add(Key.Space, Keys.Space);
            KeysMatchDict.Add(Key.PageUp, Keys.PageUp);
            KeysMatchDict.Add(Key.PageDown, Keys.PageDown);
            KeysMatchDict.Add(Key.End, Keys.End);
            KeysMatchDict.Add(Key.Home, Keys.Home);
            KeysMatchDict.Add(Key.Left, Keys.Left);
            KeysMatchDict.Add(Key.ArrowLeft, Keys.ArrowLeft);
            KeysMatchDict.Add(Key.Up, Keys.Up);
            KeysMatchDict.Add(Key.ArrowUp, Keys.ArrowUp);
            KeysMatchDict.Add(Key.Right, Keys.Right);
            KeysMatchDict.Add(Key.ArrowRight, Keys.ArrowRight);
            KeysMatchDict.Add(Key.Down, Keys.Down);
            KeysMatchDict.Add(Key.ArrowDown, Keys.ArrowDown);
            KeysMatchDict.Add(Key.Insert, Keys.Insert);
            KeysMatchDict.Add(Key.Delete, Keys.Delete);
            KeysMatchDict.Add(Key.Semicolon, Keys.Semicolon);
            KeysMatchDict.Add(Key.Equal, Keys.Equal);
            KeysMatchDict.Add(Key.NumberPad0, Keys.NumberPad0);
            KeysMatchDict.Add(Key.NumberPad1, Keys.NumberPad1);
            KeysMatchDict.Add(Key.NumberPad2, Keys.NumberPad2);
            KeysMatchDict.Add(Key.NumberPad3, Keys.NumberPad3);
            KeysMatchDict.Add(Key.NumberPad4, Keys.NumberPad4);
            KeysMatchDict.Add(Key.NumberPad5, Keys.NumberPad5);
            KeysMatchDict.Add(Key.NumberPad6, Keys.NumberPad6);
            KeysMatchDict.Add(Key.NumberPad7, Keys.NumberPad7);
            KeysMatchDict.Add(Key.NumberPad8, Keys.NumberPad8);
            KeysMatchDict.Add(Key.NumberPad9, Keys.NumberPad9);
            KeysMatchDict.Add(Key.Multiply, Keys.Multiply);
            KeysMatchDict.Add(Key.Add, Keys.Add);
            KeysMatchDict.Add(Key.Separator, Keys.Separator);
            KeysMatchDict.Add(Key.Subtract, Keys.Subtract);
            KeysMatchDict.Add(Key.Decimal, Keys.Decimal);
            KeysMatchDict.Add(Key.Divide, Keys.Divide);
            KeysMatchDict.Add(Key.F1, Keys.F1);
            KeysMatchDict.Add(Key.F2, Keys.F2);
            KeysMatchDict.Add(Key.F3, Keys.F3);
            KeysMatchDict.Add(Key.F4, Keys.F4);
            KeysMatchDict.Add(Key.F5, Keys.F5);
            KeysMatchDict.Add(Key.F6, Keys.F6);
            KeysMatchDict.Add(Key.F7, Keys.F7);
            KeysMatchDict.Add(Key.F8, Keys.F8);
            KeysMatchDict.Add(Key.F9, Keys.F9);
            KeysMatchDict.Add(Key.F10, Keys.F10);
            KeysMatchDict.Add(Key.F11, Keys.F11);
            KeysMatchDict.Add(Key.F12, Keys.F12);
            KeysMatchDict.Add(Key.Meta, Keys.Meta);
            KeysMatchDict.Add(Key.Command, Keys.Command);
            KeysMatchDict.Add(Key.ZenkakuHankaku, Keys.ZenkakuHankaku);
        }

        protected override string FindMatch(string keyIdentifier)
        {
            if (KeysMatchDict.TryGetValue(keyIdentifier, out var match))
                return match;
            return keyIdentifier;
        }
    }
}
