using ApplicationName.QA.TestsBasis.Ui.UiElements;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class HomeTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/HomeTestPage";

        //MULTIPLE FIND ATTRIBUTES

        [Find(Use.IdEquals, "multiple-find-attributes-1")]
        [Find(Use.ClassNameEquals, "multiple-find-attributes-2")]
        public IUiElementsList<IUiElement> ButtonMultipleFindAttributes;

        [Find(Use.XPath, "//table[@id='NOT EXISTING XPATH']")]
        [Find(Use.XPath, "//table[@id='Table2']")]
        public Table Table2;

        //DYNAMIC UiElements INSTANTIATION

        private IUiElement? _dynamiclyInstantiatedUiElement;
        public IUiElement Dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator
        {
            get
            {
                _dynamiclyInstantiatedUiElement ??= Find<IUiElement>(
                    Use.XPath,
                    "//table[@id='Table2']//tr[2]//th[2]",
                    name: "Dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator");

                return _dynamiclyInstantiatedUiElement;
            }
        }

        //FRAMES UiElements support with AUTO SWITCHING

        [Find(Use.IdEquals, "frame -> button",
            framesFindMethod: Use.IdEquals,
            framesFindCriteria: "frame")]
        public IUiElement ButtonInsideFrame;

        [Frame(Use.IdEquals, "frame")]
        public TopLevelFrame Frame;

        [Find(Use.IdEquals, "button-not-in-frame")]
        public IUiElement ButtonNotInFrame;


        private IUiElement? _buttonInsideFrame_DynamicInstantiation;
        public IUiElement ButtonInsideFrame_DynamicInstantiation
        {
            get
            {
                if (_buttonInsideFrame_DynamicInstantiation == null)
                {
                    IUiElementsFindInfo findOption = new UiElementsFindInfo(
                        elementsFindMethod: Use.IdEquals.ToString(), elementsFindCriteria: "frame -> button",
                        framesFindMethod: Use.IdEquals.ToString(), framesFindCriteria: "frame");

                    _buttonInsideFrame_DynamicInstantiation = Find<IUiElement>(findOptions: new() { findOption },
                        name: nameof(ButtonInsideFrame_DynamicInstantiation));
                }

                return _buttonInsideFrame_DynamicInstantiation;
            }
        }

        //SHADOW ROOT HOST UiElements support with AUTO SWITCHING

        [Find(Use.IdEquals, "shadow-root-host -> button",
            shadowRootHostsFindMethod: Use.IdEquals,
            shadowRootHostsFindCriteria: "shadow-root-host")]
        public IUiElement ButtonInsideShadowRootHost;

        [Frame(Use.IdEquals, "shadow-root-host")]
        public TopLevelShadowRootHost ShadowRootHost;

        [Find(Use.IdEquals, "button-not-in-shadow-root-host")]
        public IUiElement ButtonNotInShadowRootHost;

        private IUiElement? _buttonInsideShadowRootHost_DynamicInstantiation;
        public IUiElement ButtonInsideShadowRootHost_DynamicInstantiation
        {
            get
            {
                if (_buttonInsideShadowRootHost_DynamicInstantiation == null)
                {
                    IUiElementsFindInfo findOption = new UiElementsFindInfo(
                        elementsFindMethod: Use.IdEquals.ToString(), elementsFindCriteria: "shadow-root-host -> button",
                        shadowRootHostsFindMethod: Use.IdEquals.ToString(), shadowRootHostsFindCriteria: "shadow-root-host");

                    _buttonInsideShadowRootHost_DynamicInstantiation = Find<IUiElement>(
                        findOptions: new() { findOption },
                        name: nameof(ButtonInsideShadowRootHost_DynamicInstantiation));
                }

                return _buttonInsideShadowRootHost_DynamicInstantiation;
            }
        }

        //COMPLEX SCENARIOS

        //UIELEMENT INSIDE FRAME WITHIN SHADOW ROOT HOST USING ATTRIBUTE

        [Find(shadowRootHostsFindMethod: Use.IdEquals, shadowRootHostsFindCriteria: "shadow-root-host",
            framesFindMethod: Use.IdEquals, framesFindCriteria: "shadow-root-host -> frame",
            elementsFindMethod: Use.IdEquals, elementsFindCriteria: "shadow-root-host -> frame -> button",
            findOrderType: FindOrderType.FrameInsideShadowRootHost)]
        public IUiElement ButtonInsideFrameWithinShadowRootHost;

        [Frame(framesFindMethod: Use.IdEquals,
            framesFindCriteria: "shadow-root-host -> frame",
            shadowRootHostsFindMethod: Use.IdEquals,
            shadowRootHostsFindCriteria: "shadow-root-host")]
        public FrameInsideShadowRootHost FrameInsideShadowRootHost;

        [ShadowRootHost(shadowRootHostsFindMethod: Use.IdEquals,
            shadowRootHostsFindCriteria: "frame -> shadow-root-host",
            framesFindMethod: Use.IdEquals,
            framesFindCriteria: "frame")]
        public ShadowRootHostInsideFrame ShadowRootHostInsideFrame;

        private IUiElement? _buttonInsideShadowRootHostWithinFrame_DynamicInstantiation;
        public IUiElement ButtonInsideShadowRootHostWithinFrame_DynamicInstantiation
        {
            get
            {
                if (_buttonInsideShadowRootHostWithinFrame_DynamicInstantiation == null)
                {
                    IUiElementsFindInfo findOption = new UiElementsFindInfo(
                          framesFindMethod: Use.IdEquals.ToString(), framesFindCriteria: "frame",
                          shadowRootHostsFindMethod: Use.IdEquals.ToString(), shadowRootHostsFindCriteria: "frame -> shadow-root-host",
                          elementsFindMethod: Use.IdEquals.ToString(), elementsFindCriteria: "frame -> shadow-root-host -> button",
                          findOrderType: FindOrderType.ShadowRootHostInsideFrame);

                    _buttonInsideShadowRootHostWithinFrame_DynamicInstantiation = Find<IUiElement>(
                        findOptions: new() { findOption },
                        name: nameof(ButtonInsideShadowRootHostWithinFrame_DynamicInstantiation));
                }

                return _buttonInsideShadowRootHostWithinFrame_DynamicInstantiation;
            }
        }

        //CUSTOM FIND METHODS

        [Find("CustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute", "attribute-for-custom-find-option-search-method-registered-from-activator-create-instance-t-value")]
        public IUiElement CustomFindMethodElement;
    }
}
