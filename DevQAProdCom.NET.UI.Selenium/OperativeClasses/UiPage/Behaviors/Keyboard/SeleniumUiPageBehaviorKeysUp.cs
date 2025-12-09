using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Keyboard;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiPage.Behaviors.Keyboard
{
    public class SeleniumUiPageBehaviorKeysUp(IBehaviorParameters parameters, IKeyMatcher keyMatcher) : SeleniumUiPageBehavior(parameters), IUiPageBehaviorKeysUp
    {
        public void KeysUp(params string[] keys)
        {
            foreach (var key in keys)
            {
                var matchData = keyMatcher.Match<string>(key);
                var actions = new Actions(WebDriver);

                if (matchData.IsMatched)
                    actions.KeyUp(matchData.MatchedKey).Perform();
                else
                {
                    char[] chars = matchData.Key.ToCharArray();

                    foreach (char @char in chars)
                        actions.KeyUp(@char.ToString()).Perform();
                }
            }
        }

        public void KeysUp(params Key[] keys)
        {
            KeysUp(keys.Select(x => x.ToString()).ToArray());
        }
    }
}
