using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.OperativeClasses;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses
{
    public class SeleniumKeyMatcher : BaseKeyMatcher<string>
    {
        public SeleniumKeyMatcher()
        {
            KeysMatchDict.Add(Key.Null.ToString(), Keys.Null);
            KeysMatchDict.Add(Key.Cancel.ToString(), Keys.Cancel);
            KeysMatchDict.Add(Key.Help.ToString(), Keys.Help);
            KeysMatchDict.Add(Key.Backspace.ToString(), Keys.Backspace);
            KeysMatchDict.Add(Key.Tab.ToString(), Keys.Tab);
            KeysMatchDict.Add(Key.Clear.ToString(), Keys.Clear);
            KeysMatchDict.Add(Key.Return.ToString(), Keys.Return);
            KeysMatchDict.Add(Key.Enter.ToString(), Keys.Enter);
            KeysMatchDict.Add(Key.Shift.ToString(), Keys.Shift);
            KeysMatchDict.Add(Key.LeftShift.ToString(), Keys.LeftShift);
            KeysMatchDict.Add(Key.Control.ToString(), Keys.Control);
            KeysMatchDict.Add(Key.LeftControl.ToString(), Keys.LeftControl);
            KeysMatchDict.Add(Key.Alt.ToString(), Keys.Alt);
            KeysMatchDict.Add(Key.LeftAlt.ToString(), Keys.LeftAlt);
            KeysMatchDict.Add(Key.Pause.ToString(), Keys.Pause);
            KeysMatchDict.Add(Key.Escape.ToString(), Keys.Escape);
            KeysMatchDict.Add(Key.Space.ToString(), Keys.Space);
            KeysMatchDict.Add(Key.PageUp.ToString(), Keys.PageUp);
            KeysMatchDict.Add(Key.PageDown.ToString(), Keys.PageDown);
            KeysMatchDict.Add(Key.End.ToString(), Keys.End);
            KeysMatchDict.Add(Key.Home.ToString(), Keys.Home);
            KeysMatchDict.Add(Key.Left.ToString(), Keys.Left);
            KeysMatchDict.Add(Key.ArrowLeft.ToString(), Keys.ArrowLeft);
            KeysMatchDict.Add(Key.Up.ToString(), Keys.Up);
            KeysMatchDict.Add(Key.ArrowUp.ToString(), Keys.ArrowUp);
            KeysMatchDict.Add(Key.Right.ToString(), Keys.Right);
            KeysMatchDict.Add(Key.ArrowRight.ToString(), Keys.ArrowRight);
            KeysMatchDict.Add(Key.Down.ToString(), Keys.Down);
            KeysMatchDict.Add(Key.ArrowDown.ToString(), Keys.ArrowDown);
            KeysMatchDict.Add(Key.Insert.ToString(), Keys.Insert);
            KeysMatchDict.Add(Key.Delete.ToString(), Keys.Delete);
            KeysMatchDict.Add(Key.Semicolon.ToString(), Keys.Semicolon);
            KeysMatchDict.Add(Key.Equal.ToString(), Keys.Equal);
            KeysMatchDict.Add(Key.NumberPad0.ToString(), Keys.NumberPad0);
            KeysMatchDict.Add(Key.NumberPad1.ToString(), Keys.NumberPad1);
            KeysMatchDict.Add(Key.NumberPad2.ToString(), Keys.NumberPad2);
            KeysMatchDict.Add(Key.NumberPad3.ToString(), Keys.NumberPad3);
            KeysMatchDict.Add(Key.NumberPad4.ToString(), Keys.NumberPad4);
            KeysMatchDict.Add(Key.NumberPad5.ToString(), Keys.NumberPad5);
            KeysMatchDict.Add(Key.NumberPad6.ToString(), Keys.NumberPad6);
            KeysMatchDict.Add(Key.NumberPad7.ToString(), Keys.NumberPad7);
            KeysMatchDict.Add(Key.NumberPad8.ToString(), Keys.NumberPad8);
            KeysMatchDict.Add(Key.NumberPad9.ToString(), Keys.NumberPad9);
            KeysMatchDict.Add(Key.Multiply.ToString(), Keys.Multiply);
            KeysMatchDict.Add(Key.Add.ToString(), Keys.Add);
            KeysMatchDict.Add(Key.Separator.ToString(), Keys.Separator);
            KeysMatchDict.Add(Key.Subtract.ToString(), Keys.Subtract);
            KeysMatchDict.Add(Key.Decimal.ToString(), Keys.Decimal);
            KeysMatchDict.Add(Key.Divide.ToString(), Keys.Divide);
            KeysMatchDict.Add(Key.F1.ToString(), Keys.F1);
            KeysMatchDict.Add(Key.F2.ToString(), Keys.F2);
            KeysMatchDict.Add(Key.F3.ToString(), Keys.F3);
            KeysMatchDict.Add(Key.F4.ToString(), Keys.F4);
            KeysMatchDict.Add(Key.F5.ToString(), Keys.F5);
            KeysMatchDict.Add(Key.F6.ToString(), Keys.F6);
            KeysMatchDict.Add(Key.F7.ToString(), Keys.F7);
            KeysMatchDict.Add(Key.F8.ToString(), Keys.F8);
            KeysMatchDict.Add(Key.F9.ToString(), Keys.F9);
            KeysMatchDict.Add(Key.F10.ToString(), Keys.F10);
            KeysMatchDict.Add(Key.F11.ToString(), Keys.F11);
            KeysMatchDict.Add(Key.F12.ToString(), Keys.F12);
            KeysMatchDict.Add(Key.Meta.ToString(), Keys.Meta);
            KeysMatchDict.Add(Key.Command.ToString(), Keys.Command);
            KeysMatchDict.Add(Key.ZenkakuHankaku.ToString(), Keys.ZenkakuHankaku);
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
