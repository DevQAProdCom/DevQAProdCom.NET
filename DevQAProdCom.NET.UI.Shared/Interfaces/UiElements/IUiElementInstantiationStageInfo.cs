using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElementInstantiationStageInfo
    {
        public string Name { get; }
        public string FullName { get; }

        public IReadOnlyList<IUiElementsFindInfo> FindOptions { get; }
        public IUiElement? ParentContainer { get; }
        public int NestingLevel { get; }
        public IReadOnlyList<INestingLevelFindParameters> NestingLevelsFindParameters { get; }
        public bool IsList { get; }
        public int? UiIndex { get; }
        public bool IsElementOfList { get; }
    }
}
