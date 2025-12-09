using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Keyboard;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiPage.Behaviors.Keyboard
{
    public class SeleniumUiPageBehaviorKeysDown(IBehaviorParameters parameters, IKeyMatcher keyMatcher) : SeleniumUiPageBehavior(parameters), IUiPageBehaviorKeysDown
    {
        public void KeysDown(params string[] keys)
        {
            foreach (var key in keys)
            {
                var matchData = keyMatcher.Match<string>(key);
                var actions = new Actions(WebDriver);

                if (matchData.IsMatched)
                    actions.KeyDown(matchData.MatchedKey).Perform();
                else
                {
                    char[] chars = matchData.Key.ToCharArray();

                    foreach (char @char in chars)
                        actions.KeyDown(@char.ToString()).Perform();
                }
            }
        }

        public void KeysDown(params Key[] keys)
        {
            KeysDown(keys.Select(x => x.ToString()).ToArray());
        }
    }
}
