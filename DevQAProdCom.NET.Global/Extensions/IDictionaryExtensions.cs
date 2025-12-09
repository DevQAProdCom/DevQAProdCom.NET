namespace DevQAProdCom.NET.Global.Extensions
{
    public static class IDictionaryExtensions
    {
        public static void Upsert<TKey, TValue>(this IDictionary<TKey, TValue>? dict, TKey key, TValue value)
        {
            if (dict != null)
            {
                if (dict.ContainsKey(key))
                    dict[key] = value;
                else
                    dict.Add(key, value);
            }
        }

        public static void Upsert<TKey, TValue>(this IDictionary<TKey, TValue>? dict,
            params KeyValuePair<TKey, TValue>[] keyValuePairs)
            where TKey : notnull
            where TValue : notnull
        {
            if (dict != null && keyValuePairs?.Length > 0)
                foreach (var keyValuePair in keyValuePairs)
                    dict.Upsert(keyValuePair.Key, keyValuePair.Value);
        }

        public static void Upsert<TKey, TValue>(this IDictionary<TKey, TValue>? targetDictionary,
           IDictionary<TKey, TValue>? sourceDictonary)
            where TKey : notnull
            where TValue : notnull
        {
            if (sourceDictonary?.Count() > 0)
                targetDictionary.Upsert(sourceDictonary.ToArray());
        }

        public static TValue Get<TValue>(this IDictionary<string, object>? dict, string key)
        {
            if (dict?.Count > 0)
            {
                if (dict.TryGetValue(key, out object? value))
                {
                    try
                    {
                        return (TValue)value;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Unable to convert value from dictionary received by key '{key}' to type '{typeof(TValue).FullName}'.");
                    }
                }

                throw new KeyNotFoundException($"No entry with key '{key}' exists in dictionary.");
            }

            throw new ArgumentException("Provided dictionary is either null or empty.");
        }
    }
}
