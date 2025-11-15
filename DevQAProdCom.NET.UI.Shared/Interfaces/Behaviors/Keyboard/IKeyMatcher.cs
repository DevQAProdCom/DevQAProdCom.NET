namespace DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Keyboard
{
    public interface IKeyMatcher
    {
        public object Match(string keyIdentifier);
        public void RegisterKeyMatch(string keyIdentifier, object keyMatchedValue);
    }
}
