using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class FindAttribute : BaseUiElementSearchAttribute
    {
        public FindAttribute(string elementsFindMethod, string elementsFindCriteria,
           string? framesFindMethod = null, string? framesFindCriteria = null,
           string? shadowRootHostsFindMethod = null, string? shadowRootHostsFindCriteria = null, FindOrderType findOrderType = FindOrderType.NotSet)
           : base(elementsFindMethod: elementsFindMethod, elementsFindCriteria: elementsFindCriteria,
                 framesFindMethod: framesFindMethod, framesFindCriteria: framesFindCriteria,
                 shadowRootHostsFindMethod: shadowRootHostsFindMethod, shadowRootHostsFindCriteria: shadowRootHostsFindCriteria, 
                 findOrderType: findOrderType)
        {
        }

        public FindAttribute(Use elementsFindMethod, string elementsFindCriteria,
            Use framesFindMethod = Use.NotSet, string? framesFindCriteria = null,
            Use shadowRootHostsFindMethod = Use.NotSet, string? shadowRootHostsFindCriteria = null, FindOrderType findOrderType = FindOrderType.NotSet)
            : this(elementsFindMethod: elementsFindMethod.ToString(), elementsFindCriteria: elementsFindCriteria,
                  framesFindMethod: framesFindMethod.ToString(), framesFindCriteria: framesFindCriteria,
                  shadowRootHostsFindMethod: shadowRootHostsFindMethod.ToString(), shadowRootHostsFindCriteria: shadowRootHostsFindCriteria,
                  findOrderType: findOrderType)
        {
        }
    }
}
