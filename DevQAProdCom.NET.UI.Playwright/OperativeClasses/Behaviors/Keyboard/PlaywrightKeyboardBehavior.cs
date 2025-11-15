using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Keyboard
{
    public class PlaywrightKeyboardBehavior : BaseKeyboardBehavior
    {
        protected IPage? Page { get; }

        public PlaywrightKeyboardBehavior(IBehaviorParameters parameters, IKeyMatcher keyMatcher) : base(parameters, keyMatcher)
        {
            Page = Parameters.GetOrDefault<IPage>(SharedUiConstants.IUiPage);
            Page ??= (UiElement as PlaywrightUiElement)?.GetPage();

            if (Page == null)
                throw new ArgumentNullException($"'{SharedUiConstants.IUiPage}' parameter should be provided to be able to apply Keyboard Behavior.");
        }

        public override void KeysDown(params string[] keys)
        {
            UiElement?.FocusJs();
            foreach (var key in keys)
            {
                var matchedKey = Match<string>(key);
                Page.Keyboard.DownAsync(matchedKey).Wait();
            }
        }

        public override void KeysUp(params string[] keys)
        {
            UiElement?.FocusJs();
            foreach (var key in keys)
            {
                var matchedKey = Match<string>(key);
                Page.Keyboard.UpAsync(matchedKey).Wait();
            }
        }

        public override void PressKeysSequentially(params string[] keys)
        {
            UiElement?.FocusJs();
            foreach (var key in keys)
            {
                var matchedKey = Match<string>(key);
                Page.Keyboard.PressAsync(matchedKey).Wait();
            }
        }

        public override void SendText(string textKeys)
        {
            UiElement?.FocusJs();
            Page.Keyboard.TypeAsync(textKeys);
        }
    }
}
