using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements
{
    public class UiElementInstantiationStageInfo : IUiElementInstantiationStageInfo
    {
        public string _name;
        public string Name
        {
            get

            {
                if (UiIndex != null)
                    return $"{_name}[{UiIndex}]";

                return _name;
            }
        }

        private string? _fullName;
        public string FullName
        {
            get
            {
                if (_fullName == null)
                    _fullName = GetFullName();

                return _fullName;
            }
        }

        public IReadOnlyList<IUiElementsFindInfo> FindOptions { get; internal set; }
        public IParentUiElement? ParentContainer { get; internal set; }

        private IReadOnlyList<INestingLevelFindParameters>? _nestingLevelsFindParameters;
        public IReadOnlyList<INestingLevelFindParameters> NestingLevelsFindParameters
        {
            get
            {
                if (_nestingLevelsFindParameters == null)
                    _nestingLevelsFindParameters = GetAllNestingLevelFindParameters();

                return _nestingLevelsFindParameters;
            }
        }

        public int NestingLevel => NestingLevelsFindParameters.Count;

        private int? _uiIndex;
        public int? UiIndex
        {
            get
            {
                return _uiIndex;
            }
            private set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "Index of UI element should be more than or equal 1.");

                _uiIndex = value;
            }
        }

        private bool _isList = false;
        public bool IsList
        {
            get
            {
                if (UiIndex >= 1)
                    _isList = false;

                return _isList;
            }
            private set
            {
                _isList = value;
            }
        }

        public bool IsElementOfList => UiIndex >= 1;

        public UiElementInstantiationStageInfo(string name,
            List<IUiElementsFindInfo> findOptions,
            IParentUiElement? parentContainer = null,
            bool isList = false,
            int? uiIndex = null)
        {
            _name = name;
            FindOptions = findOptions.AsReadOnly();
            ParentContainer = parentContainer;
            IsList = isList;
            UiIndex = uiIndex;
        }

        private string GetFullName()
        {
            var fullName = new List<string>() { Name };
            var parent = ParentContainer;

            while (parent != null)
            {
                fullName.Add(parent.Info.InstantiationStage.Name);
                parent = parent.Info.InstantiationStage.ParentContainer;
            }

            fullName.Add(SharedUiConstants.Page);
            fullName.Reverse();

            return string.Join(" -> ", fullName);
        }

        private IReadOnlyList<INestingLevelFindParameters> GetAllNestingLevelFindParameters()
        {
            List<INestingLevelFindParameters> nestingLevelsFindParameters = new();
            var nestingLevel = GetNestingLevel();

            //ADD CURRENT ELEMENT NESTING LEVEL
            var nestingLevelFindParameters = GetSpecifiedNestingLevelFindParameters(nestingLevel, Name, FindOptions, ParentContainer!, IsList, UiIndex);
            nestingLevelsFindParameters.Add(nestingLevelFindParameters);

            //ADD NESTING LEVELS OF ALL PARENT CONTAINERS
            IUiElement? parentContainer = ParentContainer;

            while (parentContainer != null)
            {
                nestingLevel--;
                nestingLevelFindParameters = GetSpecifiedNestingLevelFindParameters(nestingLevel, parentContainer.Info.InstantiationStage.Name, parentContainer.Info.InstantiationStage.FindOptions, parentContainer.Info.InstantiationStage.ParentContainer!, parentContainer.Info.InstantiationStage.IsList, parentContainer.Info.InstantiationStage.UiIndex);
                nestingLevelsFindParameters.Insert(0, nestingLevelFindParameters);
                parentContainer = parentContainer.Info.InstantiationStage.ParentContainer;
            }

            return nestingLevelsFindParameters.AsReadOnly();
        }

        private INestingLevelFindParameters GetSpecifiedNestingLevelFindParameters(uint nestingLevel, string name, IReadOnlyList<IUiElementsFindInfo> findOptions, IUiElement parentContainer, bool isList, int? uiIndex)
        {
            List<IFindParameters> findParametersList = new();

            foreach (var findOption in findOptions)
            {
                IFindParameters findParameters = new FindParameters(name, findOption, nestingLevel, parentContainer, isList, uiIndex);
                findParametersList.Add(findParameters);
            }

            INestingLevelFindParameters nestingLevelFindParameters = new NestingLevelFindParameters(findParametersList.AsReadOnly(), nestingLevel);

            return nestingLevelFindParameters;
        }

        private uint GetNestingLevel()
        {
            uint nestingLevel = 0;
            IUiElement? parentContainer = ParentContainer;

            while (parentContainer != null)
            {
                nestingLevel++;
                parentContainer = parentContainer.Info.InstantiationStage.ParentContainer;
            }

            return nestingLevel;
        }
    }
}
