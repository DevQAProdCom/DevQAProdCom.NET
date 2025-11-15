using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Attributes
{
    public abstract class BaseUiElementSearchAttribute : Attribute
    {
        public string? ElementsFindMethod { get; set; }
        public string? ElementsFindCriteria { get; set; }
        public string? FramesFindMethod { get; set; }
        public string? FramesFindCriteria { get; set; }
        public string? ShadowRootHostsFindMethod { get; set; }
        public string? ShadowRootHostsFindCriteria { get; set; }
        public FindOrderType FindOrderType { get; }

        public BaseUiElementSearchAttribute(string? elementsFindMethod = null, string? elementsFindCriteria = null,
           string? framesFindMethod = null, string? framesFindCriteria = null,
           string? shadowRootHostsFindMethod = null, string? shadowRootHostsFindCriteria = null, FindOrderType findOrderType = FindOrderType.NotSet)
        {
            ElementsFindMethod = elementsFindMethod;
            ElementsFindCriteria = elementsFindCriteria;
            FramesFindMethod = framesFindMethod;
            FramesFindCriteria = framesFindCriteria;
            ShadowRootHostsFindMethod = shadowRootHostsFindMethod;
            ShadowRootHostsFindCriteria = shadowRootHostsFindCriteria;
            FindOrderType = findOrderType;
            CheckParameters();
        }

        private void CheckParameters()
        {
            //If both frame and shadow root find parameters are set - find order should be specified
            if (!string.IsNullOrEmpty(FramesFindMethod) && !string.IsNullOrEmpty(FramesFindCriteria)
                && !string.IsNullOrEmpty(ShadowRootHostsFindMethod) && !string.IsNullOrEmpty(ShadowRootHostsFindCriteria))
            {
                if (FindOrderType != FindOrderType.FrameInsideShadowRoot || FindOrderType != FindOrderType.ShadowRootInsideFrame)
                    throw new ArgumentException(@$"Either '{FindOrderType.FrameInsideShadowRoot}' or '{FindOrderType.ShadowRootInsideFrame}' 'FindOrderType findOrderType' parameter should be set 
                           when both FramesFindMethod, FramesFindCriteria and ShadowRootHostsFindMethod, ShadowRootHostsFindCriteria are set.");
            }
        }
    }
}
