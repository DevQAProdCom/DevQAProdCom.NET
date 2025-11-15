namespace DevQAProdCom.NET.UI.Shared.Constants
{
    public class SharedUiConstants
    {
        public const string DefaultUiInteractorInstance = "Default UiInteractor Instance";
        public const string DefaultUiInteractorsManagerInstance = "Default UiInteractor Instance";

        public const string Tab = "Tab";
        public const string DefaultTab = "Default Tab";
        public const string NativeElement = "Native Element";
        public const string UiElementArgument = "uiElementArgument";
        public const string IUiElement = "IUiElement";
        public const string IWebDriver = "IWebDriver";
        public const string IBrowser = "IBrowser";
        public const string IUiPage = "IUiPage";
        public const string ILocator = "ILocator";
        public const string TextArgument = "textArgument";
        public const string KeyCodeArgument = "keyCodeArgument";
        public const string IExecuteJavaScript = "IExecuteJavaScript";
        public const string XArgument = "xArgument";
        public const string YArgument = "yArgument";
        public const string NoMethodProvided = "NoMethodProvided";
        public const string NoCriteriaProvided = "NoCriteriaProvided";
        public const string ElementsSemicolon = "Elements:";
        public const string FramesSemicolon = "Frames:";
        public const string FrameFindOption = "FrameFindOption";
        public const string FindOption = "FindOption";
        public const string FindParameters = "FindParameters";
        public const string FindParametersSemicolon = $"{FindParameters}:";
        public const string FindOptionSemicolon = $"{FindOption}:";
        public const string UseDot = "Use.";
        public const string Name = "Name";
        public const string NameSemicolon = $"{Name}:";
        public const string ExceptionMessage = "EXCEPTION MESSAGE";
        public const string Hyphen = "-";
        public const string DefaultCookiePathValue = "/";
        public const string ShadowRootHostsSemicolon = "ShadowRootHosts:";
        public const string BROWSER = "BROWSER";
        public const string BROWSER_VERSION = "BROWSER_VERSION";
        public const string DownloadsDefaultDirectory = "DownloadsDefaultDirectory";
        public const string Page = "Page";

        public const double ScrollUntilTimeoutSec = 60;
        public const double ScrollUntilPollingIntervalSec = 0.5;

        public const double UiElementWaitTimeoutSec = 60;
        public const double UiElementWaitPollingIntervalSec = 0.5;
        public static class Files
        {
            public const string JavaScriptFilesFolder = "JavaScripts";
            public static string CopyPasteTextJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "CopyPasteText.js");
            public static string SetInnerHtmlJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "SetInnerHtml.js");
            public static string SetTextContentJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "SetTextContent.js");
            public static string GetElementFilesListJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "GetElementFilesList.js");
            public static string KeyDownJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "KeyDown.js");
            public static string KeyUpJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "KeyUp.js");
            public static string KeyPressJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "KeyPress.js");
            public static string IsElementInViewportJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "IsElementInViewport.js");
            public static string FocusJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "Focus.js");
            public static string MouseDownJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "MouseDown.js");
            public static string MouseUpJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "MouseUp.js");
            public static string MouseMoveJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "MouseMove.js");
            public static string MouseContextClickJavaScriptFilePath = Path.Combine(JavaScriptFilesFolder, "MouseContextClick.js");
        }
    }
}
