namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Traits.Others
{
    public interface IUiElementTraitGetAttribute
    {
        bool GetBooleanAttribute(string attributeName);
        string? GetNonBooleanAttribute(string attributeName);
        string? GetAttribute(string attributeName, bool isBooleanAttributeType); //explicit attributes / boolean attributes //TODO write comment why isPlaywright returns "empty"  if attrubite exists but not set to particular value
    }
}
