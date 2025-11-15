using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Keyboard
{
    public interface IKeyboardBehavior : IBehavior
    {
        //https://playwright.dev/docs/api/class-keyboard#keyboard-press
        //In most cases, you should use locator.fill() instead.You only need to press keys one by one if there is special keyboard handling on the page - in this case use locator.pressSequentially().
        //Sends a keydown, keypress/input, and keyup event for each character in the text.
        //To press a special key, like Control or ArrowDown, use keyboard.press().
        //Usage
        //await page.keyboard.type('Hello'); // Types instantly
        //await page.keyboard.type('World', { delay: 100 }); // Types slower, like a user
        //https://developer.mozilla.org/en-US/docs/Web/API/UI_Events/Keyboard_event_key_values
        //https://www.toptal.com/developers/keycode //https://keycode.info
        public void KeysDown(params string[] keys);
        public void KeysUp(params string[] keys);

        public void PressKeysSequentially(params string[] keys);
        public void PressKeysSimultaneously(string keysCombination);
        public void PressKeysSimultaneously(params string[] keysCombination);
        public void SendText(string textKeys);
    }
}
