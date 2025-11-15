using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search
{
    public class FindOption : IFindOption
    {
        public string? Method { get; }
        public string? Criteria { get; }

        public FindOption(string method, string criteria)
        {
            Method = method;
            Criteria = criteria;
        }

        public FindOption(Use method, string criteria) : this(method.ToString(), criteria)
        {
        }

        public override string ToString()
        {
            string method = string.IsNullOrEmpty(Method) ? SharedUiConstants.NoMethodProvided : Method;
            string criteria = string.IsNullOrEmpty(Criteria) ? SharedUiConstants.NoCriteriaProvided : Criteria;

            return $"{SharedUiConstants.UseDot}{method}(\"{criteria}\")";
        }

        public bool IsSet => (!string.IsNullOrEmpty(Method) && Method != Use.NotSet.ToString()) && !string.IsNullOrEmpty(Criteria);

    }
}
