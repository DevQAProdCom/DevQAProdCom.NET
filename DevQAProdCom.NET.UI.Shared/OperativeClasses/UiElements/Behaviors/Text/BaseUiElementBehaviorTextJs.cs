using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class BaseUiElementBehaviorTextJs(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters)
    {
        protected void ExecuteScript(string path, string text)
        {
            UiElement.FocusJs();
            var filePath = new FileInfo(path);
            UiElement.ExecuteJavaScript(filePath, new KeyValuePair<string, object>(SharedUiConstants.JavaScriptArguments.TextArgument, text));
        }
    }
}



//protected string GetScriptPath(string path)
//{
//    var currentDirectory = Directory.GetCurrentDirectory();
//    var parentDirectory = Directory.GetParent(currentDirectory)?.FullName;

//    if (!string.IsNullOrEmpty(parentDirectory))
//        return Path.Combine(parentDirectory, path);
//    return path;
//}

//protected void ExecuteScript(string path, string text)
//{
//    UiElement.FocusJs();
//    var scriptPath = GetScriptPath(path);
//    var filePath = new FileInfo(scriptPath);
//    UiElement.ExecuteJavaScript(filePath, new KeyValuePair<string, object>("textParameter", text));
//}
