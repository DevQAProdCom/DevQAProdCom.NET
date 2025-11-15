using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.OperativeClasses;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public abstract class BaseKeyboardBehavior : BaseBehavior, IKeyboardBehavior
    {
        protected IKeyMatcher KeyMatcher;
        protected IUiElement? UiElement { get; }

        public BaseKeyboardBehavior(IBehaviorParameters parameters, IKeyMatcher keyMatcher) : base(parameters)
        {
            KeyMatcher = keyMatcher;
            UiElement = Parameters.GetOrDefault<IUiElement>(SharedUiConstants.IUiElement);
        }

        public abstract void KeysDown(params string[] keys);
        public abstract void KeysUp(params string[] keys);
        public abstract void PressKeysSequentially(params string[] keys);

        public void PressKeysSimultaneously(string keysCombination)
        {
            string[] keys = keysCombination.ToArray("+");
            PressKeysSimultaneously(keys);
        }

        public void PressKeysSimultaneously(params string[] keys)
        {
            if (keys?.Length > 0)
            {
                //Key Down
                KeysDown(keys);

                //Keys Up
                string[] keysReversed = keys.Reverse().ToArray();
                KeysUp(keysReversed.ToArray());
            }
        }

        public abstract void SendText(string textKeys);

        protected T Match<T>(string key)
        {
            T keyMatch = (T)KeyMatcher.Match(key);
            return keyMatch;
        }

        protected List<T> Match<T>(params string[] keys)
        {
            List<T> keysMatches = new();

            foreach (var key in keys)
                keysMatches.Add(Match<T>(key));

            return keysMatches;
        }
    }
}
