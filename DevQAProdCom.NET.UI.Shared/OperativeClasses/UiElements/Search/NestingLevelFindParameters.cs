using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search
{
    public class NestingLevelFindParameters : INestingLevelFindParameters
    {
        public IReadOnlyList<IFindParameters> Parameters { get; set; }
        public uint NestingLevel { get; set; }

        public NestingLevelFindParameters(IReadOnlyList<IFindParameters> parameters, uint nestingLevel = 0)
        {
            Parameters = parameters;
            NestingLevel = nestingLevel;
        }
    }
}
