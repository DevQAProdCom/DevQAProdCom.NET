using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public abstract class BaseKeyMatcher<T> : IKeyMatcher
    {
        protected Dictionary<string, T> KeysMatchDict = new();

        public T MatchT(string keyIdentifier)
        {
            return FindMatch(keyIdentifier);
        }

        public void RegisterKeyMatchT(string keyIdentifier, T keyMatchedValue)
        {
            KeysMatchDict.Add(keyIdentifier, keyMatchedValue);
        }

        protected abstract T FindMatch(string keyIdentifier);

        #region Explicit implementation of interface

        public object Match(string keyIdentifier) => MatchT(keyIdentifier);
        public void RegisterKeyMatch(string keyIdentifier, object keyMatchedValue) => RegisterKeyMatchT(keyIdentifier, (T)keyMatchedValue);

        #endregion Explicit implementation of interface
    }
}
