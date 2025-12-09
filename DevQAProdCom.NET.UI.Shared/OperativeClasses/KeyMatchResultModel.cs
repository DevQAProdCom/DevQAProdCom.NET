namespace DevQAProdCom.NET.UI.Shared.OperativeClasses
{
    public class KeyMatchResultModel<T>
    {
        public string Key { get; set; }
        public T? MatchedKey { get; set; }
        public bool IsMatched { get; set; }
    }
}
