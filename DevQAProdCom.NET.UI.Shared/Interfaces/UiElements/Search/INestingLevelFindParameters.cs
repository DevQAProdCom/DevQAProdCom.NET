namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search
{
    public interface INestingLevelFindParameters
    {
        public IReadOnlyList<IFindParameters> Parameters { get; }
        public uint NestingLevel { get; }
    }
}
