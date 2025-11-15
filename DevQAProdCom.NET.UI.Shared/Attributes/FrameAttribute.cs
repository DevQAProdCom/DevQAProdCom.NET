using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class FrameAttribute : BaseUiElementSearchAttribute
    {
        public FrameAttribute(string framesFindMethod, string framesFindCriteria) : base(framesFindMethod: framesFindMethod, framesFindCriteria: framesFindCriteria)
        {
        }

        public FrameAttribute(Use framesFindMethod, string framesFindCriteria) : this(framesFindMethod: framesFindMethod.ToString(), framesFindCriteria: framesFindCriteria)
        {
        }

        public FrameAttribute(string framesFindMethod, string framesFindCriteria, string shadowRootHostsFindMethod, string shadowRootHostsFindCriteria)
            : base(framesFindMethod: framesFindMethod, framesFindCriteria: framesFindCriteria, 
                  shadowRootHostsFindMethod: shadowRootHostsFindMethod, shadowRootHostsFindCriteria: shadowRootHostsFindCriteria,
                  findOrderType: FindOrderType.ShadowRootInsideFrame)
        {
        }

        public FrameAttribute(Use framesFindMethod, string framesFindCriteria, Use shadowRootHostsFindMethod, string shadowRootHostsFindCriteria)
             : this(framesFindMethod: framesFindMethod.ToString(), framesFindCriteria: framesFindCriteria,
                   shadowRootHostsFindMethod: shadowRootHostsFindMethod.ToString(), shadowRootHostsFindCriteria: shadowRootHostsFindCriteria)
        {
        }
    }
}
