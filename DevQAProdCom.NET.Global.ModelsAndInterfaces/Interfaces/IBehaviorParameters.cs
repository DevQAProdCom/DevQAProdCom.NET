namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces
{
    public interface IBehaviorParameters
    {
        public Dictionary<string, object> ParamsDictionary { get; set; }
        public T Get<T>(string key);
        public T? GetOrDefault<T>(string key);
        public bool ContainsKey(string key);
    }
}
