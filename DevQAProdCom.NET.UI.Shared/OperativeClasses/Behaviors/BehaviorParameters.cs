using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public class BehaviorParameters : IBehaviorParameters
    {
        public Dictionary<string, object> ParamsDictionary { get; set; } = new();

        public BehaviorParameters(params KeyValuePair<string, object>[] parameters)
        {
            foreach (var parameter in parameters)
                ParamsDictionary.Add(parameter.Key, parameter.Value);
        }

        public T Get<T>(string key)
        {
            return ParamsDictionary.Get<T>(key);
        }

        public T? GetOrDefault<T>(string key)
        {
            if (ContainsKey(key))
                return ParamsDictionary.Get<T>(key);

            return default;
        }

        public bool ContainsKey(string key)
        {
            return ParamsDictionary.ContainsKey(key);
        }
    }
}
