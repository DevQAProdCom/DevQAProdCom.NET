using DevQAProdCom.NET.UI.Shared.OperativeClasses;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Behaviors
{
    public interface IKeyMatcher
    {
        //public bool TryMatchOrDefault(char key, out object result);
        //public KeyMatchResultModel Match(string key);

        //public bool TryMatchOrDefault<T>(char key, out T result)
        //{
        //    if (TryMatchOrDefault(key, out object objectResult))
        //    {
        //        result = (T)objectResult;
        //        return true;
        //    }
        //    else
        //    {
        //        result = (T)objectResult;
        //        return false;
        //    }
        //}

        public KeyMatchResultModel<T> Match<T>(string key) where T : class;
        //{



        //    if (TryMatchOrDefault(key, out object objectResult))
        //    {
        //        result = (T)objectResult;
        //        return true;
        //    }
        //    else
        //    {
        //        result = (T)objectResult;
        //        return false;
        //    }
        //}

        //public List<T> Match<T>(params char[] keys)
        //{
        //    List<T> keysMatches = new();

        //    foreach (var key in keys)
        //        keysMatches.Add(Match<T>(key));

        //    return keysMatches;
        //}

        public List<KeyMatchResultModel<T>> Match<T>(params string[] keys) where T : class
        {
            List<KeyMatchResultModel<T>> keysMatches = new();

            foreach (var key in keys)
                keysMatches.Add(Match<T>(key));

            return keysMatches;
        }

        public void RegisterKeyMatch(string key, object keyMatchedValue);
    }
}
