using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Behaviors;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses
{
    public abstract class BaseKeyMatcher<T> : IKeyMatcher where T : class
    {
        protected Dictionary<string, T> KeysMatchDict = new();

        //public T MatchT(char key)
        //{
        //    return FindMatch(key);
        //}

        //public bool MatchT(string key, T result)
        //{
        //    return TryFindMatch(key, out result);
        //}

        public void RegisterKeyMatchT(string key, T keyMatchedValue)
        {
            KeysMatchDict.Add(key, keyMatchedValue);
        }

        //protected T FindMatch(char key)
        //{
        //    return FindMatch(key.ToString());
        //}
        //protected abstract bool TryFindMatch(string key, out T result);


        public KeyMatchResultModel<TResult> Match<TResult>(string key) where TResult : class
        {
            KeyMatchResultModel<TResult> keyMatchResultModel = new KeyMatchResultModel<TResult>() { Key = key };

            keyMatchResultModel.IsMatched = KeysMatchDict.TryGetValue(key, out T? result);

            if (keyMatchResultModel.IsMatched)
                keyMatchResultModel.MatchedKey = result as TResult;

            return keyMatchResultModel;
        }


        #region Explicit implementation of interface
        // public KeyMatchResultModel<T> Match<T>(string key);

        //public object Match(char key) => MatchT(key);
        //public object Match(string key) => MatchT(key);
        public void RegisterKeyMatch(string key, object keyMatchedValue) => RegisterKeyMatchT(key, (T)keyMatchedValue);

        #endregion Explicit implementation of interface
    }
}
