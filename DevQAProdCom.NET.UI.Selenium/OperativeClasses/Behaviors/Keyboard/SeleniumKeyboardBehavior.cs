using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Keyboard
{
    public class SeleniumKeyboardBehavior : BaseKeyboardBehavior
    {
        protected IWebDriver? WebDriver { get; }

        public SeleniumKeyboardBehavior(IBehaviorParameters parameters, IKeyMatcher keyMatcher) : base(parameters, keyMatcher)
        {
            WebDriver = Parameters.GetOrDefault<IWebDriver>(SharedUiConstants.IWebDriver);
            WebDriver ??= (UiElement as SeleniumUiElement)?.GetIWebDriver();

            if (WebDriver == null)
                throw new ArgumentNullException($"'{SharedUiConstants.IWebDriver}' parameter should be provided to be able to apply Keyboard Behavior.");
        }

        public override void KeysDown(params string[] keys)
        {
            UiElement?.FocusJs();
            foreach (var key in keys)
                new Actions(WebDriver).KeyDown(Match<string>(key)).Perform();
        }

        public override void KeysUp(params string[] keys)
        {
            UiElement?.FocusJs();
            foreach (var key in keys)
                new Actions(WebDriver).KeyUp(Match<string>(key)).Perform();
        }

        public override void PressKeysSequentially(params string[] keys)
        {
            UiElement?.FocusJs();
            foreach (var key in keys)
            {
                var matchedKey = Match<string>(key);

                new Actions(WebDriver)
                    .KeyDown(matchedKey)
                    .KeyUp(matchedKey)
                    .Perform();
            }
        }

        public override void SendText(string textKeys)
        {
            UiElement?.FocusJs();
            new Actions(WebDriver).SendKeys(textKeys).Perform();
        }
    }
}
