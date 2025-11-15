using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class ShadowRootHostAttribute : BaseUiElementSearchAttribute
    {
        public ShadowRootHostAttribute(string shadowRootHostsFindMethod, string shadowRootHostsFindCriteria)
            : base(shadowRootHostsFindMethod: shadowRootHostsFindMethod, shadowRootHostsFindCriteria: shadowRootHostsFindCriteria)
        {
        }

        public ShadowRootHostAttribute(Use shadowRootHostsFindMethod, string shadowRootHostsFindCriteria)
            : this(shadowRootHostsFindMethod: shadowRootHostsFindMethod.ToString(), shadowRootHostsFindCriteria: shadowRootHostsFindCriteria)
        {
        }

        public ShadowRootHostAttribute(string shadowRootHostsFindMethod, string shadowRootHostsFindCriteria, string framesFindMethod, string framesFindCriteria)
            : base(shadowRootHostsFindMethod: shadowRootHostsFindMethod, shadowRootHostsFindCriteria: shadowRootHostsFindCriteria,
                  framesFindMethod: framesFindMethod, framesFindCriteria: framesFindCriteria, findOrderType: FindOrderType.FrameInsideShadowRoot)
        {
        }

        public ShadowRootHostAttribute(Use shadowRootHostsFindMethod, string shadowRootHostsFindCriteria, Use framesFindMethod, string framesFindCriteria)
            : this(shadowRootHostsFindMethod: shadowRootHostsFindMethod.ToString(), shadowRootHostsFindCriteria: shadowRootHostsFindCriteria,
                  framesFindMethod: framesFindMethod.ToString(), framesFindCriteria: framesFindCriteria)
        {
        }
    }
}
