using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiPage.Behaviors.Keyboard
{
    public class PlaywrightUiPageBehaviorKeysDown(IBehaviorParameters parameters, IKeyMatcher keyMatcher) : PlaywrightUiPageBehavior(parameters), IUiPageBehaviorKeysDown
    {
        public void KeysDown(params string[] keys)
        {
            foreach (var key in keys)
            {
                var matchData = keyMatcher.Match<string>(key);

                if (matchData.IsMatched)
                    Page.Keyboard.DownAsync(matchData.MatchedKey).Wait();
                else
                {
                    char[] chars = matchData.Key.ToCharArray();

                    foreach (char @char in chars)
                        Page.Keyboard.DownAsync(@char.ToString()).Wait();
                }
            }
        }

        public void KeysDown(params Key[] keys)
        {
            KeysDown(keys.Select(x => x.ToString()).ToArray());
        }
    }
}
